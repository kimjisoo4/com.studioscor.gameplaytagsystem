#if SCOR_ENABLE_VISUALSCRIPTING
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    [UnitTitle("Wait Trigger GameplayTag")]
    [UnitSubtitle("GameplayTagSystem Task")]
    [UnitCategory("Time\\StudioScor\\GameplayTagSystem")]
    public class WaitTriggerGameplayTagUnit : GameplayTagSystemWaitUnit
    {
        [DoNotSerialize]
        [PortLabel("Event")]
        public ControlOutput Event; 

        [DoNotSerialize]
        [PortLabel("GameplayTag")]
        [PortLabelHidden]
        public ValueInput GameplayTag { get; private set; }

        private GameplayTagSystemComponent _GameplayTagSystemComponent;
        private GameplayTag _GameplayTag;
        private bool _Trigger;

        protected override void Definition()
        {
            base.Definition();

            Event = ControlOutput(nameof(Event));

            GameplayTag = ValueInput<GameplayTag>(nameof(GameplayTag), null);

            Succession(Enter, Event);       
            Requirement(GameplayTag, Enter);
        }

        protected override IEnumerator Await(Flow flow)
        {
            yield return Exit;

            if (_GameplayTagSystemComponent)
            {
                _GameplayTagSystemComponent.OnTriggeredTag -= WaitGameplayTagUnit_OnTriggeredTag;
            }

            _GameplayTagSystemComponent = flow.GetValue<GameplayTagSystemComponent>(GameplayTagSystemComponent);
            _GameplayTagSystemComponent.OnTriggeredTag += WaitGameplayTagUnit_OnTriggeredTag;
            _GameplayTag = flow.GetValue<GameplayTag>(GameplayTag);

            while (true)
            {
                if (_Trigger)
                {
                    _Trigger = false;

                    yield return Event;
                }

                yield return null;  
            }
        }

        private void WaitGameplayTagUnit_OnTriggeredTag(GameplayTagSystemComponent gameplayTagSystemComponent, GameplayTag gameplayTag)
        {
            if (_GameplayTag == gameplayTag)
            {
                _Trigger = true;
            }
        }
    }
}

#endif