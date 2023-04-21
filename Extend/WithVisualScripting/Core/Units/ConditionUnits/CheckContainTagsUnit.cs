#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    [UnitTitle("Check Contain Tags")]
    [UnitSubtitle("GameplayTagSystem Condition")]
    [UnitCategory("StudioScor\\GameplayTagSystem")]
    public class CheckContainTagsUnit : GameplayTagSystemUnit
    {
        [Serialize]
        [Inspectable]
        [UnitHeaderInspectable]
        [PortLabel("Container Type")]
        public EContainerType ContainerType { get; private set; }

        [Serialize]
        [Inspectable]
        [UnitHeaderInspectable]
        [PortLabel("Contain Type")]
        public EContainType ContainType { get; private set; }

        [DoNotSerialize]
        [PortLabel("GameplayTag")]
        public ValueInput GameplayTags { get; private set; }

        [DoNotSerialize]
        [PortLabel("IsContain")]
        [PortLabelHidden]
        public ValueOutput IsContaine;

        protected override void Definition()
        {
            base.Definition();

            GameplayTags = ValueInput<GameplayTag[]>(nameof(GameplayTags), null);

            IsContaine = ValueOutput<bool>(nameof(IsContaine), CheckGameplayTags);

            Requirement(Target, IsContaine);
            Requirement(GameplayTags, IsContaine);
        }

        private bool CheckGameplayTags(Flow flow)
        {
            var gameplayTagSystem = flow.GetValue<IGameplayTagSystem>(Target);
            var tags = flow.GetValue<GameplayTag[]>(GameplayTags);

            if (tags is null || tags.Length <= 0)
                return false;

            switch (ContainerType)
            {
                case EContainerType.Owned:
                    return ContainType.Equals(EContainType.All) ? gameplayTagSystem.ContainAllTagsInOwned(tags) : gameplayTagSystem.ContainAnyTagsInOwned(tags);
                case EContainerType.Block:
                    return ContainType.Equals(EContainType.All) ? gameplayTagSystem.ContainAllTagsInBlock(tags) : gameplayTagSystem.ContainAnyTagsInBlock(tags);
                default:
                    return false;
            }
        }
    }
}
#endif