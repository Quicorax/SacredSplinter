using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Services;

namespace Quicorax.SacredSplinter.MetaGame.Quests
{
    public class QuestsPopUp : VerticalSelectablePopUp
    {
        public override void SpawnElements(IAddressablesService addressables, IGameConfigService config, IGameProgressionService progression)
        {
            base.SpawnElements(addressables, config, progression);
            
            foreach (var quest in Config.Quests)
            {
                Addressables.LoadAddrssComponentObject<QuestElement>("QuestElement", _elementsHolder, questData =>
                    questData.Initialize(progression, addressables, quest, UpdateUI).ManageTaskException());
            }
        }
    }
}