#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    [UnitTitle("Contain Any BlockTags")]
    [UnitSubtitle("GameplayTagSystemComponent")]
    [UnitCategory("StudioScor\\GameplayTagSystem")]
    public class GameplayTagSystemContainAnyTagsInBlockUnit : GameplayTagSystemHasAllTagsUnit
    {
        protected override bool ContainTag(GameplayTagSystemComponent gameplayTagComponent, GameplayTag[] gameplayTags)
        {
            return gameplayTagComponent.ContainAnyTagsInBlock(gameplayTags);
        }
    }
}

#endif