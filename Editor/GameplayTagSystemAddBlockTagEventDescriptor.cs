
#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem
{
    [Descriptor(typeof(GameplayTagSystemAddBlockTagEvent))]
    public sealed class GameplayTagSystemAddBlockTagEventDescriptor : UnitDescriptor<GameplayTagSystemAddBlockTagEvent>
    {
        public GameplayTagSystemAddBlockTagEventDescriptor(GameplayTagSystemAddBlockTagEvent target) : base(target)
        {
        }

        protected override EditorTexture DefaultIcon()
        {
            return PathUtility.Load("T_AddBlockTag_D");
        }
        protected override EditorTexture DefinedIcon()
        {
            return PathUtility.Load("T_AddBlockTag_D");
        }
    }
}

#endif