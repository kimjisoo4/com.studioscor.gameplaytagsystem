using UnityEngine;

using StudioScor.Utilities;

namespace StudioScor.GameplayTagSystem
{
    [DefaultExecutionOrder(GameplayTagSystemExecutionOrder.SUB_ORDER)]
    [AddComponentMenu("StudioScor/GameplayTagSystem/GameplayTagSystem Event Component", order: 10)]
    public class GameplayTagEventComponent : BaseMonoBehaviour
    {
        [SerializeField] private GameplayTagSystemComponent _GameplayTagSystemComponent;
        [SerializeField] private GameplayTagEvent[] _GameplayEvents;

#if UNITY_EDITOR
        private void Reset()
        {
            SetGameplayTagSystemComponent();
        }
#endif

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
