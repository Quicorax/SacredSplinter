using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Services;
using UnityEngine;

namespace Quicorax.SacredSplinter.MetaGame.Quests
{
    public class QuestsPopUp : VerticalSelectablePopUp
    {
        protected override void SpawnElements()
        {
            foreach (var quest in ServiceLocator.GetService<GameConfigService>().Quests)
                InstanceElement<Quest>(View).Initialize(quest, UpdateUI);
        }
    }
}