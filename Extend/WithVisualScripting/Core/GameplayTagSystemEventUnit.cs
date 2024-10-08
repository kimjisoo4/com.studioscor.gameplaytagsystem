#if SCOR_ENABLE_VISUALSCRIPTING
using StudioScor.Utilities.VisualScripting;
using System;
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.Extend.VisualScripting
{
    public abstract class GameplayTagSystemEventUnit : CustomInterfaceEventUnit<IGameplayTagSystem, IGameplayTag>
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

            GameplayTag = ValueOutput<GameplayTagSO>(nameof(GameplayTag));

            _UseTarget = TriggerType == ETriggerType.TargetTag;

            if (_UseTarget)
                TargetTag = ValueInput<GameplayTagSO>(nameof(TargetTag), null);
        }

        protected override void AssignArguments(Flow flow, IGameplayTag gameplayTag)
        {
            flow.SetValue(GameplayTag, gameplayTag);
        }

        protected override bool ShouldTrigger(Flow flow, IGameplayTag gameplayTag)
        {
            return !_UseTarget || flow.GetValue<GameplayTagSO>(TargetTag) == gameplayTag;
        }
    }
}

#endif