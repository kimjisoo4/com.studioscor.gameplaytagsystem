#if SCOR_ENABLE_VISUALSCRIPTING
using System.Collections;
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    public abstract class WaitToggleGameplayTagUnit : GameplayTagSystemWaitUnit
    {

        [DoNotSerialize]
        [PortLabel("Toggle On")]
        public ControlOutput ToggleOn { get; private set; }

        [DoNotSerialize]
        [PortLabel("Toggle Off")]
        public ControlOutput ToggleOff { get; private set; }

        [DoNotSerialize]
        [PortLabel("Update")]
        public ControlOutput Update { get; private set; }

        [DoNotSerialize]
        [PortLabel("GameplayTags")]
        [PortLabelHidden]
        public ValueInput GameplayTags { get; private set; }
        
        [DoNotSerialize]
        [PortLabel("IsOn")]
        public ValueOutput IsToggleOn { get; private set; }

        protected GameplayTagSystemComponent _GameplayTagSystemComponent;
        protected GameplayTag[] _GameplayTags;
        protected bool _IsTrigger;
        protected bool _IsToggleOn;

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
                RemoveEvent();
            }

            _GameplayTagSystemComponent = flow.GetValue<GameplayTagSystemComponent>(GameplayTagSystemComponent);
            _GameplayTags = flow.GetValue<GameplayTag[]>(GameplayTags);

            AddEvent();

            TryCheckToggle();
            _IsTrigger = true;

            while (true)
            {
                if (_IsTrigger)
                {
                    _IsTrigger = false;

                    yield return _IsToggleOn ? ToggleOn : ToggleOff;
                }

                if (_IsToggleOn)
                {
                    yield return Update;
                }

                yield return null;
            }
        }

        protected abstract void TryCheckToggle();
        protected abstract void AddEvent();
        protected abstract void RemoveEvent();
    }
}

#endif