using System.Collections.Generic;
using UnityEngine;

using System.Diagnostics;


namespace StudioScor.GameplayTagSystem
{
    [DefaultExecutionOrder(GameplayTagSystemExecutionOrder.MAIN_ORDER)]
    [AddComponentMenu("StudioScor/GameplayTagSystem/GameplayTag System Component", order:0)]
    public partial class GameplayTagSystemComponent : MonoBehaviour
    {
        #region Events
        public delegate void GameplayTagEventHandler(GameplayTagSystemComponent gameplayTagComponent, GameplayTag gameplayTag);
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


        public event GameplayTagEventHandler OnAddedNewOwnedTag;
        public event GameplayTagEventHandler OnAddedOwnedTag;
        public event GameplayTagEventHandler OnSubtractedOwnedTag;
        public event GameplayTagEventHandler OnRemovedOwnedTag;

        public event GameplayTagEventHandler OnAddedNewBlockTag;
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

            OnTriggerTag(triggerTag);

            OnTriggeredTag?.Invoke(this, triggerTag);
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
                    OnAddNewOwnedTag(addOwnedTag);
                }
            }
            else
            {
                _OwnedTags.TryAdd(addOwnedTag, 1);

                OnAddNewOwnedTag(addOwnedTag);
            }

            OnAddOwnedTag(addOwnedTag);
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
                if (value > 1)
                {
                    _OwnedTags[removeOwnedTag] -= 1;
                }
                else if (value == 1)
                {
                    _OwnedTags[removeOwnedTag] = 0;

                    OnRemoveOwnedTag(removeOwnedTag);
                }
            }

            OnSubtractOwnedTag(removeOwnedTag);
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
                    OnAddNewBlockTag(addBlockTag);
                }
            }
            else
            {
                _BlockTags.TryAdd(addBlockTag, 1);

                OnAddNewBlockTag(addBlockTag);
            }

            OnAddBlockTag(addBlockTag);
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

        public void RemoveBlockTag(GameplayTag removeBlockTags)
        {
            if (removeBlockTags == null)
                return;

            if (BlockTags.TryGetValue(removeBlockTags, out int value))
            {
                if (value > 1)
                {
                    _BlockTags[removeBlockTags] -= 1;
                }
                else if (value == 1)
                {
                    _BlockTags[removeBlockTags] = 0;

                    OnRemoveBlockTag(removeBlockTags);
                }
            }

            OnSubtractBlcokTag(removeBlockTags);
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

        #region CallBack
        protected virtual void OnTriggerTag(GameplayTag  triggerTag)
        {
            Log("On Trigger Tag - " + triggerTag);

            OnTriggeredTag?.Invoke(this, triggerTag);

            TriggerTagWithVisualScripting(triggerTag);
        }
        protected virtual void OnAddOwnedTag(GameplayTag addOwnedTag)
        {
            Log("Add Owned Tag - " + addOwnedTag);

            OnAddedOwnedTag?.Invoke(this, addOwnedTag);

            AddOwnedTagWithVisualScripting(addOwnedTag);
        }
        protected virtual void OnSubtractOwnedTag(GameplayTag addOwnedTag)
        {
            Log("Subtract Owned Tag - " + addOwnedTag);

            OnSubtractedOwnedTag?.Invoke(this, addOwnedTag);

            SubtractOwnedTagWithVisualScripting(addOwnedTag);
        }
        protected virtual void OnAddNewOwnedTag(GameplayTag addNewOwnedTag)
        {
            Log("Add New Owned Tag - " + addNewOwnedTag);

            OnAddedNewOwnedTag?.Invoke(this, addNewOwnedTag);

            AddNewOwnedTagWithVisualScripting(addNewOwnedTag);
        }
        protected virtual void OnRemoveOwnedTag(GameplayTag removeOwnedTag)
        {
            Log("Remove Owned Tag - " + removeOwnedTag);

            OnRemovedOwnedTag?.Invoke(this, removeOwnedTag);

            RemoveOwnedTagWithVisualScripting(removeOwnedTag);
        }
        protected virtual void OnAddBlockTag(GameplayTag addBlockTag)
        {
            Log("Add Block Tag - " + addBlockTag);

            OnAddedBlockTag?.Invoke(this, addBlockTag);

            AddBlockTagWithVisualScripting(addBlockTag);
        }
        protected virtual void OnSubtractBlcokTag(GameplayTag addBlockTag)
        {
            Log("Subtract Block Tag - " + addBlockTag);

            OnSubtractedBlockTag?.Invoke(this, addBlockTag);

            SubtractBlockTagWithVisualScripting(addBlockTag);
        }
        protected virtual void OnAddNewBlockTag(GameplayTag addNewBlockTag)
        {
            Log("Add New Block Tag - " + addNewBlockTag);

            OnAddedNewBlockTag?.Invoke(this, addNewBlockTag);

            AddNewBlockTagWithVisualScripting(addNewBlockTag);
        }
        protected virtual void OnRemoveBlockTag(GameplayTag removeBlockTag)
        {
            Log("Remove Block Tag - " + removeBlockTag);

            OnRemovedBlockTag?.Invoke(this, removeBlockTag);

            RemoveBlockTagWithVisualScripting(removeBlockTag);
        }
        #endregion
    }
}
