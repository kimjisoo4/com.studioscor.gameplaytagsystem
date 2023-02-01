#if SCOR_ENABLE_VISUALSCRIPTING
using System;
using System.Collections.Generic;

using Unity.VisualScripting;
using StudioScor.Utilities.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    public abstract class GameplayTagSystemEventUnit: CustomGameObjectEventUnit<GameplayTagSystemComponent, GameplayTag>
    {
        [DoNotSerialize]
        [PortLabel("GameplayTag")]
        public ValueOutput GameplayTag { get; private set; }

        public override Type MessageListenerType => typeof(GameplayTagSystemMessageListener);

        protected override void Definition()
        {
            base.Definition();

            GameplayTag = ValueOutput<GameplayTag>(nameof(GameplayTag));
        }

        protected override void AssignArguments(Flow flow, GameplayTag data)
        {
            flow.SetValue(GameplayTag, data);
        }
    }
}

#endif