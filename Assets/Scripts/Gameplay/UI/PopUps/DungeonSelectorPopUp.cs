using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class DungeonData
{
    public string Header;
    public string Description;
    public Sprite Image;
}

public class DungeonSelectorPopUp : BasePopUp
{
    [SerializeField]
    private DungeonModel _dungeonModel;

    [SerializeField]
    private TMP_Text _header, _description, _index;
    [SerializeField]
    private Image _dungeonImage;
    [SerializeField]
    private GameObject _artifactCheck;

    private Dictionary<int, DungeonData> _dungeons = new();

    private int _actualDungeonIndex = 0;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        for (int i = 0; i < _dungeonModel.Entries.Count; i++)
            _dungeons.Add(i, _dungeonModel.Entries[i]);

        SetDungeon();
    }
    public void ChangeDungeon(bool next)
    {
        if (next)
        {
            if (_actualDungeonIndex < _dungeons.Count - 1)
                _actualDungeonIndex++;
            else
                _actualDungeonIndex = 0;
        }
        else
        {
            if (_actualDungeonIndex > 0)
                _actualDungeonIndex--;
            else
                _actualDungeonIndex = _dungeons.Count - 1;
        }

        SetDungeon();
    }

    public void SetDungeon()
    {
        DungeonData data = _dungeons[_actualDungeonIndex];

        _index.text = _actualDungeonIndex.ToString();

        _header.text = data.Header;
        _description.text = data.Description;
        _dungeonImage.sprite = data.Image;
    }

    public void SelectLocation()
    {
        Debug.Log("LOCATION SELECTED: " + _actualDungeonIndex);
        CloseSelf();
    }
}