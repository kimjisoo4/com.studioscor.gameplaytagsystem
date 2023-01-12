
#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem
{
    [Descriptor(typeof(GameplayTagSystemAddOwnedTagEvent))]
    public sealed class GameplayTagSystemAddOwnedTagEventDescriptor : UnitDescriptor<GameplayTagSystemAddOwnedTagEvent>
    {
        public GameplayTagSystemAddOwnedTagEventDescriptor(GameplayTagSystemAddOwnedTagEvent target) : base(target)
        {
        }

        protected override EditorTexture DefaultIcon()
        {
            return PathUtility.Load("T_AddOwnedTag_D");
        }
        protected override EditorTexture DefinedIcon()
        {
            return PathUtility.Load("T_AddOwnedTag_D");
        }
    }
}

#endif