
#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting.Editor
{
    [Descriptor(typeof(GameplayTagSystemSubtractBlockTagEvent))]
    public sealed class GameplayTagSystemSubtractBlockTagEventDescriptor : UnitDescriptor<GameplayTagSystemSubtractBlockTagEvent>
    {
        public GameplayTagSystemSubtractBlockTagEventDescriptor(GameplayTagSystemSubtractBlockTagEvent target) : base(target)
        {
        }

        protected override EditorTexture DefaultIcon()
        {
            return PathUtility.Load("T_SubtractBlockTag_D");
        }
        protected override EditorTexture DefinedIcon()
        {
            return PathUtility.Load("T_SubtractBlockTag_D");
        }
    }
}

#endif