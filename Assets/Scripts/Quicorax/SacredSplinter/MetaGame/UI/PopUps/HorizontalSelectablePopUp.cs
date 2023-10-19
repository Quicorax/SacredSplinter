using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using DG.Tweening;
using Quicorax.SacredSplinter.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Quicorax.SacredSplinter.MetaGame.UI.PopUps
{
    public abstract class HorizontalSelectablePopUp : BasePopUp
    {
        [SerializeField] private TMP_Text _header, _description, _index;
        [SerializeField] private Image _image;

        [SerializeField] private Button _select, _next, _previous, _close;

        [Inject] protected IAdventureConfigurationService _adventureConfiguration;
        [Inject] private IAddressablesService _addressables;

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

            _onCancel = onCancel;
            _onSelect = onSelect;
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
            FadeAnim(_header, () => SetHeader(header));
            FadeAnim(_description, () => _description.text = description);
            FadeAnim(_image, () => SetSprites(header));
        }

        private void SetSprites(string header) => SetSpritesAsync(header).ManageTaskException();

        private async Task SetSpritesAsync(string header) =>
            _image.sprite = await _addressables.LoadAddrssAsset<Sprite>(header);

        protected override void CloseSelf()
        {
            _onCancel?.Invoke();
            base.CloseSelf();
        }

        protected virtual void SetHeader(string header)
        {
            _header.text = header;
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