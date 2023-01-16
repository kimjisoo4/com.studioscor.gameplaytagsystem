#if SCOR_ENABLE_VISUALSCRIPTING
using System;
using System.Collections.Generic;

using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem
{
    public abstract class GameplayTagSystemCustomUnitEvent : GameObjectEventUnit<GameplayTag>
    {
        [DoNotSerialize]
        public ValueOutput GameplayTag { get; private set; }
        public override Type MessageListenerType => default;
        protected abstract string EventName { get; }

        public override EventHook GetHook(GraphReference reference)
        {
            var data = reference.GetElementData<Data>(this);

            return new EventHook(EventName, data.target);
        }

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