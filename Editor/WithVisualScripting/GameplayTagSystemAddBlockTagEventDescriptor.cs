
#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting.Editor
{
    [Descriptor(typeof(GameplayTagSystemAddBlockTagEvent))]
    public sealed class GameplayTagSystemAddBlockTagEventDescriptor : UnitDescriptor<GameplayTagSystemAddBlockTagEvent>
    {
        public GameplayTagSystemAddBlockTagEventDescriptor(GameplayTagSystemAddBlockTagEvent target) : base(target)
        {
        }

        protected override EditorTexture DefaultIcon()
        {
            return GameplayTagSystemPathUtility.Load("T_AddBlockTag_D");
        }
        protected override EditorTexture DefinedIcon()
        {
            return GameplayTagSystemPathUtility.Load("T_AddBlockTag_D");
        }
    }
}

#endif