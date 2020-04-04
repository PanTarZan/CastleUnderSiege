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
        if (account == null)
        {
            name = "";
            timeSpent = 0;
            levelsUnlocked = 1;
            enemiesKilled = 0;
            gold = 0;
        }
        else
        {
            name = account.AccountName;
            timeSpent = account.timeSpent;
            levelsUnlocked = account.levelsUnlocked;
            enemiesKilled = account.enemiesKilled;
            gold = account.gold;
        }
    }
}
