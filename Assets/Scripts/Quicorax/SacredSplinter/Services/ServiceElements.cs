using System;
using Quicorax.SacredSplinter.Models;
using Quicorax.SacredSplinter.Services.EventBus;
using UnityEngine;

namespace Quicorax.SacredSplinter.Services
{
    [Serializable]
    public class ServiceElements
    {
        public Transform PopUpTransformParent;
        public StringEventBus OnPlayerDeath;

        public ServiceElements(Transform popUpTransformParent,
            StringEventBus onPlayerDeath)
        {
            PopUpTransformParent = popUpTransformParent;
            OnPlayerDeath = onPlayerDeath;
        }
    }
}