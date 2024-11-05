using UnityEngine;
using StudioScor.Utilities;
using UnityEngine.Events;

namespace StudioScor.GameplayTagSystem
{
    [DefaultExecutionOrder(GameplayTagSystemUtility.SUB_ORDER)]
    [AddComponentMenu("StudioScor/GameplayTagSystem/GameplayTag Condition Observer Component", order: 0)]
    public class GameplayTagConditionObserverComponent : BaseMonoBehaviour
    {
        [Header(" [ GameplayTag Condition Observer Component ] ")]
        [SerializeField] private GameplayTagConditionObserver conditionObserver;
        [SerializeField] private GameObject target;

        [Header(" Unity Events ")]
        [SerializeField] private bool _useEventInStart = true;
        [SerializeField] private UnityEvent onActivated;
        [SerializeField] private UnityEvent onDeactivated;

        private void Awake()
        {
            conditionObserver.OnActivated += ConditionObserver_OnChangedState;
            conditionObserver.OnDeactivate += ConditionObserver_OnDeactivate;
        }
        private void OnDestroy()
        {
            conditionObserver.OnActivated -= ConditionObserver_OnChangedState;
            conditionObserver.OnDeactivate -= ConditionObserver_OnDeactivate;
        }


        private void OnEnable()
        {
            conditionObserver.SetOwner(target);
            conditionObserver.OnObserver();
        }
        private void OnDisable()
        {
            conditionObserver.EndObserver();
        }

        private void ConditionObserver_OnChangedState(GameplayTagObserver gameplayTagConditionObserver, bool inStart)
        {
            if (_useEventInStart != inStart)
                return;

            Log($"{nameof(onActivated)}");

            onActivated?.Invoke();
        }
        private void ConditionObserver_OnDeactivate(GameplayTagObserver gameplayTagConditionObserver, bool inStart)
        {
            if (_useEventInStart != inStart)
                return;

            Log($"{nameof(onDeactivated)}");

            onDeactivated?.Invoke();
        }
    }
}
