#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    [UnitTitle("Check Condition Tags")]
    [UnitSubtitle("GameplayTagSystem")]
    [UnitCategory("StudioScor\\GameplayTagSystem")]
    public class CheckConditionTagsUnit : GameplayTagSystemUnit
    {
        [DoNotSerialize]
        [PortLabel("ObstacledTags")]
        public ValueInput ObstacledTags { get; private set; }

        [DoNotSerialize]
        [PortLabel("RequiredTags")]
        public ValueInput RequiredTags { get; private set; }

        [DoNotSerialize]
        [PortLabel("Condition")]
        [PortLabelHidden]
        public ValueOutput IsPositive;

        protected override void Definition()
        {
            base.Definition();

            ObstacledTags = ValueInput<GameplayTag[]>(nameof(ObstacledTags), default);
            RequiredTags = ValueInput<GameplayTag[]>(nameof(RequiredTags), default);

            IsPositive = ValueOutput<bool>(nameof(IsPositive), CheckGameplayTags);

            Requirement(Target, IsPositive);
            Requirement(ObstacledTags, IsPositive);
            Requirement(RequiredTags, IsPositive);
        }

        private bool CheckGameplayTags(Flow flow)
        {
            var gameplayTagSystem = flow.GetValue<IGameplayTagSystem>(Target);

            var obstacledTags = flow.GetValue<GameplayTag[]>(ObstacledTags);

            if (obstacledTags.Length > 0 && gameplayTagSystem.ContainAnyTagsInOwned(obstacledTags))
                return false;

            var requiredTags = flow.GetValue<GameplayTag[]>(RequiredTags);

            if (requiredTags.Length > 0 && !gameplayTagSystem.ContainAllTagsInOwned(requiredTags))
                return false;

            return true;
        }
    }
}
#endif