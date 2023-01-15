﻿using System.Collections.Generic;
using Quicorax.SacredSplinter.GamePlay.Interactions.Combat;
using UnityEngine;

namespace Quicorax.SacredSplinter.Models
{
    public class EnemiesDataModel: IModel
    {
        public List<EnemyData> EnemiesData = new();

        public EnemyData GetRandomEnemy() => EnemiesData[Random.Range(0, EnemiesData.Count)];
    }
}