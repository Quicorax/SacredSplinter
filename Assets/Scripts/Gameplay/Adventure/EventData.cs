using System;

[Serializable]
public class EventData
{
    public string Header;
    public string Action;
    public int Chance;

    public string FailHeader;
    public string FailKind;
    public int FailAmount;

    public string SuccedHeader;
    public string SuccedKind;
    public int SuccedMinAmount;
    public int SuccedMaxAmount;
}
