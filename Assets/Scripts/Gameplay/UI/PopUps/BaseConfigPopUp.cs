using UnityEngine;

public class BaseConfigPopUp : BasePopUp
{
    [SerializeField]
    private GameObject _audioON;

    private GameProgressionService _progression;

    internal void SetSound(GameProgressionService progression)
    {
        _progression = progression;

        TurnAudio(_progression.CheckSoundOff());
    }

    public void ToggleAudio()
    {
        bool audio = !_progression.CheckSoundOff();

        _progression.SetSoundOff(audio);

        TurnAudio(audio);
    }

    private void TurnAudio(bool isOn) => _audioON.SetActive(isOn);
}
