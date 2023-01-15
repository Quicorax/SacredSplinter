using System.Collections.Generic;
using Quicorax.SacredSplinter.GamePlay.Interactions.Combat;
using UnityEngine;

namespace Quicorax.SacredSplinter.Models
{
    public class EnemiesDataModel: IModel
    {
        public List<EnemiesData> EnemiesData = new();

        public EnemiesData GetRandomEnemy() => EnemiesData[Random.Range(0, EnemiesData.Count)];
    }
}