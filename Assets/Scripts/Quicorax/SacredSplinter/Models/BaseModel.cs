using System.Collections.Generic;
using Quicorax.SacredSplinter.Models;

namespace Quicorax.SacredSplinter.Services
{
    public class BaseModel : IModel
    {
        public List<BaseData> Entries = new();
    }
}