[System.Serializable]
public class AccountData
{
    public string name;
    public int timeSpent;
    public int levelsUnlocked;
    public int enemiesKilled;
    public int gold;

    public AccountData(Account account)
    {
            name = account.AccountName;
            timeSpent = account.timeSpent;
            levelsUnlocked = account.levelsUnlocked;
            enemiesKilled = account.enemiesKilled;
            gold = account.gold;
        
    }
}
