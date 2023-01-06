using DG.Tweening;
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
    public BaseModel Model;

    [SerializeField]
    private TMP_Text _header, _description, _index;
    [SerializeField]
    private Image _image;

    private Dictionary<int, BaseData> _elements = new();

    internal int ActualIndex = 0;

    internal BaseData CurrentElement;

    internal Action<BaseData> OnSelect;
    private Action _onCancel;

    public void Initialize(Action<BaseData> onSelect, Action onCancel)
    {
        OnSelect = onSelect;
        _onCancel = onCancel;
    }
    public override void CloseSelf()
    {
        _onCancel?.Invoke();
        base.CloseSelf();
    }

    public override void BaseInitialize(Action onClosePopUp)
    {
        base.BaseInitialize(onClosePopUp);

        for (int i = 0; i < Model.Entries.Count; i++)
            _elements.Add(i, Model.Entries[i]);

        PrintElementData();
    }

    public void ChangeElement(bool next)
    {
        if (next)
        {
            if (ActualIndex < _elements.Count - 1)
                ActualIndex++;
            else
                ActualIndex = 0;
        }
        else
        {
            if (ActualIndex > 0)
                ActualIndex--;
            else
                ActualIndex = _elements.Count - 1;
        }

        PrintElementData();
    }

    private void PrintElementData()
    {
        CurrentElement = _elements[ActualIndex];

        FadeAnim(_index, ()=> _index.text = ActualIndex.ToString());
        FadeAnim(_header, ()=> _header.text = CurrentElement.Header);
        FadeAnim(_description , ()=>_description.text = CurrentElement.Description);
        FadeAnim(_image, () => _image.sprite = CurrentElement.Image);
    }

    private void FadeAnim(MaskableGraphic objectToFade, Action onFullFaded)
    {
        objectToFade.DOFade(0, 0.2f).OnComplete(() =>
        {
            OnMiddleOfFade();
            onFullFaded?.Invoke();
            objectToFade.DOFade(1, 0.2f);
        });
    }

    public virtual void OnMiddleOfFade() { }


}
