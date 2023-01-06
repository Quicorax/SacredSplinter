using Quicorax;
using UnityEngine;

public class VerticalSelectablePopUp : BasePopUp
{
    [SerializeField]
    private SimpleEventBus _onResourcesUpdated;

    public GameObject View;

    [SerializeField]
    private Transform _elementsHolder;

    public void Initialize() => SpawnElements();

    internal virtual void SpawnElements() { }

    internal T InstanceElement<T>(GameObject view) => Instantiate(view, _elementsHolder).GetComponent<T>();
    internal void UpdateUI() => _onResourcesUpdated.NotifyEvent();
}
