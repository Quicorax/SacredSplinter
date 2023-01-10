using System.Collections.Generic;
using Quicorax.SacredSplinter.Services;

namespace Quicorax.SacredSplinter.Models
{
    public class QuestModel : IModel
    {
        public List<QuestData> Quests = new();
    }
}