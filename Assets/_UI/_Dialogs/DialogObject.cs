using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogObject
{
    public DialogSpeaker Character;

    [TextArea(3, 10)]
    public string[] text;
}
