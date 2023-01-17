using System.Collections.Generic;
using UnityEngine;

using System.Diagnostics;

namespace StudioScor.GameplayTagSystem
{
    [DefaultExecutionOrder(GameplayTagSystemExecutionOrder.MAIN_ORDER)]
    [AddComponentMenu("StudioScor/GameplayTagSystem/GameplayTag System Component", order:0)]
    public partial class GameplayTagSystem : MonoBehaviour
    {
        #region Events
        public delegate void GameplayTagEventHandler(GameplayTagSystem gameplayTagComponent, GameplayTag gameplayTag);
        #endregion

        [Header(" [ Setup ] ")]
        [SerializeField] private FGameplayTags _InitializationTags;

#if UNITY_EDITOR
        [Header(" [ Use Debug ] ")]
        [SerializeField] private bool _UseDebug;
        protected bool UseDebug => _UseDebug;
#endif

        private Dictionary<GameplayTag, int> _OwnedTags;
        private Dictionary<GameplayTag, int> _BlockTags;

        public IReadOnlyDictionary<GameplayTag, int> OwnedTags
        {
            get
            {
                if (!_WasSetup)
                    Setup();

                return _OwnedTags;
            }
        }
        public IReadOnlyDictionary<GameplayTag, int> BlockTags
        {
            get
            {
                if (!_WasSetup)
                    Setup();

                return _BlockTags;
            }
        }


        public event GameplayTagEventHandler OnGrantedOwnedTag;
        public event GameplayTagEventHandler OnAddedOwnedTag;
        public event GameplayTagEventHandler OnSubtractedOwnedTag;
        public event GameplayTagEventHandler OnRemovedOwnedTag;

        public event GameplayTagEventHandler OnGrantedBlockTag;
        public event GameplayTagEventHandler OnAddedBlockTag;
        public event GameplayTagEventHandler OnSubtractedBlockTag;
        public event GameplayTagEventHandler OnRemovedBlockTag;

        public event GameplayTagEventHandler OnTriggeredTag;

        private bool _WasSetup = false;


        #region EDITOR ONLY
        [Conditional("UNITY_EDITOR")]
        protected void Log(object content, bool isError = false)
        {
#if UNITY_EDITOR
            if (isError)
            {
                UnityEngine.Debug.LogError("GameplayTag Sytstem [ " + transform.name + " ] : " + content, this);

                return;
            }

            if (UseDebug)
                UnityEngine.Debug.Log("GameplayTag Sytstem [ " + transform.name + " ] : " + content, this);
#endif
        }
        #endregion
        private void Awake()
        {
            if (!_WasSetup)
                Setup();
        }

        protected virtual void Setup()
        {
            if (_WasSetup)
                return;

            _WasSetup = true;

            _OwnedTags = new();
            _BlockTags = new();

            AddOwnedTags(_InitializationTags.Owneds);
            AddBlockTags(_InitializationTags.Blocks);
        }

        public void ResetGameplayTagSystem()
        {
            _OwnedTags.Clear();
            _BlockTags.Clear();

            AddOwnedTags(_InitializationTags.Owneds);
            AddBlockTags(_InitializationTags.Blocks);
        }

        #region Trigger Tag
        public void TriggerTag(GameplayTag triggerTag)
        {
            if (triggerTag is null)
                return;

            Callback_OnTriggerTag(triggerTag);
            Callback_OnTriggerTag_VisualScripting(triggerTag);
        }

        public void TriggerTags(GameplayTag[] triggerTags)
        {
            if (triggerTags is null)
                return;

            foreach (GameplayTag tag in triggerTags)
            {
                TriggerTag(tag);
            }
        }
#endregion

        #region Add, Remove Tags
        public void AddOwnedTag(GameplayTag addOwnedTag)
        {
            if (addOwnedTag == null)
                return;

            if (OwnedTags.ContainsKey(addOwnedTag))
            {
                _OwnedTags[addOwnedTag] += 1;

                if (_OwnedTags[addOwnedTag] == 1)
                {
                    Callback_OnGrantedOwnedTag(addOwnedTag);
                    Callback_OnGrantedOwnedTag_VisualScripting(addOwnedTag);
                }
            }
            else
            {
                _OwnedTags.TryAdd(addOwnedTag, 1);

                Callback_OnGrantedOwnedTag(addOwnedTag);
                Callback_OnGrantedOwnedTag_VisualScripting(addOwnedTag);
            }

            Callback_OnAddedOwnedTag(addOwnedTag);
            Callback_OnAddedOwnedTag_VisualScripting(addOwnedTag);
        }

        public void AddOwnedTags(GameplayTag[] addOwnedTags)
        {
            if (addOwnedTags == null)
                return;

            foreach (GameplayTag tag in addOwnedTags)
            {
                AddOwnedTag(tag);
            }
        }

        public void RemoveOwnedTag(GameplayTag removeOwnedTag)
        {
            if (removeOwnedTag == null)
                return;

            if (OwnedTags.TryGetValue(removeOwnedTag, out int value))
            {
                _OwnedTags[removeOwnedTag] -= 1;

                if (value == 0)
                {
                    Callback_OnRemovedOwnedTag(removeOwnedTag);
                    Callback_OnRemovedOwnedTag_VisualScripting(removeOwnedTag);
                }
            }
            else
            {
                _OwnedTags.Add(removeOwnedTag, -1);
            }

            Callback_OnSubtractedOwnedTag(removeOwnedTag);
            Callback_OnSubtractedOwnedTag_VisualScripting(removeOwnedTag);
        }

        public void RemoveOwnedTags(GameplayTag[] removeOwnedTags)
        {
            if (removeOwnedTags == null)
                return;

            foreach (GameplayTag tag in removeOwnedTags)
            {
                RemoveOwnedTag(tag);
            }
        }

        public void AddBlockTag(GameplayTag addBlockTag)
        {
            if (addBlockTag == null)
                return;

            if (BlockTags.ContainsKey(addBlockTag))
            {
                _BlockTags[addBlockTag] += 1;

                if (_BlockTags[addBlockTag] == 1)
                {
                    Callback_OnGrantedBlockTag(addBlockTag);
                    Callback_OnGrantedBlockTag_VisualScripting(addBlockTag);
                }
            }
            else
            {
                _BlockTags.TryAdd(addBlockTag, 1);

                Callback_OnGrantedBlockTag(addBlockTag);
                Callback_OnGrantedBlockTag_VisualScripting(addBlockTag);
            }

            Callback_OnAddedBlockTag(addBlockTag);
            Callback_OnAddedBlockTag_VisualScripting(addBlockTag);
        }

        public void AddBlockTags(GameplayTag[] addBlockTags)
        {
            if (addBlockTags == null)
                return;

            foreach (GameplayTag tag in addBlockTags)
            {
                AddBlockTag(tag);
            }
        }

        public void RemoveBlockTag(GameplayTag removeBlockTag)
        {
            if (removeBlockTag == null)
                return;

            if (BlockTags.TryGetValue(removeBlockTag, out int value))
            {
                _BlockTags[removeBlockTag] -= 1;

                if (value == 0)
                {
                    _BlockTags[removeBlockTag] = 0;

                    Callback_OnRemovedBlockTag(removeBlockTag);
                    Callback_OnRemovedBlockTag_VisualScripting(removeBlockTag);
                }
            }
            else
            {
                _BlockTags.Add(removeBlockTag, -1);
            }

            Callback_OnSubtractedBlcokTag(removeBlockTag);
            Callback_OnSubtractedBlockTag_VisualScripting(removeBlockTag);
        }

        public void RemoveBlockTags(GameplayTag[] removeTags)
        {
            if (removeTags == null)
                return;

            foreach (GameplayTag tag in removeTags)
            {
                RemoveBlockTag(tag);
            }
        }
#endregion

        #region Check Has Tag
        
        public bool ContainOwnedTag(GameplayTag tag)
        {
            return ContainTag(OwnedTags, tag);
        }
        public bool ContainBlockTag(GameplayTag tag)
        {
            return ContainTag(BlockTags, tag);
        }
        protected bool ContainTag(IReadOnlyDictionary<GameplayTag, int> container, GameplayTag tag)
        {
            if (tag is null)
                return false;

            if (!container.TryGetValue(tag, out int value))
                return false;

            return value > 0;
        }

        protected bool ContainAllTags(IReadOnlyDictionary<GameplayTag, int> container, GameplayTag[] tags)
        {
            foreach (GameplayTag tag in tags)
            {
                if (!ContainTag(container, tag))
                {
                    return false;
                }
            }
            return true;
        }
        protected bool ContainAllTags(IReadOnlyDictionary<GameplayTag, int> container, IReadOnlyCollection<GameplayTag> tags)
        {
            foreach (GameplayTag tag in tags)
            {
                if (!ContainTag(container, tag))
                {
                    return false;
                }
            }
            return true;
        }

        protected bool ContainAnyTags(IReadOnlyDictionary<GameplayTag, int> container, GameplayTag[] tags)
        {
            foreach (GameplayTag tag in tags)
            {
                if (ContainTag(container, tag))
                {
                    return true;
                }
            }
            return false;
        }
        protected bool ContainAnyTags(IReadOnlyDictionary<GameplayTag, int> container, IReadOnlyCollection<GameplayTag> tags)
        {
            foreach (GameplayTag tag in tags)
            {
                if (ContainTag(container, tag))
                {
                    return true;
                }
            }
            return false;
        }

        public bool ContainAllTagsInOwned(GameplayTag[] tags)
        {
            return ContainAllTags(OwnedTags, tags);
        }
        public bool ContainAllTagsInOwned(IReadOnlyCollection<GameplayTag> tags)
        {
            return ContainAllTags(OwnedTags, tags);
        }
        public bool ContainAllTagsInBlock(GameplayTag[] tags)
        {
            return ContainAllTags(BlockTags, tags);
        }
        public bool ContainAllTagsInBlock(IReadOnlyCollection<GameplayTag> tags)
        {
            return ContainAllTags(BlockTags, tags);
        }
        public bool ContainAnyTagsInOwned(GameplayTag[] tags)
        {
            return ContainAnyTags(OwnedTags, tags);
        }
        public bool ContainAnyTagsInOwned(IReadOnlyCollection<GameplayTag> tags)
        {
            return ContainAnyTags(OwnedTags, tags);
        }
        public bool ContainAnyTagsInBlock(GameplayTag[] tags)
        {
            return ContainAnyTags(BlockTags, tags);
        }
        public bool ContainAnyTagsInBlock(IReadOnlyCollection<GameplayTag> tags)
        {
            return ContainAnyTags(BlockTags, tags);
        }

        #endregion


        #region With VisualScripting
        [Conditional("SCOR_ENABLE_VISUALSCRIPTING")]
        partial void Callback_OnTriggerTag_VisualScripting(GameplayTag triggerTag);


        [Conditional("SCOR_ENABLE_VISUALSCRIPTING")]
        partial void Callback_OnGrantedOwnedTag_VisualScripting(GameplayTag addNewOwnedTag);

        [Conditional("SCOR_ENABLE_VISUALSCRIPTING")]
        partial void Callback_OnAddedOwnedTag_VisualScripting(GameplayTag addOwnedTag);

        [Conditional("SCOR_ENABLE_VISUALSCRIPTING")]
        partial void Callback_OnRemovedBlockTag_VisualScripting(GameplayTag removeBlockTag);

        [Conditional("SCOR_ENABLE_VISUALSCRIPTING")]
        partial void Callback_OnSubtractedOwnedTag_VisualScripting(GameplayTag removeOwnedTag);

        [Conditional("SCOR_ENABLE_VISUALSCRIPTING")]
        partial void Callback_OnGrantedBlockTag_VisualScripting(GameplayTag addNewBlockTag);

        [Conditional("SCOR_ENABLE_VISUALSCRIPTING")]
        partial void Callback_OnAddedBlockTag_VisualScripting(GameplayTag addBlockTag);

        [Conditional("SCOR_ENABLE_VISUALSCRIPTING")]
        partial void Callback_OnRemovedOwnedTag_VisualScripting(GameplayTag removeOwnedTag);

        [Conditional("SCOR_ENABLE_VISUALSCRIPTING")]
        partial void Callback_OnSubtractedBlockTag_VisualScripting(GameplayTag removeBlockTag);
        
        #endregion

        #region CallBack
        protected virtual void Callback_OnTriggerTag(GameplayTag  triggerTag)
        {
            Log("On Trigger Tag - " + triggerTag);

            OnTriggeredTag?.Invoke(this, triggerTag);
        }
        protected virtual void Callback_OnAddedOwnedTag(GameplayTag addOwnedTag)
        {
            Log("Add Owned Tag - " + addOwnedTag);

            OnAddedOwnedTag?.Invoke(this, addOwnedTag);
        }
        protected virtual void Callback_OnSubtractedOwnedTag(GameplayTag addOwnedTag)
        {
            Log("Subtract Owned Tag - " + addOwnedTag);

            OnSubtractedOwnedTag?.Invoke(this, addOwnedTag);
        }
        protected virtual void Callback_OnGrantedOwnedTag(GameplayTag addNewOwnedTag)
        {
            Log("Add New Owned Tag - " + addNewOwnedTag);

            OnGrantedOwnedTag?.Invoke(this, addNewOwnedTag);
        }
        protected virtual void Callback_OnRemovedOwnedTag(GameplayTag removeOwnedTag)
        {
            Log("Remove Owned Tag - " + removeOwnedTag);

            OnRemovedOwnedTag?.Invoke(this, removeOwnedTag);
        }
        protected virtual void Callback_OnAddedBlockTag(GameplayTag addBlockTag)
        {
            Log("Add Block Tag - " + addBlockTag);

            OnAddedBlockTag?.Invoke(this, addBlockTag);
        }
        protected virtual void Callback_OnSubtractedBlcokTag(GameplayTag addBlockTag)
        {
            Log("Subtract Block Tag - " + addBlockTag);

            OnSubtractedBlockTag?.Invoke(this, addBlockTag);
        }
        protected virtual void Callback_OnGrantedBlockTag(GameplayTag addNewBlockTag)
        {
            Log("Add New Block Tag - " + addNewBlockTag);

            OnGrantedBlockTag?.Invoke(this, addNewBlockTag);
        }
        protected virtual void Callback_OnRemovedBlockTag(GameplayTag removeBlockTag)
        {
            Log("Remove Block Tag - " + removeBlockTag);

            OnRemovedBlockTag?.Invoke(this, removeBlockTag);
        }
        #endregion
    }
}
