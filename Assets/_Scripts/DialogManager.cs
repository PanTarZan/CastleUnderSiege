using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    
    [SerializeField] public Camera dialogCamera;
    [SerializeField] public GameObject mainCamera;

    [SerializeField] public Canvas dialogCanvas;
    [SerializeField] public Canvas gameUI;

    [SerializeField] public GameObject namePanel;
    [SerializeField] public GameObject textPanel;
    [SerializeField] public GameObject characterGraphic;
    

    public DialogObject[] listOfLines;

    private Queue<string> sentences;
    private Queue<DialogObject> dialogs;
    
    public void Start()
    {
        sentences = new Queue<string>();
        dialogs = new Queue<DialogObject>();
        BeginDialog();
    }

    private void BeginDialog()
    {
        dialogCanvas.gameObject.SetActive(true);

        dialogs.Clear();
        foreach (var actor in listOfLines)
        {
            dialogs.Enqueue(actor);
        }

        StartNextActor();
    }

    public void StartNextActor()
    {
        if (dialogs.Count == 0)
        {
            EndDialog();
            return;
        }

        var dialogEntry = dialogs.Dequeue();
        string actorName = dialogEntry.Character.name;

        Debug.Log("Starting talking with: " + actorName);
        SetActorUIProperties(dialogEntry);

        sentences.Clear();
        foreach (var sentence in dialogEntry.text)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    private void SetActorUIProperties(DialogObject dialogEntry)
    {
        characterGraphic.GetComponent<Image>().sprite = dialogEntry.Character.graphic;
        namePanel.GetComponentInChildren<Text>().text = dialogEntry.Character.name;
        namePanel.GetComponent<Image>().color = dialogEntry.Character.borderColor;
        textPanel.GetComponent<Image>().color = dialogEntry.Character.borderColor;
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            StartNextActor();
            return;
        }
        string sentence = sentences.Dequeue();
        StartCoroutine(UpdateTextPanel(sentence));
        Debug.Log(sentence);
    }

    private IEnumerator UpdateTextPanel(string sentence)
    {
        string text_to_display = "";
        foreach (var letter in sentence)
        {
            text_to_display += letter;
            textPanel.GetComponentInChildren<Text>().text = text_to_display;
            yield return null;
        }

    }

    public void EndDialog()
    {
        Debug.Log("EndDialog");
    }
}

