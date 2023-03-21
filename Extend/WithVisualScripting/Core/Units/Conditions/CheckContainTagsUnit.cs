#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    [UnitTitle("Check Contain Tags")]
    [UnitSubtitle("GameplayTagSystem")]
    [UnitCategory("StudioScor\\GameplayTagSystem")]
    public class CheckContainTagsUnit : GameplayTagSystemUnit
    {
        [DoNotSerialize]
        [PortLabel("GameplayType")]
        public ValueInput Type { get; private set; }

        [DoNotSerialize]
        [PortLabel("ContainType")]
        public ValueInput ContainType { get; private set; }

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

            Type = ValueInput<EGameplayTagType>(nameof(Type), EGameplayTagType.Owned);
            ContainType = ValueInput<EContainType>(nameof(ContainType), EContainType.All);
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
            
            var type = flow.GetValue<EGameplayTagType>(Type);
            var containType = flow.GetValue<EContainType>(ContainType);

            switch (type)
            {
                case EGameplayTagType.Owned:
                    return containType.Equals(EContainType.All) ? gameplayTagSystem.ContainAllTagsInOwned(tags) : gameplayTagSystem.ContainAnyTagsInOwned(tags);
                case EGameplayTagType.Block:
                    return containType.Equals(EContainType.All) ? gameplayTagSystem.ContainAllTagsInBlock(tags) : gameplayTagSystem.ContainAnyTagsInBlock(tags);
                default:
                    return false;
            }
        }
    }
}
#endif