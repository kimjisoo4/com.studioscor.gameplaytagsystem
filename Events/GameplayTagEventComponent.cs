using UnityEngine;

using System.Diagnostics;

namespace StudioScor.GameplayTagSystem
{
    [DefaultExecutionOrder(GameplayTagSystemExecutionOrder.SUB_ORDER)]
    [AddComponentMenu("StudioScor/GameplayTagSystem/GameplayTagSystem Event Component", order: 10)]
    public class GameplayTagEventComponent : MonoBehaviour
    {
        [SerializeField] private GameplayTagSystemComponent _GameplayTagSystemComponent;
        [SerializeField] private GameplayTagEvent[] _GameplayEvents;

        #region EDITOR ONLY
#if UNITY_EDITOR
        [Header(" [ Use Debug ] ")]
        [SerializeField] private bool _UseDebug;
        protected bool UseDebug => _UseDebug;
        private void Reset()
        {
            SetGameplayTagSystemComponent();
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
            if (!_GameplayTagSystemComponent)
                SetGameplayTagSystemComponent();
        }

        private void OnDisable()
        {
            ResetGameplayEvents();
        }
        private void OnEnable()
        {
            if (!_GameplayTagSystemComponent)
            {
                Log("GamepalyTag System Is Null", true);

                return;
            }

            SetupGameplayEvents();
        }

        protected void SetGameplayTagSystemComponent()
        {
            if (_GameplayTagSystemComponent)
                return;

            _GameplayTagSystemComponent = GetComponentInParent<GameplayTagSystemComponent>();

            if (!_GameplayTagSystemComponent)
            {
                _GameplayTagSystemComponent = GetComponentInChildren<GameplayTagSystemComponent>();
            }
        }

        protected void SetupGameplayEvents()
        {
            foreach (GameplayTagEvent gameplayTagEvent in _GameplayEvents)
            {
                gameplayTagEvent.SetGameplayEvent(_GameplayTagSystemComponent);
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
