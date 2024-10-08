#if SCOR_ENABLE_VISUALSCRIPTING
using UnityEngine;
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.Extend.VisualScripting
{
    [UnitCategory("StudioScor\\GameplayTagSystem")]
    public abstract class GameplayTagSystemUnit : Unit
    {
        [DoNotSerialize]
        [NullMeansSelf]
        [PortLabel("Target")]
        [PortLabelHidden]
        public ValueInput Target;

        protected override void Definition()
        {
            Target = ValueInput<GameObject>(nameof(Target), null).NullMeansSelf();
        }
    }
}

#endif