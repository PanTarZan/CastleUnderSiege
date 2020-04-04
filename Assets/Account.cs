using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Account : MonoBehaviour
{
    public string AccountName;
    public int timeSpent;
    public int levelsUnlocked;
    public int enemiesKilled;
    public int gold;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("SaveSystem");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void SaveGame()
    {
        CUS_Save_system.SaveAccountData(this);
    }

    public void CreateCleanSaveFile()
    {
        CUS_Save_system.SaveAccountData(null);
    }

    public void LoadGame()
    {
        AccountData data = CUS_Save_system.LoadAccountData();
        AccountName = data.name;
        timeSpent = data.timeSpent;
        levelsUnlocked = data.levelsUnlocked;
        enemiesKilled = data.enemiesKilled;
        gold = data.gold;
    }
}
