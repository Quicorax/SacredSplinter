using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class EntryData
{
    public string Header;
    public string Description;
    public Sprite Image;
}
public class EncyclopediaPopUp : BasePopUp
{
    [SerializeField]
    private EncyclopediaModel _encyclopediaModel;

    [SerializeField]
    private TMP_Text _header, _description, _index;
    [SerializeField]
    private Image _entryImage;

    private Dictionary<int, EntryData> _encyclopedia = new();
    private int _actualEntryIndex = 0;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        for (int i = 0; i < _encyclopediaModel.Entries.Count; i++)
            _encyclopedia.Add(i, _encyclopediaModel.Entries[i]);

        SetEntry();
    }

    public void ChangeEntry(bool next)
    {
        if (next)
        {
            if (_actualEntryIndex < _encyclopedia.Count -1)
                _actualEntryIndex++;
            else
                _actualEntryIndex = 0;
        }
        else
        {
            if (_actualEntryIndex > 0)
                _actualEntryIndex--;
            else
                _actualEntryIndex = _encyclopedia.Count -1;
        }

        SetEntry();
    }

    public void SetEntry()
    {
        EntryData data = _encyclopedia[_actualEntryIndex];

        _index.text = _actualEntryIndex.ToString();
        _header.text = data.Header;
        _description.text = data.Description;
        _entryImage.sprite = data.Image;
    }
}