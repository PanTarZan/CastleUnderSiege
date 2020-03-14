using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [Header("Dialog Events")]
    public UnityEvent OnDialogBegin;
    public UnityEvent OnDialogFinish;

    [Header("Main Properties")]
    [SerializeField] AudioClip DialogMusic;

    [Header("UI Panels")]
    [SerializeField]  GameObject namePanel = null;
    [SerializeField]  GameObject textPanel = null;
    [SerializeField]  GameObject characterGraphic = null;


    [Header("Dialog Entries")]
    public DialogObject[] listOfLines;
    private Queue<string> sentences;
    private Queue<DialogObject> dialogs;
    private Coroutine currentCoroutine;

    public void BeginDialog()
    {
        OnDialogBegin.Invoke();

        sentences = new Queue<string>();
        dialogs = new Queue<DialogObject>();

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
        string actorName = dialogEntry.Character.character_name;
        
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
        namePanel.GetComponentInChildren<Text>().text = dialogEntry.Character.character_name;
    }

    public void DisplayNextSentence()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        if (sentences.Count == 0)
        {
            StartNextActor();
            return;
        }
        string sentence = sentences.Dequeue();
        currentCoroutine = StartCoroutine(UpdateTextPanel(sentence));
        
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
        StopCoroutine(currentCoroutine);
        OnDialogFinish.Invoke();
    }
    
}

