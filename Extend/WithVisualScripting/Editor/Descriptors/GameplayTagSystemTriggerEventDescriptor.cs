
#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting.Editor
{

    [Descriptor(typeof(GameplayTagSystemTriggerTagEvent))]
    public sealed class GameplayTagSystemTriggerEventDescriptor : UnitDescriptor<GameplayTagSystemTriggerTagEvent>
    {
        public GameplayTagSystemTriggerEventDescriptor(GameplayTagSystemTriggerTagEvent target) : base(target)
        {
        }

        protected override EditorTexture DefaultIcon()
        {
            return GameplayTagSystemPathUtilityWithVisualScripting.Load("T_Icon_GameplayTag_Event_D");
        }
        protected override EditorTexture DefinedIcon()
        {
            return GameplayTagSystemPathUtilityWithVisualScripting.Load("T_Icon_GameplayTag_Event_D");
        }
    }
}

#endif