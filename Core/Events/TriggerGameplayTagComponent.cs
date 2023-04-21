using UnityEngine;
using StudioScor.Utilities;

namespace StudioScor.GameplayTagSystem
{
    [DefaultExecutionOrder(GameplayTagSystemExecutionOrder.SUB_ORDER)]
    [AddComponentMenu("StudioScor/GameplayTagSystem/Trigger GameplayTagComponent", order: 10)]
    public class TriggerGameplayTagComponent : BaseMonoBehaviour
    {
        [Header(" [ Grant GameplayTag Component ] ")]
        [SerializeField] private GameObject _Target;
        [Space(5f)]
        [SerializeField] private GameplayTag[] _TriggerTags;

        [Header(" [ Auto Playing ] ")]
        [SerializeField] private bool _AutoPlaying;

        private IGameplayTagSystem _GameplayTagSystem;

#if UNITY_EDITOR
        private void Reset()
        {
            Setup();
        }

        private void OnValidate()
        {
            if (_Target)
            {
                if (_Target.TryGetComponentInParentOrChildren(out _GameplayTagSystem))
                {
                    if (_Target != _GameplayTagSystem.gameObject)
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
            if (_AutoPlaying)
                OnTriggerTag();
        }

        private void Setup()
        {
            if (_GameplayTagSystem is not null)
                return;

            if (gameObject.TryGetComponentInParentOrChildren(out _GameplayTagSystem))
            {
                _Target = _GameplayTagSystem.gameObject;
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
            _Target = null;

            _GameplayTagSystem = gameplayTagSystem;

            if (_GameplayTagSystem is not null)
            {
                _Target = _GameplayTagSystem.gameObject;
            }
        }

        public void OnTriggerTag(GameplayTag triggerTag)
        {
            Log("On Trigger GameplayTags");

            if (_GameplayTagSystem is null)
            {
                Log("GamepalyTag System Is Null", true);

                return;
            }

            _GameplayTagSystem.TriggerTag(triggerTag);
        }

        public void OnTriggerTag()
        {
            Log("On Trigger GameplayTags");

            if (_GameplayTagSystem is null)
            {
                Log("GamepalyTag System Is Null", true);

                return;
            }

            _GameplayTagSystem.TriggerTags(_TriggerTags);
        }
    }
}
