#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    [UnitTitle("Check Contain Tag")]
    [UnitSubtitle("GameplayTagSystem")]
    [UnitCategory("StudioScor\\GameplayTagSystem")]
    public class CheckContainTagUnit : GameplayTagSystemUnit
    {
        [DoNotSerialize]
        [PortLabel("Type")]
        public ValueInput Type { get; private set; }

        [DoNotSerialize]
        [PortLabel("ContainType")]
        public ValueInput ContainType { get; private set; }

        [DoNotSerialize]
        [PortLabel("GameplayTag")]
        public ValueInput GameplayTag { get; private set; }

        [DoNotSerialize]
        [PortLabel("IsContain")]
        [PortLabelHidden]
        public ValueOutput IsContaine;

        [Serialize]
        [Inspectable]
        [InspectorToggleLeft]
        public bool UseList { get; set; } = true;

        protected override void Definition()
        {
            base.Definition();

            Type = ValueInput<EGameplayTagType>(nameof(Type), EGameplayTagType.Owned);

            if(UseList)
            {
                ContainType = ValueInput<EContainType>(nameof(ContainType), EContainType.All);
                GameplayTag = ValueInput<GameplayTag[]>(nameof(GameplayTag), null);
            }
            else
            {
                GameplayTag = ValueInput<GameplayTag>(nameof(GameplayTag), null);
            }
            

            IsContaine = ValueOutput<bool>(nameof(IsContaine), CheckGameplayTags);

            Requirement(Target, IsContaine);
            Requirement(GameplayTag, IsContaine);
        }

        private bool CheckGameplayTags(Flow flow)
        {
            var gameplayTagSystem = flow.GetValue<IGameplayTagSystem>(Target);

            if (UseList)
            {
                var tags = flow.GetValue<GameplayTag[]>(GameplayTag);

                if (tags is null || tags.Length <= 0)
                    return false;

                var gameplayTagType = flow.GetValue<EGameplayTagType>(Type);
                var containType = flow.GetValue<EContainType>(ContainType);

                switch (gameplayTagType)
                {
                    case EGameplayTagType.Owned:
                        return containType.Equals(EContainType.All) ? gameplayTagSystem.ContainAllTagsInOwned(tags) : gameplayTagSystem.ContainAnyTagsInOwned(tags);
                    case EGameplayTagType.Block:
                        return containType.Equals(EContainType.All) ? gameplayTagSystem.ContainAllTagsInBlock(tags) : gameplayTagSystem.ContainAnyTagsInBlock(tags);
                    default:
                        return false;
                }
            }
            else
            {
                var tag = flow.GetValue<GameplayTag>(GameplayTag);

                if (!tag)
                    return false;

                var gameplayTagType = flow.GetValue<EGameplayTagType>(Type);

                switch (gameplayTagType)
                {
                    case EGameplayTagType.Owned:
                        return gameplayTagSystem.ContainOwnedTag(tag);
                    case EGameplayTagType.Block:
                        return gameplayTagSystem.ContainBlockTag(tag);
                    default:
                        return false;
                }
            } 
        }
    }
}
#endif