using DG.Tweening;
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

        private bool _elementDiscovered;

        private GameProgressionService _gameProgression;

        private readonly Color _darkGray = new(0.12f, 0.12f, 0.12f);

        private void Start()
        {
            _gameProgression = ServiceLocator.GetService<GameProgressionService>();
            Model = ServiceLocator.GetService<ModelsService>().GetModel<BaseModel>("Encyclopedia"); //TODO: ¿¿

            ElementChanged();
        }

        public void ElementChanged() =>
            _elementDiscovered = _gameProgression.GetEnemyDiscovered(CurrentElement.Header);

        public override void OnMiddleOfFade()
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
    }
}