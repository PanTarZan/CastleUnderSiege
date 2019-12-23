using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManagement : MonoBehaviour
{
    public HeadQuaters HQ;
    public bool isIntroFinished = false;

    public int currentLineNumber = 0;
    public List<GameObject> rozmowcy;
    public List<DialogObject> listOfLines;
    [SerializeField] public Camera dialogCamera;

    public bool isGamePaused = false;

    [SerializeField] public Canvas dialogCanvas;
    [SerializeField] public GameObject namePanel;
    [SerializeField] public GameObject textPanel;
    [SerializeField] public GameObject characterGraphic;
    [SerializeField] public Sprite[] availableGraphics;

    [SerializeField] public Canvas gameUI;
    [SerializeField] public GameObject enemiesToEnable;
    [SerializeField] public Camera mainCamera;

    public GameObject PausedScreen;

    // Start is called before the first frame update
    void Start()
    {
        isGamePaused = true;
        LoadDialogFile();
        StartDialog();

    }

    private void LoadDialogFile()
    {
        DialogManager DM = new DialogManager();
        listOfLines = DM.LoadDialogXML("test-doc.xml");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isGamePaused = !isGamePaused;
            if (isIntroFinished)
            {
                ShowPausedScreen();
            }
        }

        CheckGameState();
        if (currentLineNumber < listOfLines.Count)
        {
            CheckIfDialogFinished();
            UpdateDialogUI(currentLineNumber);
            if (Input.GetKeyDown(KeyCode.Space) && !isIntroFinished)
            {
                MoveToNextDialogLine();
            }
        }
        else
        {
            if (!isIntroFinished)
            {
                 EndDialogSequence();
            }
        }
        
    }

    private void UpdateDialogUI(int currentLineentryNumber)
    {
        namePanel.GetComponentInChildren<Text>().text = listOfLines[currentLineNumber].Name;
        textPanel.GetComponentInChildren<Text>().text = listOfLines[currentLineNumber].Text;

        foreach (var graphic in availableGraphics)
        {
            if (graphic.name == listOfLines[currentLineNumber].Graphic)
            {
                characterGraphic.GetComponent<Image>().sprite = graphic;
            }

        }
    }

    private void MoveToNextDialogLine()
    {
        currentLineNumber++;
    }

    private void EndDialogSequence()
    {
        dialogCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
        isGamePaused = false;
        dialogCanvas.gameObject.SetActive(false);
        gameUI.gameObject.SetActive(true);
        isIntroFinished = true;
    }

    private void StartDialog()
    {
        dialogCanvas.gameObject.SetActive(true);
        dialogCamera.gameObject.SetActive(true);
        var opponents = dialogCanvas.GetComponents<Image>();
        
        foreach (var opponent in opponents)
        {
            rozmowcy.Add(opponent.gameObject);
        }

        //rozmowcy[numerRozmowcy].SetActive(true);
        


    }

    private void CheckIfDialogFinished()
    {
        if (isIntroFinished)
        {
            isGamePaused = false;
        }
        else
        {
            isGamePaused = true;
        }
    }


    private void ShowPausedScreen()
    {
        PausedScreen.SetActive(isGamePaused);
    }

    private void CheckGameState()
    {

        if (isGamePaused)
        {
            Time.timeScale = 0;
            

        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
