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

    public void LoadGame(string path)
    {
        AccountData data = CUS_Save_system.LoadAccountData(path);
        AccountName = data.name;
        timeSpent = data.timeSpent;
        levelsUnlocked = data.levelsUnlocked;
        enemiesKilled = data.enemiesKilled;
        gold = data.gold;
    }
    public Account(string _name)
    {
        AccountName = _name;
        levelsUnlocked = 1;
    }
}
