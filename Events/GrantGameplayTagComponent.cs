using UnityEngine;
using System.Diagnostics;

namespace StudioScor.GameplayTagSystem
{
    [DefaultExecutionOrder(GameplayTagSystemExecutionOrder.SUB_ORDER)]
    [AddComponentMenu("StudioScor/GameplayTagSystem/Grant GameplayTagComponent", order:10)]
    public class GrantGameplayTagComponent : MonoBehaviour
    {
        [Header(" [ Grant GameplayTag Component ] ")]
        [SerializeField] private GameplayTagSystem _GameplayTagSystem;
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
            if (!_GameplayTagSystem)
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
            _GameplayTagSystem = GetComponentInParent<GameplayTagSystem>();

            if (!_GameplayTagSystem)
            {
                _GameplayTagSystem = GetComponentInChildren<GameplayTagSystem>();
            }
        }

        public void AddGameplayTags()
        {
            if (_WasGrantTags)
                return;

            _WasGrantTags = true;

            if (!_GameplayTagSystem)
            {
                Log("GamepalyTag System Is Null", true);

                return;
            }

            _GameplayTagSystem.AddOwnedTags(_ToggleTags.Owneds);
            _GameplayTagSystem.AddBlockTags(_ToggleTags.Blocks);
        }
        public void RemoveGameplayTags()
        {
            if (!_WasGrantTags)
                return;

            _WasGrantTags = false;

            if (!_GameplayTagSystem)
            {
                Log("GamepalyTag System Is Null", true);

                return;
            }

            _GameplayTagSystem.RemoveOwnedTags(_ToggleTags.Owneds);
            _GameplayTagSystem.RemoveBlockTags(_ToggleTags.Blocks);
        }
    }
}
