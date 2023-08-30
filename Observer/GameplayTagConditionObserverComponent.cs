using UnityEngine;
using StudioScor.Utilities;
using UnityEngine.Events;

namespace StudioScor.GameplayTagSystem
{
    [DefaultExecutionOrder(GameplayTagSystemUtility.SUB_ORDER)]
    [AddComponentMenu("StudioScor/GameplayTagSystem/GameplayTagSystem Component", order: 0)]
    public class GameplayTagConditionObserverComponent : BaseMonoBehaviour
    {
        [Header(" [ GameplayTag Condition Observer Component ] ")]
        [SerializeField] private GameplayTagConditionObserver conditionObserver;
        [SerializeField] private GameObject target;

        [Header(" Unity Events ")]
        [SerializeField] private UnityEvent onActivated;
        [SerializeField] private UnityEvent onDeactivated;

        private void Awake()
        {
            conditionObserver.OnChangedState += ConditionObserver_OnChangedState;
        }
        private void OnDestroy()
        {
            conditionObserver.OnChangedState -= ConditionObserver_OnChangedState;
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

        private void ConditionObserver_OnChangedState(GameplayTagObserver gameplayTagConditionObserver, bool isOn)
        {
            Log("Condition Changed", false, isOn ? SUtility.NAME_COLOR_GREEN : SUtility.NAME_COLOR_GRAY);

            if (isOn)
                onActivated?.Invoke();
            else
                onDeactivated?.Invoke();
        }
    }
}
