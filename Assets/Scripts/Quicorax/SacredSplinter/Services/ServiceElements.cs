using System;
using Quicorax.SacredSplinter.Models;
using Quicorax.SacredSplinter.Services.EventBus;
using UnityEngine;

namespace Quicorax.SacredSplinter.Services
{
    [Serializable]
    public class ServiceElements
    {
        public ViewElementsModel ViewElements;
        public Transform PopUpTransformParent;
        public StringEventBus OnPlayerDeath;

        public ServiceElements(Transform popUpTransformParent, ViewElementsModel viewElements,
            StringEventBus onPlayerDeath)
        {
            PopUpTransformParent = popUpTransformParent;
            ViewElements = viewElements;
            OnPlayerDeath = onPlayerDeath;
        }
    }
}