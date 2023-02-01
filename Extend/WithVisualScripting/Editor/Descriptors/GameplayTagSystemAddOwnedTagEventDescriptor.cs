
#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting.Editor
{
    [Descriptor(typeof(GameplayTagSystemAddOwnedTagEvent))]
    public sealed class GameplayTagSystemAddOwnedTagEventDescriptor : UnitDescriptor<GameplayTagSystemAddOwnedTagEvent>
    {
        public GameplayTagSystemAddOwnedTagEventDescriptor(GameplayTagSystemAddOwnedTagEvent target) : base(target)
        {
        }

        protected override EditorTexture DefaultIcon()
        {
            return GameplayTagSystemPathUtilityWithVisualScripting.Load("T_Icon_GameplayTag_Event_Add_D");
        }
        protected override EditorTexture DefinedIcon()
        {
            return GameplayTagSystemPathUtilityWithVisualScripting.Load("T_Icon_GameplayTag_Event_Add_D");
        }
    }
}

#endif