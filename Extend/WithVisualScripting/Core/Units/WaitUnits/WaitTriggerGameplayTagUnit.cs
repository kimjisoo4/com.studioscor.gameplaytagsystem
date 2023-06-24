#if SCOR_ENABLE_VISUALSCRIPTING
using System;
using System.Linq;
using Unity.VisualScripting;
using StudioScor.Utilities.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    [UnitTitle("Task Wait Trigger GameplayTag")]
    [UnitShortTitle("TaskWaitTriggerTag")]
    [UnitSubtitle("GameplayTagSystem Task")]
    [UnitCategory("StudioScor\\Task")]
    public class WaitTriggerGameplayTagUnit : WaitTriggerEventUnit<IGameplayTagSystemEvent, GameplayTag>
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

        public new class Data : WaitTriggerEventUnit<IGameplayTagSystemEvent, GameplayTag>.Data
        {
            public GameplayTag GameplayTag;
            public GameplayTag[] GameplayTags;
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
                GameplayTag = ValueInput<GameplayTag[]>(nameof(GameplayTag), null);
            }
            else
            {
                GameplayTag = ValueInput<GameplayTag>(nameof(GameplayTag), null);
            }
            
        }

        protected override void SetData(Flow flow)
        {
            var data = flow.stack.GetElementData<Data>(this);

            if (useList)
            {
                data.GameplayTags = flow.GetValue<GameplayTag[]>(GameplayTag);
            }
            else
            {
                data.GameplayTag = flow.GetValue<GameplayTag>(GameplayTag);
            }

            
        }
        protected override bool ShouldTrigger(Flow flow, GameplayTag gameplayTag)
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