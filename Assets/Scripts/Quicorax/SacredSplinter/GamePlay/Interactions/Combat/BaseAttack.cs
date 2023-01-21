using System;
using System.Threading.Tasks;
using Quicorax.SacredSplinter.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quicorax.SacredSplinter.GamePlay.Interactions.Combat
{
    public class BaseAttack : MonoBehaviour
    {
        [SerializeField] private Button _useButton;
        [SerializeField] private Image _attackIcon;
        [SerializeField] private TMP_Text _cooldown;

        private AttackData _attackData;
        private Action<AttackData> _onUse;

        private int _currentCooldownTurns;

        public void Initialize(AttackData attackData, Action<AttackData> onUse)
        {
            _attackData = attackData;
            _onUse = onUse;
            _useButton.onClick.AddListener(OnUse);

            //LoadSpriteAsync().ManageTaskException(); //TODO: Addressables sprite
        }

        private async Task LoadSpriteAsync() =>
            _attackIcon.sprite = await ServiceLocator.GetService<AddressablesService>()
                .LoadAddrssAsset<Sprite>(_attackData.Header);

        public void TryAwake()
        {
            if (_currentCooldownTurns > 0)
            {
                _cooldown.text = _currentCooldownTurns.ToString();
                _currentCooldownTurns--;
                return;
            }

            _cooldown.text = string.Empty;
            _useButton.interactable = true;
        }

        private void OnUse()
        {
            _currentCooldownTurns = _attackData.CooldownTurns;

            _cooldown.text = _currentCooldownTurns.ToString();
            _useButton.interactable = false;

            _onUse?.Invoke(_attackData);
        }
    }
}