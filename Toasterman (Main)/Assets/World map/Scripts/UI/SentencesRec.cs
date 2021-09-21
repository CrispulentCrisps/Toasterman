using UnityEngine;
[System.Serializable]

public class SentencesRec
{
    public string Words;
    public bool ToastIn;
    public bool AndrussIn;
    [Range(0,8)]
    public int ToastEmote;
    [Range(-1, 8)]//-1 is for entering
    public int AndrussEmote;
}
