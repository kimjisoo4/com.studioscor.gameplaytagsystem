#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    [UnitTitle("Contain OwnedTag")]
    [UnitSubtitle("GameplayTagSystemComponent")]
    [UnitCategory("StudioScor\\GameplayTagSystem")]
    public class GameplayTagSystemContainOwnedTagUnit : GameplayTagSystemHasTagUnit
    {
        protected override bool ContainTag(GameplayTagSystemComponent gameplayTagComponent, GameplayTag gameplayTag)
        {
            return gameplayTagComponent.ContainOwnedTag(gameplayTag);
        }
    }
}

#endif