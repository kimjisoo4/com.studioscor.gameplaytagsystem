#if SCOR_ENABLE_VISUALSCRIPTING

using System;
using System.Diagnostics;
using UnityEngine.Pool;

namespace StudioScor.GameplayTagSystem
{
    public class TriggerTagData
    {
        private static ObjectPool<TriggerTagData> _pool;
        private IGameplayTag _triggerTag;
        private object _data;
        public TriggerTagData()
        { }

        public IGameplayTag TriggerTag => _triggerTag;
        public object Data => _data;

        public static TriggerTagData Get(IGameplayTag gameplayTag, object data)
        {
            if(_pool is null)
            {
                _pool = new ObjectPool<TriggerTagData>(Create);
            }

            var poolData = _pool.Get();

            poolData._triggerTag = gameplayTag;
            poolData._data = data;

            return poolData;
        }

        private static TriggerTagData Create()
        {
#if UNITY_EDITOR
            if(_pool.CountActive > 50)
            {
                UnityEngine.Debug.Log($"[{nameof(TriggerTagData)}] TriggerTagData is need released");
            }
#endif
            return new TriggerTagData();
        }

        public void Release()
        {
            _pool.Release(this);
        }
    }

    public static class GameplayTagSystemWithVisualScripting
    {
        public const string TRIGGER_TAG = "TriggerTag";

        public const string ADD_OWNED_TAG = "AddOwnedTag";
        public const string SUBTRACT_OWNED_TAG = "SubtractOwnedTag";
        public const string REMOVE_OWNED_TAG = "RemoveOwnedTag";

        public const string ADD_BLOCK_TAG = "AddBlockTag";
        public const string SUBTRACT_BLOCK_TAG = "SubtractBlockTag";
        public const string REMOVE_BLOCK_TAG = "RemoveBlockTag";

        public const string GRANT_OWNED_TAG = "GrantOwnedTag";
        public const string GRANT_BLOCK_TAG = "GrantBlockTag";
    }
}
#endif