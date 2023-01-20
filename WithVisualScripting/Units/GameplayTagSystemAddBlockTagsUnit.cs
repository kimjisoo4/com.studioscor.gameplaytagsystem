#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    [UnitTitle("Add Block GameplayTags")]
    [UnitSubtitle("GameplayTagSystemComponent")]
    [UnitCategory("StudioScor\\GameplayTagSystem")]
    public class GameplayTagSystemAddBlockTagsUnit : GameplayTagSystemMultiTagUnit
    {
        protected override ControlOutput OnFlow(Flow flow)
        {
            var gameplayTagSystemComponent = flow.GetValue<GameplayTagSystemComponent>(GameplayTagSystemComponent);
            var gameplayTags = flow.GetValue<GameplayTag[]>(GameplayTags);

            gameplayTagSystemComponent.AddBlockTags(gameplayTags);

            return Exit;
        }
    }
}

#endif