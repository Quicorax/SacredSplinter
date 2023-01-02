using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class BaseData
{
    public string Header;
    public string Description;
    public Sprite Image;
}

public class SelectorPopUp : BasePopUp
{
    [SerializeField]
    private BaseModel _model;

    [SerializeField]
    private TMP_Text _header, _description, _index;
    [SerializeField]
    private Image _image;

    private Dictionary<int, BaseData> _elements = new();

    private Action<BaseData> _onElementSelected;

    private int _actualElementIndex = 0;

    public BaseData CurrentElement;

    public override void Initialize(Action onClosePopUp, Action<BaseData> onElementSelected)
    {
        base.Initialize(onClosePopUp, onElementSelected);

        _onElementSelected = onElementSelected;

        for (int i = 0; i < _model.Entries.Count; i++)
            _elements.Add(i, _model.Entries[i]);

        PrintElementData();
    }

    public void ChangeElement(bool next)
    {
        if (next)
        {
            if (_actualElementIndex < _elements.Count - 1)
                _actualElementIndex++;
            else
                _actualElementIndex = 0;
        }
        else
        {
            if (_actualElementIndex > 0)
                _actualElementIndex--;
            else
                _actualElementIndex = _elements.Count - 1;
        }

        PrintElementData();
    }

    private void PrintElementData()
    {
        CurrentElement = _elements[_actualElementIndex];

        _index.text = _actualElementIndex.ToString();

        _header.text = CurrentElement.Header;
        _description.text = CurrentElement.Description;
        _image.sprite = CurrentElement.Image;
    }

    public virtual void SelectElement()
    {
        _onElementSelected?.Invoke(_model.Entries[_actualElementIndex]);
        CloseSelf();
    }
}
