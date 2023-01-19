
#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting.Editor
{
    [Descriptor(typeof(GameplayTagSystemGrantOwnedTagEvent))]
    public sealed class GameplayTagSystemAddNewOwnedTagEventDescriptor : UnitDescriptor<GameplayTagSystemGrantOwnedTagEvent>
    {
        public GameplayTagSystemAddNewOwnedTagEventDescriptor(GameplayTagSystemGrantOwnedTagEvent target) : base(target)
        {
        }

        protected override EditorTexture DefaultIcon()
        {
            return GameplayTagSystemPathUtility.Load("T_AddNewOwnedTag_D");
        }
        protected override EditorTexture DefinedIcon()
        {
            return GameplayTagSystemPathUtility.Load("T_AddNewOwnedTag_D");
        }
    }
}

#endif