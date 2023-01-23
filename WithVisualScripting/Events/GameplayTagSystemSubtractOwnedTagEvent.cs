#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    [UnitTitle("On Subtracted OwnedTag")]
    [UnitSubtitle("Event")]
    [UnitCategory("Events\\StudioScor\\GameplayTagSystem")]
    public class GameplayTagSystemSubtractOwnedTagEvent : GameplayTagSystemEventUnit
    {
        protected override string hookName => GameplayTagSystemVisualScriptingEvent.SUBTRACT_OWNED_TAG;
    }
}

#endif