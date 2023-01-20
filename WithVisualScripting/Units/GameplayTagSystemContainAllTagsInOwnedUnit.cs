#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    [UnitTitle("Contain All OwnedTags")]
    [UnitSubtitle("GameplayTagSystemComponent")]
    [UnitCategory("StudioScor\\GameplayTagSystem")]
    public class GameplayTagSystemContainAllTagsInOwnedUnit : GameplayTagSystemHasAllTagsUnit
    {
        protected override bool ContainTag(GameplayTagSystemComponent gameplayTagComponent, GameplayTag[] gameplayTags)
        {
            return gameplayTagComponent.ContainAllTagsInOwned(gameplayTags);
        }
    }
}

#endif