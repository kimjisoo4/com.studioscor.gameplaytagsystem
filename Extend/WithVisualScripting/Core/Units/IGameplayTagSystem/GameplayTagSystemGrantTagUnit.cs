#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    [UnitTitle("Grant GameplayTag")]
    [UnitShortTitle("GrantTag")]
    [UnitCategory("StudioScor\\GameplayTagSystem")]
    public class GameplayTagSystemGrantTagUnit : GameplayTagSystemFlowUnit
    {
        [DoNotSerialize]
        [PortLabel("GameplayTag")]
        public ValueInput GameplayTag { get; private set; }

        [DoNotSerialize]
        [PortLabel("Type")]
        public ValueInput Type { get; private set; }

        [Serialize]
        [Inspectable]
        [InspectorToggleLeft]
        public bool UseList { get; set; } = false;

        protected override void Definition()
        {
            base.Definition();

            Type = ValueInput<EGameplayTagType>(nameof(Type), EGameplayTagType.Owned);

            if (UseList)
                GameplayTag = ValueInput<GameplayTag[]>(nameof(GameplayTag), null);
            else
                GameplayTag = ValueInput<GameplayTag>(nameof(GameplayTag), null);

            Requirement(GameplayTag, Enter);
        }

        protected override ControlOutput OnFlow(Flow flow)
        {
            var gameplayTagSystem = flow.GetValue<IGameplayTagSystem>(Target);
            var type = flow.GetValue<EGameplayTagType>(Type);

            if (UseList)
            {
                var grantTags = flow.GetValue<GameplayTag[]>(GameplayTag);

                switch (type)
                {
                    case EGameplayTagType.Owned:
                        gameplayTagSystem.AddOwnedTags(grantTags);
                        break;
                    case EGameplayTagType.Block:
                        gameplayTagSystem.AddBlockTags(grantTags);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                var grantTag = flow.GetValue<GameplayTag>(GameplayTag);

                switch (type)
                {
                    case EGameplayTagType.Owned:
                        gameplayTagSystem.AddOwnedTag(grantTag);
                        break;
                    case EGameplayTagType.Block:
                        gameplayTagSystem.AddBlockTag(grantTag);
                        break;
                    default:
                        break;
                }
            }

            return Exit;
        }
    }
}

#endif