using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Services;

namespace Quicorax.SacredSplinter.MetaGame.Quests
{
    public class QuestsPopUp : VerticalSelectablePopUp
    {
        protected override void SpawnElements()
        {
            foreach (var quest in ServiceLocator.GetService<GameConfigService>().Quests)
            {
                Addressables.LoadAddrssComponentObject<QuestElement>("QuestElement", _elementsHolder, questData =>
                    questData.Initialize(quest, UpdateUI, Progression, Addressables).ManageTaskException());
            }
        }
    }
}