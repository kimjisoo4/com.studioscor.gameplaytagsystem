#if SCOR_ENABLE_VISUALSCRIPTING
using System;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using StudioScor.Utilities.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    [UnitTitle("Task Contain GameplayTag")]
    [UnitShortTitle("TaskContainTag")]
    [UnitSubtitle("GameplayTagSystem Task")]
    [UnitCategory("StudioScor\\Task")]
    public class TaskContainGameplayTagUnit : UpdateUnit
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

        private bool useList;

        public new class Data : UpdateUnit.Data
        {
            public IGameplayTagSystemEvent GameplayTagSystemEvent;
            public IGameplayTagSystem GameplayTagSystem;
            public bool UseList;
            public EContainerType GameplayTagType;
            public EContainType ContainType;
            public GameplayTag GameplayTag;
            public GameplayTag[] GameplayTags;
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

            useList = StructureType.Equals(EStructureType.List);

            if (useList)
            {
                ContainType = ValueInput<EContainType>(nameof(ContainType), EContainType.All);
                GameplayTag = ValueInput<GameplayTag[]>(nameof(GameplayTag), null);
            }
            else
            {
                GameplayTag = ValueInput<GameplayTag>(nameof(GameplayTag), null);
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

        protected override void OnEnter(Flow flow)
        {
            var data = flow.stack.GetElementData<Data>(this);

            var target = flow.GetValue<GameObject>(Target);
            var gameplayTagSystem = flow.GetValue<IGameplayTagSystem>(Target);
            var gameplayTagSystemEvent = flow.GetValue<IGameplayTagSystemEvent>(Target);

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

            Action<GameplayTag> grantHandler = tag => GrantTag(reference, tag);
            Action<GameplayTag> removeHandler = tag => RemoveTag(reference, tag);

            EventBus.Register(grantHook, grantHandler);
            EventBus.Register(removeHook, removeHandler);

            data.GrantHook = grantHook;
            data.RemoveHook = removeHook;

            data.GrantHandler = grantHandler;
            data.RemoveHandler = removeHandler;

            data.GameplayTagSystem = gameplayTagSystem;
            data.GameplayTagSystemEvent = gameplayTagSystemEvent;
            data.GameplayTagType = ContainerType;

            data.UseList = useList;

            if (useList)
            {
                var gameplayTags = flow.GetValue<GameplayTag[]>(GameplayTag);

                data.GameplayTag = null;
                data.GameplayTags = gameplayTags;
                data.ContainType = flow.GetValue<EContainType>(ContainType);
            }
            else
            {
                var gameplayTag = flow.GetValue<GameplayTag>(GameplayTag);

                data.GameplayTag = gameplayTag;
                data.GameplayTags = null;
            }
        }

        protected override void OnInterrupt(Flow flow)
        {
            var data = flow.stack.GetElementData<Data>(this);

            if (!data.IsActivate)
                return;

            data.IsActivate = false;

        }

        protected override ControlOutput OnManualUpdate(Flow flow)
        {
            var data = flow.stack.GetElementData<Data>(this);

            if (data.IsOn)
            {
                flow.Invoke(Update);
            }

            flow.Dispose();

            return null;
        }

        protected override void OnUpdate(Flow flow)
        {
            var data = flow.stack.GetElementData<Data>(this);

            if(data.IsOn)
            {
                flow.Invoke(Update);
            }

            flow.Dispose();
        }

        private void GrantTag(GraphReference reference, GameplayTag tag)
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
        private void RemoveTag(GraphReference reference, GameplayTag tag)
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