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
}

public class HorizontalSelectablePopUp : BasePopUp
{
    internal BaseModel Model;
    internal int ActualIndex = 0;

    [SerializeField]
    private TMP_Text _header, _description, _index;
    [SerializeField]
    private Image _image;

    private Dictionary<int, BaseData> _elements = new();

    internal BaseData CurrentElement;

    internal Action<BaseData> OnSelect;
    private Action _onCancel;

    public void Initialize(BaseModel model, Action<BaseData> onSelect, Action onCancel)    
    {
        Model = model;
        OnSelect = onSelect;
        _onCancel = onCancel;


        for (int i = 0; i < model.Entries.Count; i++)
            _elements.Add(i, model.Entries[i]);

        PrintElementData();
    }

    public override void CloseSelf()
    {
        _onCancel?.Invoke();
        base.CloseSelf();
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
        FadeAnim(_image, () => _image.sprite = ServiceLocator.GetService<ImagesService>().GetViewImage(CurrentElement.Header));
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
