#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem
{
    [UnitTitle("OnSubtractBlockTagEvent")]
    [UnitCategory("Events\\StudioScor\\GameplayTagSystem")]
    public class GameplayTagSystemSubtractBlockTagEvent : GameplayTagSystemCustomUnitEvent
    {
        protected override string EventName => GameplayTagSystemVisualScriptingEvent.SUBTRACT_BLOCK_TAG;
    }
}

#endif