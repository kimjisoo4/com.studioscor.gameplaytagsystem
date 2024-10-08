
#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.Extend.VisualScripting.Editor
{
    [Descriptor(typeof(GameplayTagSystemGrantOwnedTagEvent))]
    public sealed class GameplayTagSystemAddNewOwnedTagEventDescriptor : UnitDescriptor<GameplayTagSystemGrantOwnedTagEvent>
    {
        public GameplayTagSystemAddNewOwnedTagEventDescriptor(GameplayTagSystemGrantOwnedTagEvent target) : base(target)
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