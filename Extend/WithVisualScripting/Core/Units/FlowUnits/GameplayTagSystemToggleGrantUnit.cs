#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.Extend.VisualScripting
{
    [UnitTitle("Toggle Grant GameplayTags")]
    [UnitSubtitle("GameplayTagSystem Unit")]
    [UnitCategory("StudioScor\\GameplayTagSystem")]
    public class GameplayTagSystemToggleGrantUnit : GameplayTagSystemUnit
    {
        [DoNotSerialize]
        [PortLabel("On")]
        public ControlInput ToggleOn;

        [DoNotSerialize]
        [PortLabel("Off")]
        public ControlInput ToggleOff;

        [DoNotSerialize]
        [PortLabel("Turn On")]
        public ControlOutput TurnOn;

        [DoNotSerialize]
        [PortLabel("Turn Off")]
        public ControlOutput TurnOff;

        [DoNotSerialize]
        [PortLabel("OwnedTags")]
        [AllowsNull]
        public ValueInput OwnedTags;

        [DoNotSerialize]
        [PortLabel("BlockTags")]
        [AllowsNull]
        public ValueInput BlockTags;

        [DoNotSerialize]
        [PortLabel("IsOn")]
        public ValueOutput IsOn;


        protected override void Definition()
        {
            base.Definition();

            ToggleOn = ControlInput(nameof(ToggleOn), OnToggle);
            ToggleOff = ControlInput(nameof(ToggleOff), OffToggle);
            TurnOn = ControlOutput(nameof(TurnOn));
            TurnOff = ControlOutput(nameof(TurnOff));

            OwnedTags = ValueInput<GameplayTag[]>(nameof(OwnedTags), null).AllowsNull();
            BlockTags = ValueInput<GameplayTag[]>(nameof(BlockTags), null).AllowsNull();

            IsOn = ValueOutput<bool>(nameof(IsOn));

            Succession(ToggleOn, TurnOn);
            Succession(ToggleOff, TurnOff);

            Requirement(Target, ToggleOn);
        }

        private ControlOutput OnToggle(Flow flow)
        {
            var gameplayTagSystem = flow.GetValue<IGameplayTagSystem>(Target);

            var ownedTags = flow.GetValue<GameplayTag[]>(OwnedTags);
            var blockTags = flow.GetValue<GameplayTag[]>(BlockTags);

            gameplayTagSystem.AddOwnedTags(ownedTags);
            gameplayTagSystem.AddBlockTags(blockTags);

            flow.SetValue(IsOn, true);

            return TurnOn;
        }

        private ControlOutput OffToggle(Flow flow)
        {
            var gameplayTagSystem = flow.GetValue<IGameplayTagSystem>(Target);

            var ownedTags = flow.GetValue<GameplayTag[]>(OwnedTags);
            var blockTags = flow.GetValue<GameplayTag[]>(BlockTags);

            gameplayTagSystem.RemoveOwnedTags(ownedTags);
            gameplayTagSystem.RemoveBlockTags(blockTags);

            flow.SetValue(IsOn, false);

            return TurnOff;
        }
    }
}

#endif