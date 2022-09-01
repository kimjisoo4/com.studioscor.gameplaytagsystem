using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Collections.Generic;

namespace KimScor.GameplayTagSystem
{
    public class GameplayTagSystemDebuger : MonoBehaviour
    {
        [SerializeField] private GameplayTagSystem _GameplayTagSystem;
        [SerializeField] private GridLayoutGroup _Grid;
        [SerializeField] private GameplayTagBlockUI _Block;
        [SerializeField] private List<GameplayTagBlockUI> _Blocks;
        [SerializeField] private bool _LookBlockTag = false;

#if UNITY_EDITOR
        private void Reset()
        {
            _Grid = GetComponentInChildren<GridLayoutGroup>();
            _Block = GetComponentInChildren<GameplayTagBlockUI>();
        }
#endif
        private void OnEnable()
        {
            GameplayTagSystem_UpdateTag(_GameplayTagSystem);

            _GameplayTagSystem.OnAddBlockTag += GameplayTagSystem_UpdateTag;
            _GameplayTagSystem.OnAddOwnedTag += GameplayTagSystem_UpdateTag;
            _GameplayTagSystem.OnRemoveBlockTag += GameplayTagSystem_UpdateTag;
            _GameplayTagSystem.OnRemoveOwnedTag += GameplayTagSystem_UpdateTag;
        }
        private void OnDisable()
        {
            _GameplayTagSystem.OnAddBlockTag -= GameplayTagSystem_UpdateTag;
            _GameplayTagSystem.OnAddOwnedTag -= GameplayTagSystem_UpdateTag;
            _GameplayTagSystem.OnRemoveBlockTag -= GameplayTagSystem_UpdateTag;
            _GameplayTagSystem.OnRemoveOwnedTag -= GameplayTagSystem_UpdateTag;
        }

        public void SetGameplayTagSystem(GameplayTagSystem gameplayTagSystem)
        {
            if(_GameplayTagSystem != null)
            {
                _GameplayTagSystem.OnAddBlockTag -= GameplayTagSystem_UpdateTag;
                _GameplayTagSystem.OnAddOwnedTag -= GameplayTagSystem_UpdateTag;
                _GameplayTagSystem.OnRemoveBlockTag -= GameplayTagSystem_UpdateTag;
                _GameplayTagSystem.OnRemoveOwnedTag -= GameplayTagSystem_UpdateTag;
            }

            _GameplayTagSystem = gameplayTagSystem;

            if (_GameplayTagSystem != null)
            {
                _GameplayTagSystem.OnAddBlockTag += GameplayTagSystem_UpdateTag;
                _GameplayTagSystem.OnAddOwnedTag += GameplayTagSystem_UpdateTag;
                _GameplayTagSystem.OnRemoveBlockTag += GameplayTagSystem_UpdateTag;
                _GameplayTagSystem.OnRemoveOwnedTag += GameplayTagSystem_UpdateTag;
            }

            UpdateGameplayTagBlock();
        }

        [ContextMenu("ChangeLook")]
        public void ChangeLook()
        {
            _LookBlockTag = !_LookBlockTag;

            UpdateGameplayTagBlock();
        }

        private void UpdateGameplayTagBlock()
        {
            if(_GameplayTagSystem == null)
            {
                foreach (var block in _Blocks)
                {
                    block.gameObject.SetActive(false);
                }

                return;
            }

            int count = 0;

            IReadOnlyDictionary<GameplayTag, int> container = !_LookBlockTag ? _GameplayTagSystem.OwnedTags : _GameplayTagSystem.BlockTags;

            if (container.Count == 0)
                return;

            foreach (KeyValuePair<GameplayTag, int> tag in container)
            {
                if (_Blocks.Count > count)
                {
                    _Blocks[count].SetText(tag);

                    _Blocks[count].gameObject.SetActive(true);
                }
                else
                {
                    var newBlock = Instantiate(_Block, _Grid.transform);

                    newBlock.SetText(tag);

                    newBlock.gameObject.SetActive(true);

                    _Blocks.Add(newBlock);
                }

                count++;
            }

            for (int i = count; i < _Blocks.Count; i++)
            {
                _Blocks[i].gameObject.SetActive(false);
            }
        }

        private void GameplayTagSystem_UpdateTag(GameplayTagSystem gameplayTagSystem, GameplayTag changedTag = null)
        {
            UpdateGameplayTagBlock();
        }
    }
}
