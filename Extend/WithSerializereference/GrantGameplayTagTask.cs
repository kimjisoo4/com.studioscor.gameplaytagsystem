using StudioScor.Utilities;
using UnityEngine;

namespace StudioScor.GameplayTagSystem.Extend.TaskSystem
{
    [System.Serializable]
    public class GrantGameplayTagsTask : Task, ISubTask
    {
        [Header(" [ Grant Gameplay Tag Task ] ")]
        [SerializeField] private FGameplayTags _grantTags;
        [SerializeField][Range(0f, 1f)] private float _startTime = 0.2f;
        [SerializeField][Range(0f, 1f)] private float _endTime = 0.8f;

        public bool IsFixedUpdate => false;

        private IGameplayTagSystem _gameplayTagSystem;
        private GrantGameplayTagsTask _original;
        private float _start;
        private float _end;
        private bool _wasGranted;

        protected override void SetupTask()
        {
            base.SetupTask();

            _gameplayTagSystem = Owner.GetGameplayTagSystem();
        }
        public override ITask Clone()
        {
            var clone = new GrantGameplayTagsTask();

            clone._original = this;

            return clone;
        }

        protected override void EnterTask()
        {
            base.EnterTask();

            _wasGranted = false;

            _start = _original is null ? _startTime : _original._startTime;
            _end = _original is null ? _endTime : _original._endTime;
        }

        protected override void ExitTask()
        {
            base.ExitTask();

            RemoveGameplayTag();
        }

        public void UpdateSubTask(float normalizedTime)
        {
            if(!_wasGranted)
            {
                if (_start <= normalizedTime)
                {
                    GrantGameplayTag();
                }
            }
            else
            {
                if(normalizedTime >= _end)
                {
                    EndTask();
                }
            }
        }

        private void GrantGameplayTag()
        {
            if (_wasGranted)
                return;

            _wasGranted = true;

            _gameplayTagSystem.GrantGameplayTags(_original is null ? _grantTags : _original._grantTags);
        }
        private void RemoveGameplayTag()
        {
            if (!_wasGranted)
                return;

            _wasGranted = false;

            _gameplayTagSystem.RemoveGameplayTags(_original is null ? _grantTags : _original._grantTags);
        }

        public void FixedUpdateSubTask(float normalizedTime)
        {
            return;
        }
    }
}
