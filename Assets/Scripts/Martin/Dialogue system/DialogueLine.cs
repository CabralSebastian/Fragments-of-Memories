using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string Speaker;
    [TextArea] public string Text;
    public AudioClip clip;
}
