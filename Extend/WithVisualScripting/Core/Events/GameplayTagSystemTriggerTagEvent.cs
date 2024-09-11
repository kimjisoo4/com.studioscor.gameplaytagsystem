#if SCOR_ENABLE_VISUALSCRIPTING
using StudioScor.Utilities.VisualScripting;
using System;
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{

    [UnitTitle("On Trigger GameplayTag")]
    [UnitSubtitle("Event")]
    [UnitCategory("Events\\StudioScor\\GameplayTagSystem")]
    public class GameplayTagSystemTriggerTagEvent : CustomInterfaceEventUnit<IGameplayTagSystem, TriggerTagData>
    {
        [DoNotSerialize]
        [PortLabel("GameplayTag")]
        [PortLabelHidden]
        public ValueOutput GameplayTag { get; private set; }

        [DoNotSerialize]
        [PortLabel("EventData")]
        [PortLabelHidden]
        public ValueOutput EventData { get; private set; }

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
        protected override string HookName => GameplayTagSystemWithVisualScripting.TRIGGER_TAG;
        public override Type MessageListenerType => typeof(GameplayTagSystemMessageListener);

        protected override void Definition()
        {
            base.Definition();

            _UseTarget = TriggerType == ETriggerType.TargetTag;

            if(!_UseTarget)
            {
                GameplayTag = ValueOutput<GameplayTag>(nameof(GameplayTag));
            }
            
            EventData = ValueOutput<object>(nameof(Data));


            if (_UseTarget)
            {
                TargetTag = ValueInput<GameplayTag>(nameof(TargetTag), null);
            }
        }

        protected override void AssignArguments(Flow flow, TriggerTagData triggerTagData)
        {
            if(!_UseTarget)
            {
                flow.SetValue(GameplayTag, triggerTagData.TriggerTag);
            }
            
            flow.SetValue(EventData, triggerTagData.Data);
        }

        protected override bool ShouldTrigger(Flow flow, TriggerTagData triggerTagData)
        {
            return !_UseTarget || flow.GetValue<GameplayTag>(TargetTag) == triggerTagData.TriggerTag;
        }
    }
}

#endif