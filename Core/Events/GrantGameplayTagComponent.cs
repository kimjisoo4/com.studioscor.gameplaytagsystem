using UnityEngine;
using StudioScor.Utilities;

namespace StudioScor.GameplayTagSystem
{
    [DefaultExecutionOrder(GameplayTagSystemExecutionOrder.SUB_ORDER)]
    [AddComponentMenu("StudioScor/GameplayTagSystem/Grant GameplayTagComponent", order:10)]
    public class GrantGameplayTagComponent : BaseMonoBehaviour
    {
        [Header(" [ Grant GameplayTag Component ] ")]
        [SerializeField] private GameObject _Target;
        [SerializeField] private bool _AutoPlaying = true;
        [Space(5f)]
        [SerializeField] private FGameplayTags _ToggleTags;

        private IGameplayTagSystem _GameplayTagSystem;
        private bool _IsPlaying = false;

#if UNITY_EDITOR
        private void Reset()
        {
            Setup();
        }
        private void OnValidate()
        {
            if (_Target)
            {
                if(_Target.TryGetComponentInParentOrChildren(out _GameplayTagSystem))
                {
                    if(_Target != _GameplayTagSystem.gameObject)
                    {
                        _Target = _GameplayTagSystem.gameObject;
                    }
                }
                else
                {
                    _Target = null;
                }
            }
        }
#endif

        private void Awake()
        {
            Setup();
        }
        private void OnEnable()
        {
            if(_AutoPlaying)
                OnToggleGameplayTag();
        }
        private void OnDisable()
        {
            if(_AutoPlaying)
                EndToggleGameplayTag();
        }

        private void Setup()
        {
            if (_GameplayTagSystem is not null)
                return;

            if (gameObject.TryGetComponentInParentOrChildren(out _GameplayTagSystem))
            {
                _Target = _GameplayTagSystem.gameObject;
            }
            else
            {
                Log("GameplayTag System Component Is NULL!!", true);
            }
        }

        public void SetTarget(GameObject gameObject)
        {
            var gameplayTagSystemEvent = gameObject.GetComponent<IGameplayTagSystem>();

            SetGameplayTagSystem(gameplayTagSystemEvent);
        }
        public void SetTarget(Component component)
        {
            var gameplayTagSystemEvent = component.GetComponent<IGameplayTagSystem>();

            SetGameplayTagSystem(gameplayTagSystemEvent);
        }
        public void SetGameplayTagSystem(IGameplayTagSystem gameplayTagSystem)
        {
            if (_GameplayTagSystem is not null)
            {
                EndToggleGameplayTag();

                _Target = null;
            }

            _GameplayTagSystem = gameplayTagSystem;

            if(_GameplayTagSystem is not null)
            {
                _Target = _GameplayTagSystem.gameObject;

                if(_AutoPlaying)
                    OnToggleGameplayTag();
            }
        }

        public void OnToggleGameplayTag()
        {
            if (_IsPlaying)
                return;

            _IsPlaying = true;

            Log("On Toggle GameplayTags");

            if (_GameplayTagSystem is null)
            {
                Log("GamepalyTag System Is Null", true);

                return;
            }

            _GameplayTagSystem.AddOwnedTags(_ToggleTags.Owneds);
            _GameplayTagSystem.AddBlockTags(_ToggleTags.Blocks);
        }
        public void EndToggleGameplayTag()
        {
            if (!_IsPlaying)
                return;

            _IsPlaying = false;

            Log("End Toggle GameplayTags");

            if (_GameplayTagSystem is null)
            {
                Log("GamepalyTag System Is Null", true);

                return;
            }

            _GameplayTagSystem.RemoveOwnedTags(_ToggleTags.Owneds);
            _GameplayTagSystem.RemoveBlockTags(_ToggleTags.Blocks);
        }
    }
}
