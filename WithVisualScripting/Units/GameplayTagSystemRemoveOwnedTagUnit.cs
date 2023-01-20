#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    [UnitTitle("Remove Owned GameplayTag")]
    [UnitSubtitle("GameplayTagSystemComponent")]
    [UnitCategory("StudioScor\\GameplayTagSystem")]
    public class GameplayTagSystemRemoveOwnedTagUnit : GameplayTagSystemSingleTagUnit
    {
        protected override ControlOutput OnFlow(Flow flow)
        {
            var gameplayTagSystemComponent = flow.GetValue<GameplayTagSystemComponent>(GameplayTagSystemComponent);
            var gameplayTag = flow.GetValue<GameplayTag>(GameplayTag);

            gameplayTagSystemComponent.RemoveOwnedTag(gameplayTag);

            return Exit;
        }
    }
}

#endif