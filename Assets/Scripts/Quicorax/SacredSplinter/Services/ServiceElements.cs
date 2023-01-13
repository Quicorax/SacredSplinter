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
        public InitialResources InitialResources;
        public Transform PopUpTransformParent;
        public StringEventBus OnPlayerDeath;

        public ServiceElements(InitialResources initialResources, Transform popUpTransformParent,
            ViewElementsModel viewElements, StringEventBus onPlayerDeath)
        {
            InitialResources = initialResources;
            PopUpTransformParent = popUpTransformParent;
            ViewElements = viewElements;
            OnPlayerDeath = onPlayerDeath;
        }
    }
}