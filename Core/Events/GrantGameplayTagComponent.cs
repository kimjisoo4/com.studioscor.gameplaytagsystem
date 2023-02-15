using UnityEngine;
using StudioScor.Utilities;

namespace StudioScor.GameplayTagSystem
{
    [DefaultExecutionOrder(GameplayTagSystemExecutionOrder.SUB_ORDER)]
    [AddComponentMenu("StudioScor/GameplayTagSystem/Grant GameplayTagComponent", order:10)]
    public class GrantGameplayTagComponent : BaseMonoBehaviour
    {
        [Header(" [ Grant GameplayTag Component ] ")]
        [SerializeField] private GameplayTagSystemComponent _GameplayTagSystemComponent;
        [SerializeField] private bool _AutoActivate = true;
        [Space(5f)]
        [SerializeField] private FGameplayTags _ToggleTags;

        private bool _WasGrantTags = false;

#if UNITY_EDITOR
        private void Reset()
        {
            SetGameplayTagSystemComponent();
        }
#endif

        private void Awake()
        {
            SetGameplayTagSystemComponent();
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

        private void SetGameplayTagSystemComponent()
        {
            if (_GameplayTagSystemComponent)
                return;

            if (!gameObject.TryGetComponentInParentOrChildren(out _GameplayTagSystemComponent))
            {
                Log("GameplayTag System Component Is NULL!!", true);
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
