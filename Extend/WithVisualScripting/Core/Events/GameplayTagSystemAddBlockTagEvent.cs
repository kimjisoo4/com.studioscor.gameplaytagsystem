#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;
using UnityEngine;

namespace StudioScor.GameplayTagSystem.Extend.VisualScripting
{
    [UnitTitle("On Added BlockTag")]
    [UnitSubtitle("Event")]
    [UnitCategory("Events\\StudioScor\\GameplayTagSystem")]
    public class GameplayTagSystemAddBlockTagEvent : GameplayTagSystemEventUnit
    {
        protected override string HookName => GameplayTagSystemWithVisualScripting.ADD_BLOCK_TAG;
    }
}
#endif