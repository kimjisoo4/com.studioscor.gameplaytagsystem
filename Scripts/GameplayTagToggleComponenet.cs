using UnityEngine;

namespace KimScor.GameplayTagSystem
{
    public class GameplayTagToggleComponenet : MonoBehaviour
    {
        private GameplayTagSystem _GameplayTagSystem;

        [SerializeField] private GameplayTag[] _ToggleTags;

        private void OnEnable()
        {
            _GameplayTagSystem = GetComponentInParent<GameplayTagSystem>();

            AddGameplayTags();
        }
        private void OnDisable()
        {
            _GameplayTagSystem = null;

            RemoveGameplayTags();
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
