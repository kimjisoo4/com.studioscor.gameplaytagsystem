using UnityEngine;

namespace StudioScor.GameplayTagSystem
{
    public interface IGameplayTagSystemEvent
    {
        public GameObject gameObject { get; }
        public Transform transform { get; }

        public event GameplayTagEventHandler OnGrantedOwnedTag;
        public event GameplayTagEventHandler OnRemovedOwnedTag;
        public event GameplayTagEventHandler OnAddedOwnedTag;
        public event GameplayTagEventHandler OnSubtractedOwnedTag;

        public event GameplayTagEventHandler OnGrantedBlockTag;
        public event GameplayTagEventHandler OnRemovedBlockTag;
        public event GameplayTagEventHandler OnAddedBlockTag;
        public event GameplayTagEventHandler OnSubtractedBlockTag;

        public event GameplayTagEventHandler OnTriggeredTag;
    }
}
