#if SCOR_ENABLE_VISUALSCRIPTING
using StudioScor.Utilities.VisualScripting;
using System;
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.Extend.VisualScripting
{
    public abstract class GameplayTagSystemEventUnit : CustomInterfaceEventUnit<IGameplayTagSystem, GameplayTag>
    {
        [DoNotSerialize]
        [PortLabel("GameplayTag")]
        [PortLabelHidden]
        public ValueOutput GameplayTag { get; private set; }

        [DoNotSerialize]
        [PortLabel("TargetTag")]
        [PortLabelHidden]
        public ValueInput TargetTag { get; private set; }

        [Serialize]
        [Inspectable]
        [UnitHeaderInspectable]
        [PortLabel("Trigger Type")]
        public ETriggerType TriggerType { get; private set; } = ETriggerType.TargetTag;

        private bool _UseTarget;

        public override Type MessageListenerType => typeof(GameplayTagSystemMessageListener);

        protected override void Definition()
        {
            base.Definition();

            GameplayTag = ValueOutput<GameplayTag>(nameof(GameplayTag));

            _UseTarget = TriggerType == ETriggerType.TargetTag;

            if (_UseTarget)
                TargetTag = ValueInput<GameplayTag>(nameof(TargetTag), null);
        }

        protected override void AssignArguments(Flow flow, GameplayTag gameplayTag)
        {
            flow.SetValue(GameplayTag, (object)gameplayTag);
        }

        protected override bool ShouldTrigger(Flow flow, GameplayTag gameplayTag)
        {
            return !_UseTarget || flow.GetValue<GameplayTag>(TargetTag) == gameplayTag;
        }
    }
}

#endif