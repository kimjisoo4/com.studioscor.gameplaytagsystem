#if SCOR_ENABLE_VISUALSCRIPTING
using System;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    [UnitTitle("Wait Contain All OwnedTags")]
    [UnitSubtitle("GameplayTagSystem Task")]
    [UnitCategory("Time\\StudioScor\\GameplayTagSystem")]
    public class WaitOwnedGameplayTagUnit : GameplayTagSystemWaitUnit
    {
        [DoNotSerialize]
        [PortLabel("Toggle On")]
        public ControlOutput ToggleOn;

        [DoNotSerialize]
        [PortLabel("Toggle Off")]
        public ControlOutput ToggleOff;

        [DoNotSerialize]
        [PortLabel("Update")]
        public ControlOutput Update;

        [DoNotSerialize]
        [PortLabel("GameplayTags")]
        [PortLabelHidden]
        public ValueInput GameplayTags { get; private set; }

        [DoNotSerialize]
        [PortLabel("IsOn")]
        public ValueOutput IsToggleOn { get; private set; }

        private GameplayTagSystemComponent _GameplayTagSystemComponent;
        private GameplayTag[] _GameplayTags;
        private bool _IsTrigger;
        private bool _IsToggleOn;

        protected override void Definition()
        {
            base.Definition();

            ToggleOn = ControlOutput(nameof(ToggleOn));
            ToggleOff = ControlOutput(nameof(ToggleOff));
            Update = ControlOutput(nameof(Update));

            GameplayTags = ValueInput<GameplayTag[]>(nameof(GameplayTags), null);
            
            IsToggleOn = ValueOutput<bool>(nameof(IsToggleOn), (Flow) => { return _IsToggleOn; });

            Succession(Enter, ToggleOn);
            Succession(Enter, ToggleOff);
            Succession(Enter, Update);

            Requirement(GameplayTags, Enter);
            
            Assignment(Enter, IsToggleOn);
        }

        protected override IEnumerator Await(Flow flow)
        {
            yield return Exit;

            if (_GameplayTagSystemComponent)
            {
                _GameplayTagSystemComponent.OnGrantedOwnedTag -= WaitGameplayTagUnit_OnChangedOwnedTag;
                _GameplayTagSystemComponent.OnRemovedOwnedTag -= WaitGameplayTagUnit_OnChangedOwnedTag;
            }

            _GameplayTagSystemComponent = flow.GetValue<GameplayTagSystemComponent>(GameplayTagSystemComponent);
            _GameplayTags = flow.GetValue<GameplayTag[]>(GameplayTags);

            _GameplayTagSystemComponent.OnGrantedOwnedTag += WaitGameplayTagUnit_OnChangedOwnedTag;
            _GameplayTagSystemComponent.OnRemovedOwnedTag += WaitGameplayTagUnit_OnChangedOwnedTag;

            _IsToggleOn = _GameplayTagSystemComponent.ContainAllTagsInOwned(_GameplayTags);
            _IsTrigger = true;

            while (true)
            {
                if (_IsTrigger)
                {
                    _IsTrigger = false;

                    yield return _IsToggleOn ? ToggleOn : ToggleOff;
                }

                if(_IsToggleOn)
                {
                    yield return Update;
                }

                yield return null;
            }
        }

        private void WaitGameplayTagUnit_OnChangedOwnedTag(GameplayTagSystemComponent gameplayTagSystemComponent, GameplayTag gameplayTag)
        {
            if (_GameplayTags.Contains(gameplayTag))
            {
                bool containAllTags = gameplayTagSystemComponent.ContainAllTagsInOwned(_GameplayTags);

                if (containAllTags != _IsToggleOn)
                {
                    _IsTrigger = true;

                    _IsToggleOn = containAllTags;
                }
            }
        }
    }


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