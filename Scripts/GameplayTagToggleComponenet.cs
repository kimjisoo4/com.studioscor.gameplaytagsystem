using UnityEngine;

namespace KimScor.GameplayTagSystem
{
    public class GameplayTagToggleComponenet : MonoBehaviour
    {
        private GameplayTagSystem _GameplayTagSystem;
        [SerializeField] private bool _AutoActivate = true;

        [SerializeField] private GameplayTag[] _ToggleTags;

        private void OnEnable()
        {
            _GameplayTagSystem = GetComponentInParent<GameplayTagSystem>();

            if(_AutoActivate)
                AddGameplayTags();
        }
        private void OnDisable()
        {
            RemoveGameplayTags();

            if(_AutoActivate)
                _GameplayTagSystem = null;
        }

        public void AddGameplayTags()
        {
            if (_GameplayTagSystem is null)
                return;

            _GameplayTagSystem.AddOwnedTags(_ToggleTags);
        }
        public void RemoveGameplayTags()
        {
            if (_GameplayTagSystem is null)
                return;

            _GameplayTagSystem.RemoveOwnedTags(_ToggleTags);
        }
    }
}
