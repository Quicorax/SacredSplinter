using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EncyclopediaPopUp : HorizontalSelectablePopUp
{
    [SerializeField]
    private TMP_Text _location, _descriptionText, _unknownDescriptionText;

    [SerializeField]
    private Image _artImage;

    private bool _elementDiscovered;

    private GameProgressionService _progress;

    private Color _darkGray = new(0.12f, 0.12f, 0.12f);

    private void Start()
    {
        _progress = ServiceLocator.GetService<GameProgressionService>();
        Model = ServiceLocator.GetService<ModelsService>().GetEncyclopediaModel();

        ElementChanged();
    }

    public void ElementChanged() =>
        _elementDiscovered = _progress.CheckEnemyDiscovered(CurrentElement.Header);

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