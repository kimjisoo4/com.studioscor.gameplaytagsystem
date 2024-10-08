#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.Extend.VisualScripting
{
    [UnitTitle("On Subtracted BlockTag")]
    [UnitSubtitle("Event")]
    [UnitCategory("Events\\StudioScor\\GameplayTagSystem")]
    public class GameplayTagSystemSubtractBlockTagEvent : GameplayTagSystemEventUnit
    {
        protected override string HookName => GameplayTagSystemWithVisualScripting.SUBTRACT_BLOCK_TAG;
    }
}

#endif