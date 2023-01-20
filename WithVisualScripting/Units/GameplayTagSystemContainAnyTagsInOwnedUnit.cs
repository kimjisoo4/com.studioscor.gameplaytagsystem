#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    [UnitTitle("Contain Any OwnedTags")]
    [UnitSubtitle("GameplayTagSystemComponent")]
    [UnitCategory("StudioScor\\GameplayTagSystem")]
    public class GameplayTagSystemContainAnyTagsInOwnedUnit : GameplayTagSystemHasAllTagsUnit
    {
        protected override bool ContainTag(GameplayTagSystemComponent gameplayTagComponent, GameplayTag[] gameplayTags)
        {
            return gameplayTagComponent.ContainAnyTagsInOwned(gameplayTags);
        }
    }
}

#endif