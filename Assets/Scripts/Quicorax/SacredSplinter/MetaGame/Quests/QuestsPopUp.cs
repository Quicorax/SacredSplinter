using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Services;

namespace Quicorax.SacredSplinter.MetaGame.Quests
{
    public class QuestsPopUp : VerticalSelectablePopUp
    {
        protected override void SpawnElements()
        {
            var progression = ServiceLocator.GetService<GameProgressionService>();
            var addressables = ServiceLocator.GetService<AddressablesService>();

            foreach (var quest in ServiceLocator.GetService<GameConfigService>().Quests)
            {
                addressables.LoadAddrssComponentObject<QuestElement>("QuestElement", _elementsHolder, questData =>
                    questData.Initialize(quest, UpdateUI, progression, addressables).ManageTaskException());
            }
        }
    }
}