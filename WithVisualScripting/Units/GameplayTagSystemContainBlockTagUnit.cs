#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{

    [UnitTitle("Contain BlockTag")]
    [UnitSubtitle("GameplayTagSystemComponent")]
    [UnitCategory("StudioScor\\GameplayTagSystem")]
    public class GameplayTagSystemContainBlockTagUnit : GameplayTagSystemHasTagUnit
    {
        protected override bool ContainTag(GameplayTagSystemComponent gameplayTagSystemComponent, GameplayTag gameplayTag)
        {
            return gameplayTagSystemComponent.ContainBlockTag(gameplayTag);
        }
    }
}

#endif