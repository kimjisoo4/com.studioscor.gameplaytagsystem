#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    [UnitTitle("Add Block GameplayTag")]
    [UnitSubtitle("GameplayTagSystemComponent")]
    [UnitCategory("StudioScor\\GameplayTagSystem")]
    public class GameplayTagSystemAddBlockTagUnit : GameplayTagSystemSingleTagUnit
    {

        protected override ControlOutput OnFlow(Flow flow)
        {
            var gameplayTagSystemComponent = flow.GetValue<GameplayTagSystemComponent>(GameplayTagSystemComponent);
            var gameplayTag = flow.GetValue<GameplayTag>(GameplayTag);

            gameplayTagSystemComponent.AddBlockTag(gameplayTag);

            return Exit;
        }
    }
}

#endif