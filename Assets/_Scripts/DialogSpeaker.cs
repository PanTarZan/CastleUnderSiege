using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "DialogSpeaker", menuName = "ScriptableObjects/DialogSpeakers", order = 1)]
public class DialogSpeaker : ScriptableObject
{
    public string name;
    public Sprite graphic;
    public Color borderColor;
    public string screenPosition;
}
