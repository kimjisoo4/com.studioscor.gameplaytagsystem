#if SCOR_ENABLE_VISUALSCRIPTING
using System;
using System.Linq;
using Unity.VisualScripting;
using StudioScor.Utilities.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{

    [UnitTitle("Wait Trigger GameplayTag")]
    [UnitShortTitle("WaitTriggerTag")]
    [UnitSubtitle("GameplayTagSystem Task")]
    [UnitCategory("Time\\StudioScor\\GameplayTagSystem")]
    public class WaitTriggerGameplayTagUnit : WaitTriggerEventUnit<IGameplayTagSystemEvent, GameplayTag>
    {
        [DoNotSerialize]
        [PortLabel("GameplayTag")]
        public ValueInput GameplayTag;

        [Serialize]
        [Inspectable]
        [InspectorToggleLeft]
        public bool UseList { get; set; } = false;

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

            if(UseList)
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

            if (UseList)
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

            if (UseList)
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