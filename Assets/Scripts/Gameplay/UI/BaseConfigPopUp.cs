using UnityEngine;

public class BaseConfigPopUp : BasePopUp
{
    [SerializeField]
    private GameObject _sfxMuted, _musicMuted;

    private void Start()
    {
        TurnSound("MusicMute", PlayerPrefs.GetInt("MusicMute") == 0);
        TurnSound("SFXMute", PlayerPrefs.GetInt("SFXMute") == 0);
    }

    public void ToggleSound(string element)
    {
        int music = PlayerPrefs.GetInt(element);

        TurnSound(element, music == 1);

        PlayerPrefs.SetInt(element, music == 0 ? 1 : 0);
    }

    private void TurnSound(string element, bool isOn)
    {
        //GameManager.Instance.TurnSFXON(element, isOn);

        switch (element)
        {
            case "SFXMute":
                _sfxMuted.SetActive(!isOn);
                break;
            case "MusicMute":
                _musicMuted.SetActive(!isOn);
                break;
        }
    }
}
