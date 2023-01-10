using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Models;
using Quicorax.SacredSplinter.Services;

namespace Quicorax.SacredSplinter.MetaGame.Quests
{
    public class QuestsPopUp : VerticalSelectablePopUp
    {
        protected override void SpawnElements()
        {
            var quests = ServiceLocator.GetService<ModelsService>().GetModel<QuestModel>("Quests");

            foreach (var quest in quests.Quests)
                InstanceElement<Quest>(View).Initialize(quest, UpdateUI);
        }
    }
}