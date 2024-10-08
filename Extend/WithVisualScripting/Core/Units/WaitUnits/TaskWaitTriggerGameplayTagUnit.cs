#if SCOR_ENABLE_VISUALSCRIPTING
using System;
using System.Linq;
using Unity.VisualScripting;
using StudioScor.Utilities.VisualScripting;

namespace StudioScor.GameplayTagSystem.Extend.VisualScripting
{
    [UnitTitle("Task Wait Trigger GameplayTag")]
    [UnitShortTitle("TaskWaitTriggerTag")]
    [UnitSubtitle("GameplayTagSystem Task")]
    [UnitCategory("StudioScor\\Task\\GameplayTagSystem")]
    public class TaskWaitTriggerGameplayTagUnit : WaitTriggerEventUnit<IGameplayTagSystem, GameplayTagSO>
    {
        [DoNotSerialize]
        [PortLabel("GameplayTag")]
        [PortLabelHidden]
        public ValueInput GameplayTag;

        [Serialize]
        [Inspectable]
        [UnitHeaderInspectable]
        [PortLabel("Structure Type")]
        public EStructureType StructureType { get; set; } = EStructureType.Target;

        private bool useList;

        public override string HookName => GameplayTagSystemWithVisualScripting.TRIGGER_TAG;
        public override Type MessageListenerType => typeof(GameplayTagSystemMessageListener);

        public new class Data : WaitTriggerEventUnit<IGameplayTagSystem, GameplayTagSO>.Data
        {
            public GameplayTagSO GameplayTag;
            public GameplayTagSO[] GameplayTags;
        }
        public override IGraphElementData CreateData()
        {
            return new Data();
        }

        protected override void Definition()
        {
            base.Definition();

            useList = StructureType.Equals(EStructureType.List);

            if (useList)
            {
                GameplayTag = ValueInput<GameplayTagSO[]>(nameof(GameplayTag), null);
            }
            else
            {
                GameplayTag = ValueInput<GameplayTagSO>(nameof(GameplayTag), null);
            }
            
        }

        protected override void SetData(Flow flow)
        {
            var data = flow.stack.GetElementData<Data>(this);

            if (useList)
            {
                data.GameplayTags = flow.GetValue<GameplayTagSO[]>(GameplayTag);
            }
            else
            {
                data.GameplayTag = flow.GetValue<GameplayTagSO>(GameplayTag);
            }

            
        }
        protected override bool ShouldTrigger(Flow flow, GameplayTagSO gameplayTag)
        {
            var data = flow.stack.GetElementData<Data>(this);

            if (!data.IsActivate)
                return false;

            if (useList)
            {
                if (data.GameplayTags.Contains(gameplayTag))
                    return false;
            }
            else
            {
                if (data.GameplayTag != gameplayTag)
                    return false;
            }
            

            return true;
        }

        
    }
}

#endif