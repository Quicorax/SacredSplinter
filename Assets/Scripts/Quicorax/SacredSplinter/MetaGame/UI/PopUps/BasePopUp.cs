﻿using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Quicorax.SacredSplinter.MetaGame.UI.PopUps
{
    public abstract class BasePopUp : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        [SerializeField] internal CanvasGroup _canvasGroup;

        private Action<Button> _onClosePopUp;
        private PopUpLauncher _selfPopUp;

        public void BaseInitialize(PopUpLauncher popUpBundle, Action<Button> onClosePopUp)
        {
            _selfPopUp = popUpBundle;
            _onClosePopUp = onClosePopUp;
            
            _closeButton?.onClick.AddListener(CloseSelf);

            _canvasGroup.DOFade(0, 0.2f).From();
        }

        protected virtual void CloseSelf()
        {
            _canvasGroup.DOFade(0, 0.2f).OnComplete(() => Destroy(gameObject));

            _onClosePopUp?.Invoke(_selfPopUp.Button);
        }
    }
}