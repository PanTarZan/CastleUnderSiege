using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CUS_Save_Displayer : MonoBehaviour
{
    public RectTransform contentHandler;
    public GameObject buttonPrefab;
    public GameObject textPrefab;
    public List<string> saveFilesPath = new List<string>();
    public CUS_UI_Screen levelSelectScreen;
    public InputField newAccountName;
    // Start is called before the first frame update
    void Start()
    {
        GetSaveFileNames();
        PopulateSaveFiles();
    }




    // Update is called once per frame
    void Update()
    {

    }

    private void PopulateSaveFiles()
    {
        for (int i = 0; i <= saveFilesPath.Count-1; i++)
        {
            GameObject _acc = new GameObject("Save_" + i);
            _acc.transform.SetParent(transform);
            _acc.AddComponent<Account>().LoadGame(saveFilesPath[i]);
        }
    }

    private void GetSaveFileNames()
    {
        string path = Application.persistentDataPath;
        var files = Directory.GetFiles(path);

        foreach (var file in files)
        {
            if (file.EndsWith(".kappa"))
            {

                saveFilesPath.Add(file);
            }
        }
    }

    public void DisplaySaveInfo()
    {
        foreach (Transform child in contentHandler)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in transform)
        {
            Account acc = child.GetComponent<Account>();
            GameObject button = Instantiate(buttonPrefab);
            button.transform.SetParent(contentHandler);
            button.GetComponent<Button>().onClick.AddListener(() => ClickWithLoadGame(acc));
            button.GetComponent<Button>().onClick.AddListener(() => FindObjectOfType<CUS_UI_System>().SwitchScreens(levelSelectScreen));


            GameObject name = Instantiate(textPrefab);
            name.transform.SetParent(button.transform);
            var name_txt = name.GetComponent<Text>();
            name_txt.text = "save: "+acc.AccountName;

            GameObject time = Instantiate(textPrefab);
            time.transform.SetParent(button.transform);
            var time_txt = time.GetComponent<Text>();
            time_txt.text = "time: " + acc.timeSpent.ToString();

            GameObject levels = Instantiate(textPrefab);
            levels.transform.SetParent(button.transform);
            var levels_txt = levels.GetComponent<Text>();
            levels_txt.text = "levels: " + acc.levelsUnlocked.ToString();

            GameObject kills = Instantiate(textPrefab);
            kills.transform.SetParent(button.transform);
            var kills_txt = kills.GetComponent<Text>();
            kills_txt.text = "kills: " + acc.enemiesKilled.ToString();

            GameObject gold = Instantiate(textPrefab);
            gold.transform.SetParent(button.transform);
            var gold_txt = gold.GetComponent<Text>();
            gold_txt.text = "gold: " + acc.gold.ToString();
        }
    }

    public void ClickWithLoadGame(Account account)
    {

        
        var playerAccount = FindObjectOfType<CurrentPlayerAccount>();

        playerAccount.AccountName = account.AccountName;
        playerAccount.timeSpent = account.timeSpent;
        playerAccount.levelsUnlocked = account.levelsUnlocked;
        playerAccount.enemiesKilled = account.enemiesKilled;
        playerAccount.gold = account.gold;

    }
    public void CreateNewSave()
    {
        var path = Application.persistentDataPath+ '/' + newAccountName.text+".kappa";
        CUS_Save_system.SaveAccountData(new Account(newAccountName.text), path);
        var playerAccount = FindObjectOfType<CurrentPlayerAccount>();
        playerAccount.LoadGame(path); 
    }
}
