using UnityEngine;

public class BaseConfigPopUp : BasePopUp
{
    [SerializeField]
    private GameObject _audioON;

    private void Start()
    {
        TurnAudio(PlayerPrefs.GetInt("AudioMute") == 0);
    }

    public void ToggleAudio()
    {
        int music = PlayerPrefs.GetInt("AudioMute");

        TurnAudio(music == 1);

        PlayerPrefs.SetInt("AudioMute", music == 0 ? 1 : 0);
    }

    private void TurnAudio(bool isOn)
    {
        //TODO: GameManager.Instance.TurnAudioON(isOn);
        _audioON.SetActive(isOn);
    }
}
