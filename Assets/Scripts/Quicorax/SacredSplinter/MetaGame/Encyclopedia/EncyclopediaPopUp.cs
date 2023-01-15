using System;
using System.Collections.Generic;
using DG.Tweening;
using Quicorax.SacredSplinter.GamePlay.Interactions.Combat;
using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quicorax.SacredSplinter.MetaGame.Encyclopedia
{
    public class EncyclopediaPopUp : HorizontalSelectablePopUp
    {
        [SerializeField] private TMP_Text _descriptionText, _unknownDescriptionText;
        [SerializeField] private Image _artImage;


        private readonly Color _darkGray = new(0.12f, 0.12f, 0.12f);

        private bool _elementDiscovered;

        private Dictionary<int, EnemiesData> _enemies = new();
        private EnemiesData _currentEnemy;

        private GameProgressionService _gameProgression;
        
        public override void Initialize(Action<string> onSelect = null, Action onCancel = null)
        {
            _gameProgression = ServiceLocator.GetService<GameProgressionService>();

            base.Initialize(onSelect, onCancel);

            var enemies = ServiceLocator.GetService<GameConfigService>().Enemies;

            SetListCount(enemies.Count);

            for (var i = 0; i < enemies.Count; i++)
                _enemies.Add(i, enemies[i]);

            ElementChanged();
        }

        protected override void ElementChanged()
        {
            _currentEnemy = _enemies[ActualIndex];
            _elementDiscovered = _gameProgression.GetEnemyDiscovered(_currentEnemy.Header);
            
            PrintElementData(_currentEnemy.Header, _currentEnemy.Description);
        }

        protected override void OnMiddleOfFade()
        {
            _artImage.color = _elementDiscovered ? Color.white : _darkGray;

            if (_elementDiscovered)
            {
                _descriptionText.gameObject.SetActive(true);
                _descriptionText.DOFade(1, 0.2f);


                _unknownDescriptionText.DOFade(0, 0.2f).OnComplete(() =>
                    _unknownDescriptionText.gameObject.SetActive(false));
            }
            else
            {
                _unknownDescriptionText.gameObject.SetActive(true);
                _unknownDescriptionText.DOFade(1, 0.2f);

                _descriptionText.DOFade(0, 0.2f).OnComplete(() =>
                    _descriptionText.gameObject.SetActive(false));
            }
        }

        protected override void SelectElement()
        {
        }
    }
}