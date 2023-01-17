
#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting.Editor
{
    [Descriptor(typeof(GameplayTagSystemRemoveOwnedTagEvent))]
    public sealed class GameplayTagSystemRemoveOwnedTagEventDescriptor : UnitDescriptor<GameplayTagSystemRemoveOwnedTagEvent>
    {
        public GameplayTagSystemRemoveOwnedTagEventDescriptor(GameplayTagSystemRemoveOwnedTagEvent target) : base(target)
        {
        }

        protected override EditorTexture DefaultIcon()
        {
            return PathUtility.Load("T_RemoveOwnedTag_D");
        }
        protected override EditorTexture DefinedIcon()
        {
            return PathUtility.Load("T_RemoveOwnedTag_D");
        }
    }
}

#endif