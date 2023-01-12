using UnityEngine;

using System.Diagnostics;

namespace StudioScor.GameplayTagSystem
{
    [DefaultExecutionOrder(GameplayTagSystemExecutionOrder.SUB_ORDER)]
    [AddComponentMenu("StudioScor/GameplayTagSystem/GameplayTag Event Component", order: 10)]
    public class GameplayTagEventComponent : MonoBehaviour
    {
        [SerializeField] private GameplayTagSystem _GameplayTagSystem;
        [SerializeField] private GameplayTagEvent[] _GameplayEvents;

        #region EDITOR ONLY
#if UNITY_EDITOR
        [Header(" [ Use Debug ] ")]
        [SerializeField] private bool _UseDebug;
        protected bool UseDebug => _UseDebug;
        private void Reset()
        {
            SetGameplayTagSystem();
        }
#endif

        public void EventTestLog(string content)
        {
#if UNITY_EDITOR
            Log(content);
#endif
        }

        [Conditional("UNITY_EDITOR")]
        protected void Log(object content, bool isError = false)
        {
#if UNITY_EDITOR
            if (isError)
            {
                UnityEngine.Debug.LogError("Grant GameplayTag Componenet [ " + transform.name + " ] : " + content, this);

                return;
            }

            if (UseDebug)
                UnityEngine.Debug.Log("Grant GameplayTag Componenet [ " + transform.name + " ] : " + content, this);
#endif
        }
        #endregion

        private void Awake()
        {
            if (!_GameplayTagSystem)
                SetGameplayTagSystem();
        }

        private void OnDisable()
        {
            ResetGameplayEvents();
        }
        private void OnEnable()
        {
            if (!_GameplayTagSystem)
            {
                Log("GamepalyTag System Is Null", true);

                return;
            }

            SetupGameplayEvents();
        }

        protected void SetGameplayTagSystem()
        {
            if (_GameplayTagSystem)
                return;

            _GameplayTagSystem = GetComponentInParent<GameplayTagSystem>();

            if (!_GameplayTagSystem)
            {
                _GameplayTagSystem = GetComponentInChildren<GameplayTagSystem>();
            }
        }

        protected void SetupGameplayEvents()
        {
            foreach (GameplayTagEvent gameplayTagEvent in _GameplayEvents)
            {
                gameplayTagEvent.SetGameplayEvent(_GameplayTagSystem);
            }
        }
        protected void ResetGameplayEvents()
        {
            foreach (GameplayTagEvent gameplayTagEvent in _GameplayEvents)
            {
                gameplayTagEvent.ResetGameplayEvent();
            }
        }
    }
}
