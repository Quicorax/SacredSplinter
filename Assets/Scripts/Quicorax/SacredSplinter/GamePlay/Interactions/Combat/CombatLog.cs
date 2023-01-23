using System;
using System.Collections.Generic;
using UnityEngine;

namespace Quicorax.SacredSplinter.GamePlay.Interactions.Combat
{
    public class CombatLog : MonoBehaviour
    {
        [SerializeField] private LogMessage _logObject;

        private List<LogMessage> _currentLogs = new();

        public void SetCombatLog(string who, string target, int amount, string how) =>
            PrintLog($"<b><uppercase>{who}</b></uppercase> damaged <b><uppercase>{target}</b></uppercase> for <b><color=#810000>{amount.ToString()}HP</b></color> with <b><i>{how}</b></i>.");

        public void SkipTurnLog(string who) =>
            PrintLog($"<b><uppercase>{who}</b></uppercase> skipped the turn.");

        private void PrintLog(string message)
        {
            var log = Instantiate(_logObject, transform);
            
            log.InitializeData(message);
            _currentLogs.Add(log);
        }

        private void OnDestroy()
        {
            foreach (var log in _currentLogs)
                log.Dispose();
            
            _currentLogs.Clear();
        }
    }
}