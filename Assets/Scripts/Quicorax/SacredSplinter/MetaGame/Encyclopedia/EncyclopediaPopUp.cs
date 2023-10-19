using System;
using System.Collections.Generic;
using DG.Tweening;
using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Models;
using Quicorax.SacredSplinter.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Quicorax.SacredSplinter.MetaGame.Encyclopedia
{
    public class EncyclopediaPopUp : HorizontalSelectablePopUp
    {
        [SerializeField] private TMP_Text _descriptionText, _unknownDescriptionText;
        [SerializeField] private Image _artImage;

        private readonly Color _darkGray = new(0.09f, 0.09f, 0.09f);

        private Dictionary<int, EnemyData> _enemies = new();

        [Inject] private IGameProgressionService _gameProgression;
        [Inject] private IGameConfigService _gameConfig;

        private bool _elementDiscovered;

        public override void Initialize(Action<string> onSelect = null, Action onCancel = null)
        {
            base.Initialize(onSelect, onCancel);

            var enemies = _gameConfig.Enemies;

            SetListCount(enemies.Count);

            for (var i = 0; i < enemies.Count; i++)
                _enemies.Add(i, enemies[i]);

            ElementChanged();
        }

        protected override void SetHeader(string header)
        {
            if (_elementDiscovered)
            {
                base.SetHeader(header);
                return;
            }

            base.SetHeader("Unknown");
        }

        protected override void ElementChanged()
        {
            var currentEnemy = _enemies[ActualIndex];

            _elementDiscovered = _gameProgression.GetEnemyDiscovered(currentEnemy.Header);
            PrintElementData(currentEnemy.Header, currentEnemy.Description);
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