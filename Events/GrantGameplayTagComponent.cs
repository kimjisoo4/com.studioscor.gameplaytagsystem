using UnityEngine;
using System.Diagnostics;

namespace StudioScor.GameplayTagSystem
{
    [DefaultExecutionOrder(GameplayTagSystemExecutionOrder.SUB_ORDER)]
    [AddComponentMenu("StudioScor/GameplayTagSystem/Grant GameplayTagComponent", order:10)]
    public class GrantGameplayTagComponent : MonoBehaviour
    {
        [Header(" [ Grant GameplayTag Component ] ")]
        [SerializeField] private GameplayTagSystemComponent _GameplayTagSystemComponent;
        [Space(5f)]
        [SerializeField] private FGameplayTags _ToggleTags;
        [Space(5f)]
        [SerializeField] private bool _AutoActivate = true;

        private bool _WasGrantTags = false;

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
            {
                SetGameplayTagSystem();
            }
        }
        private void OnEnable()
        {
            if(_AutoActivate)
                AddGameplayTags();
        }
        private void OnDisable()
        {
            if(_AutoActivate)
                RemoveGameplayTags();
        }

        private void SetGameplayTagSystem()
        {
            _GameplayTagSystemComponent = GetComponentInParent<GameplayTagSystemComponent>();

            if (!_GameplayTagSystemComponent)
            {
                _GameplayTagSystemComponent = GetComponentInChildren<GameplayTagSystemComponent>();
            }
        }

        public void AddGameplayTags()
        {
            if (_WasGrantTags)
                return;

            _WasGrantTags = true;

            if (!_GameplayTagSystemComponent)
            {
                Log("GamepalyTag System Is Null", true);

                return;
            }

            _GameplayTagSystemComponent.AddOwnedTags(_ToggleTags.Owneds);
            _GameplayTagSystemComponent.AddBlockTags(_ToggleTags.Blocks);
        }
        public void RemoveGameplayTags()
        {
            if (!_WasGrantTags)
                return;

            _WasGrantTags = false;

            if (!_GameplayTagSystemComponent)
            {
                Log("GamepalyTag System Is Null", true);

                return;
            }

            _GameplayTagSystemComponent.RemoveOwnedTags(_ToggleTags.Owneds);
            _GameplayTagSystemComponent.RemoveBlockTags(_ToggleTags.Blocks);
        }
    }
}
