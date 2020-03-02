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
    //public CUS_UI_Screen screenAfterDialog;
    [SerializeField] public Camera dialogCamera;
    [SerializeField] public GameObject mainCamera;
    [SerializeField] public AudioClip DialogMusic;

    [Header("UI Panels")]
    [SerializeField] public Canvas dialogCanvas;
    [SerializeField] public GameObject namePanel;
    [SerializeField] public GameObject textPanel;
    [SerializeField] public GameObject characterGraphic;


    [Header("Dialog Entries")]
    public DialogObject[] listOfLines;
    private Queue<string> sentences;
    private Queue<DialogObject> dialogs;

    public void BeginDialog()
    {
        OnDialogBegin.Invoke();
        /*
        var ac = GetComponent<AudioSource>();
        ac.clip = DialogMusic;
        ac.Play(); 
        */

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
        //namePanel.GetComponent<Image>().color = dialogEntry.Character.borderColor;
        //textPanel.GetComponent<Image>().color = dialogEntry.Character.borderColor;
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
        OnDialogFinish.Invoke();
        /*
        FindObjectOfType<HeadQuaters>().GetComponent<HeadQuaters>().enabled = true;
        FindObjectOfType<CUS_UI_System>().SwitchScreens(screenAfterDialog);

        var ac = GetComponent<AudioSource>();
        ac.Stop();
        ac.clip = gameObject.GetComponent<LevelManagement>().StageMusic;
        ac.Play();
        
        mainCamera.SetActive(true);
        dialogCamera.gameObject.SetActive(false);
        */
    }
}

