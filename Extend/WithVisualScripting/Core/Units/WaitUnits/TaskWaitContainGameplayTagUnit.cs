#if SCOR_ENABLE_VISUALSCRIPTING
using System;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using StudioScor.Utilities.VisualScripting;

namespace StudioScor.GameplayTagSystem.Extend.VisualScripting
{
    [UnitTitle("Wait Contain GameplayTag")]
    [UnitShortTitle("TaskWaitContainTag")]
    [UnitSubtitle("GameplayTagSystem Task")]
    [UnitCategory("StudioScor\\Task\\GameplayTagSystem")]
    public class TaskWaitContainGameplayTagUnit : ToggleUpdateUnit
    {
        [DoNotSerialize]
        [PortLabel("Target")]
        [PortLabelHidden]
        [NullMeansSelf]
        public ValueInput Target;

        [Serialize]
        [Inspectable]
        [UnitHeaderInspectable]
        [PortLabel("Container Type")]
        public EContainerType ContainerType { get; private set; }

        [DoNotSerialize]
        [PortLabel("ContainType")]
        public ValueInput ContainType { get; private set; }


        [DoNotSerialize]
        [PortLabel("GameplayTag")]
        [PortLabelHidden]
        [NullMeansSelf]
        public ValueInput GameplayTag;

        [DoNotSerialize]
        [PortLabel("ToggleOn")]
        public ControlOutput ToggleOn;

        [DoNotSerialize]
        [PortLabel("ToggleOff")]
        public ControlOutput ToggleOff;


        [Serialize]
        [Inspectable]
        [UnitHeaderInspectable]
        [PortLabel("Structure Type")]
        public EStructureType StructureType { get; set; } = EStructureType.Target;

        private bool _UseList;



        public new class Data : ToggleUpdateUnit.Data
        {
            public IGameplayTagSystem GameplayTagSystemEvent;
            public IGameplayTagSystem GameplayTagSystem;
            public bool UseList;
            public EContainerType GameplayTagType;
            public EContainType ContainType;
            public GameplayTagSO GameplayTag;
            public GameplayTagSO[] GameplayTags;
            public bool IsToggle;
            public bool IsOn;

            public EventHook GrantHook;
            public EventHook RemoveHook;
            public Delegate GrantHandler;
            public Delegate RemoveHandler;
        }

        protected override void Definition()
        {
            base.Definition();

            Target = ValueInput<GameObject>(nameof(Target), null).NullMeansSelf();

            _UseList = StructureType.Equals(EStructureType.List);

            if (_UseList)
            {
                ContainType = ValueInput<EContainType>(nameof(ContainType), EContainType.All);
                GameplayTag = ValueInput<GameplayTagSO[]>(nameof(GameplayTag), null);
            }
            else
            {
                GameplayTag = ValueInput<GameplayTagSO>(nameof(GameplayTag), null);
            }

            ToggleOn = ControlOutput(nameof(ToggleOn));
            ToggleOff = ControlOutput(nameof(ToggleOff));

            Succession(Enter, ToggleOn);
            Succession(Enter, ToggleOff);
        }

        public override IGraphElementData CreateData()
        {
            return new Data();
        }

        protected override void EndActivate(Flow flow)
        {
            base.EndActivate(flow);

            var data = flow.stack.GetElementData<Data>(this);

            if(data.IsOn)
            {
                flow.Invoke(ToggleOff);
            }
        }

        protected override void OnUpdate(Flow flow)
        {
            var data = flow.stack.GetElementData<Data>(this);

            if(data.IsOn)
            {
                flow.Invoke(Update);
            }
        }

        protected override void SetValue(Flow flow)
        {
            var data = flow.stack.GetElementData<Data>(this);

            var target = flow.GetValue<GameObject>(Target);
            var gameplayTagSystem = flow.GetValue<IGameplayTagSystem>(Target);
            var gameplayTagSystemEvent = flow.GetValue<IGameplayTagSystem>(Target);

            MessageListener.AddTo(typeof(GameplayTagSystemMessageListener), target);

            // Set Event
            var reference = flow.stack.ToReference();
            EventHook grantHook;
            EventHook removeHook;

            if (ContainerType == EContainerType.Owned)
            {
                grantHook = new EventHook(GameplayTagSystemWithVisualScripting.GRANT_OWNED_TAG, gameplayTagSystemEvent);
                removeHook = new EventHook(GameplayTagSystemWithVisualScripting.REMOVE_OWNED_TAG, gameplayTagSystemEvent);
            }
            else
            {
                grantHook = new EventHook(GameplayTagSystemWithVisualScripting.GRANT_BLOCK_TAG, gameplayTagSystemEvent);
                removeHook = new EventHook(GameplayTagSystemWithVisualScripting.REMOVE_BLOCK_TAG, gameplayTagSystemEvent);
            }

            Action<GameplayTagSO> grantHandler = tag => GrantTag(reference, tag);
            Action<GameplayTagSO> removeHandler = tag => RemoveTag(reference, tag);

            EventBus.Register(grantHook, grantHandler);
            EventBus.Register(removeHook, removeHandler);

            data.GrantHook = grantHook;
            data.RemoveHook = removeHook;

            data.GrantHandler = grantHandler;
            data.RemoveHandler = removeHandler;

            data.GameplayTagSystem = gameplayTagSystem;
            data.GameplayTagSystemEvent = gameplayTagSystemEvent;
            data.GameplayTagType = ContainerType;
            
            data.UseList = _UseList;

            if (_UseList)
            {
                var gameplayTags = flow.GetValue<GameplayTagSO[]>(GameplayTag);

                data.GameplayTag = null;
                data.GameplayTags = gameplayTags;
                data.ContainType = flow.GetValue<EContainType>(ContainType);
            }
            else
            {
                var gameplayTag = flow.GetValue<GameplayTagSO>(GameplayTag);

                data.GameplayTag = gameplayTag;
                data.GameplayTags = null;
            }
        }

        protected override void ResetValue(Flow flow)
        {
            var data = flow.stack.GetElementData<Data>(this);

            EventBus.Unregister(data.GrantHook, data.GrantHandler);
            EventBus.Unregister(data.RemoveHook, data.RemoveHandler);

            data.GrantHandler = null;
            data.RemoveHandler = null;
        }

        protected override void UpdateValue(Flow flow)
        {
            var data = flow.stack.GetElementData<Data>(this);

            data.DeltaTime = data.IsManual ? flow.GetValue<float>(ManualDeltaTime) : GetDeltaTime;


            flow.SetValue(DeltaTime, data.DeltaTime);
        }

        private void GrantTag(GraphReference reference, GameplayTagSO tag)
        {
            var data = reference.GetElementData<Data>(this);

            if (data.IsOn)
                return;

            if (data.UseList)
            {
                if (data.GameplayTags.Contains(tag))
                {
                    if (data.ContainType == EContainType.All)
                    {
                        if (data.GameplayTagType == EContainerType.Owned)
                        {
                            if (data.GameplayTagSystem.ContainAllTagsInOwned(data.GameplayTags))
                            {
                                data.IsOn = true;

                                var flow = Flow.New(reference);

                                flow.Run(ToggleOn);
                            }
                        }
                        else
                        {
                            if (data.GameplayTagSystem.ContainAllTagsInBlock(data.GameplayTags))
                            {
                                data.IsOn = true;

                                var flow = Flow.New(reference);

                                flow.Run(ToggleOn);
                            }
                        }
                    }
                    else
                    {
                        data.IsOn = true;

                        var flow = Flow.New(reference);

                        flow.Run(ToggleOn);
                    }
                }
            }
            else
            {
                if (data.GameplayTag == tag)
                {
                    data.IsOn = true;

                    var flow = Flow.New(reference);

                    flow.Run(ToggleOn);
                }
            }
        }
        private void RemoveTag(GraphReference reference, GameplayTagSO tag)
        {
            var data = reference.GetElementData<Data>(this);

            if (!data.IsOn)
                return;

            if (data.UseList)
            {
                if (data.GameplayTags.Contains(tag))
                {
                    if (data.ContainType == EContainType.Any)
                    {
                        if (data.GameplayTagType == EContainerType.Owned)
                        {
                            if (!data.GameplayTagSystem.ContainAllTagsInOwned(data.GameplayTags))
                            {
                                data.IsOn = false;

                                var flow = Flow.New(reference);

                                flow.Run(ToggleOff);
                            }
                        }
                        else
                        {
                            if (!data.GameplayTagSystem.ContainAllTagsInBlock(data.GameplayTags))
                            {
                                data.IsOn = false;

                                var flow = Flow.New(reference);

                                flow.Run(ToggleOff);
                            }
                        }
                    }
                    else
                    {
                        data.IsOn = false;

                        var flow = Flow.New(reference);

                        flow.Run(ToggleOff);
                    }
                }
            }
            else
            {
                if (data.GameplayTag == tag)
                {
                    data.IsOn = false;

                    var flow = Flow.New(reference);

                    flow.Run(ToggleOff);
                }
            }
        }
    }
}

#endif