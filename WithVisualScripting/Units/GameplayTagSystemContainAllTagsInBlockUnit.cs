#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    [UnitTitle("Contain All BlockTags")]
    [UnitSubtitle("GameplayTagSystemComponent")]
    [UnitCategory("StudioScor\\GameplayTagSystem")]
    public class GameplayTagSystemContainAllTagsInBlockUnit : GameplayTagSystemHasAllTagsUnit
    {
        protected override bool ContainTag(GameplayTagSystemComponent gameplayTagComponent, GameplayTag[] gameplayTags)
        {
            return gameplayTagComponent.ContainAllTagsInBlock(gameplayTags);
        }
    }
}

#endif