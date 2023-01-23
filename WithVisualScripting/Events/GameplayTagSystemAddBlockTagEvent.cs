#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    [UnitTitle("On Added BlockTag")]
    [UnitSubtitle("Event")]
    [UnitCategory("Events\\StudioScor\\GameplayTagSystem")]
    public class GameplayTagSystemAddBlockTagEvent : GameplayTagSystemEventUnit
    {
        protected override string hookName => GameplayTagSystemVisualScriptingEvent.ADD_BLOCK_TAG;
    }
}

#endif