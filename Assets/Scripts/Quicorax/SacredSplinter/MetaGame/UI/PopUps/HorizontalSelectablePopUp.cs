using System;
using DG.Tweening;
using Quicorax.SacredSplinter.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quicorax.SacredSplinter.MetaGame.UI.PopUps
{
    public abstract class HorizontalSelectablePopUp : BasePopUp
    {
        [SerializeField] private TMP_Text _header, _description, _index;
        [SerializeField] private Image _image;
        
        [SerializeField] private Button _select, _next, _previous, _close;

        protected AdventureConfigurationService _adventureConfiguration;
        
        private Action _onCancel;
        protected Action<string> _onSelect;
        
        protected int ActualIndex = 0;
        
        private int _listCount;
        
        protected abstract void ElementChanged();
        protected abstract void OnMiddleOfFade();
        protected abstract void SelectElement();

        public virtual void Initialize(Action<string> onSelect = null, Action onCancel = null)
        {
            SetButtons();

            _onCancel = _onCancel;
            _onSelect = onSelect;

            _adventureConfiguration = ServiceLocator.GetService<AdventureConfigurationService>();
        }
        
        protected void SetListCount(int listCount) => _listCount = listCount;

        protected void ChangeElement(bool next)
        {
            if (next)
            {
                if (ActualIndex < _listCount - 1)
                    ActualIndex++;
                else
                    ActualIndex = 0;
            }
            else
            {
                if (ActualIndex > 0)
                    ActualIndex--;
                else
                    ActualIndex = _listCount - 1;
            }

            ElementChanged();
        }

        protected void PrintElementData(string header, string description)
        {
            FadeAnim(_index, () => _index.text = ActualIndex.ToString());
            FadeAnim(_header, () => _header.text = header);
            FadeAnim(_description, () => _description.text = description);
            FadeAnim(_image,
                () => _image.sprite = ServiceLocator.GetService<ElementImagesService>()
                    .GetViewImage(header));
        }
        
        protected override void CloseSelf()
        {
            _onCancel?.Invoke();
            base.CloseSelf();
        }
        
        private void SetButtons()
        {
            _select?.onClick.AddListener(SelectElement);
            _next.onClick.AddListener(NextElement);
            _previous.onClick.AddListener(PreviousElement);
            _close.onClick.AddListener(CloseSelf);
        }
        private void NextElement() => ChangeElement(true);
        private void PreviousElement() => ChangeElement(false);
        
        private void FadeAnim(MaskableGraphic objectToFade, Action onFullFaded)
        {
            objectToFade.DOFade(0, 0.2f).OnComplete(() =>
            {
                OnMiddleOfFade();
                onFullFaded?.Invoke();
                objectToFade.DOFade(1, 0.2f);
            });
        }
    }
}