
#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem
{
    [Descriptor(typeof(GameplayTagSystemAddNewOwnedTagEvent))]
    public sealed class GameplayTagSystemAddNewOwnedTagEventDescriptor : UnitDescriptor<GameplayTagSystemAddNewOwnedTagEvent>
    {
        public GameplayTagSystemAddNewOwnedTagEventDescriptor(GameplayTagSystemAddNewOwnedTagEvent target) : base(target)
        {
        }

        protected override EditorTexture DefaultIcon()
        {
            return PathUtility.Load("T_AddNewOwnedTag_D");
        }
        protected override EditorTexture DefinedIcon()
        {
            return PathUtility.Load("T_AddNewOwnedTag_D");
        }
    }
}

#endif