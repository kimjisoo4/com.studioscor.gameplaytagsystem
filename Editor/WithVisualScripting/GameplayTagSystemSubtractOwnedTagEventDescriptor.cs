
#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting.Editor
{
    [Descriptor(typeof(GameplayTagSystemSubtractOwnedTagEvent))]
    public sealed class GameplayTagSystemSubtractOwnedTagEventDescriptor : UnitDescriptor<GameplayTagSystemSubtractOwnedTagEvent>
    {
        public GameplayTagSystemSubtractOwnedTagEventDescriptor(GameplayTagSystemSubtractOwnedTagEvent target) : base(target)
        {
        }

        protected override EditorTexture DefaultIcon()
        {
            return PathUtility.Load("T_SubtractOwnedTag_D");
        }
        protected override EditorTexture DefinedIcon()
        {
            return PathUtility.Load("T_SubtractOwnedTag_D");
        }
    }
}

#endif