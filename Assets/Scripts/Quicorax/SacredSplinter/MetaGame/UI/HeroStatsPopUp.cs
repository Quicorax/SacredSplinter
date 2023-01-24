using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Models;
using TMPro;
using UnityEngine;

namespace Quicorax.SacredSplinter.MetaGame.UI
{
    public sealed class HeroStatsPopUp : BasePopUp
    {
        [SerializeField] private TMP_Text _heroClass, _attackType;
        [SerializeField] private TMP_Text _baseHealth, _evoHealth, _baseDamage, _evoDamage, _baseSpeed, _evoSpeed;
        [SerializeField] private GameObject _canIgnoreEvents;
        [SerializeField] private GameObject _canEscapeCombats;

        public void SetData(HeroData heroData)
        {
            _heroClass.text = heroData.Header;
            _attackType.text = heroData.AttackType;
            _baseHealth.text = heroData.MaxHealth.ToString();
            _evoHealth.text = $"+ {heroData.HealthEvo.ToString()}";
            _baseDamage.text = heroData.Damage.ToString();
            _evoDamage.text = $"+ {heroData.DamageEvo.ToString()}";
            _baseSpeed.text = heroData.Speed.ToString();
            _evoSpeed.text = $"+ {heroData.SpeedEvo.ToString()}";

            _canIgnoreEvents.SetActive(heroData.CanIgnoreEvents);
            _canEscapeCombats.SetActive(heroData.CanEscapeCombats);
        }
    }
}