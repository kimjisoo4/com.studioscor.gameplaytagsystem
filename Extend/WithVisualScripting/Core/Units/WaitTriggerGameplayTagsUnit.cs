#if SCOR_ENABLE_VISUALSCRIPTING
using System;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{


    [UnitTitle("Wait Trigger GameplayTags")]
    [UnitSubtitle("Task")]
    [UnitCategory("Time\\StudioScor\\GameplayTagSystem")]
    public class WaitTriggerGameplayTagsUnit : GameplayTagSystemWaitUnit
    {
        [DoNotSerialize]
        [PortLabel("Event")]
        public ControlOutput Event;

        [DoNotSerialize]
        [PortLabel("GameplayTags")]
        [PortLabelHidden]
        public ValueInput GameplayTags { get; private set; }

        private GameplayTagSystemComponent _GameplayTagSystemComponent;
        private GameplayTag[] _GameplayTags;
        private bool _Trigger;

        protected override void Definition()
        {
            base.Definition();

            Event = ControlOutput(nameof(Event));

            GameplayTags = ValueInput<GameplayTag[]>(nameof(GameplayTags), null);

            Succession(Enter, Event);
            Requirement(GameplayTags, Enter);
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
            _GameplayTags = flow.GetValue<GameplayTag[]>(GameplayTags);

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
            if (_GameplayTags.Contains(gameplayTag))
            {
                _Trigger = true;
            }
        }
    }
}

#endif