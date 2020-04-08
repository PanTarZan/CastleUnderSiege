using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class CUS_Save_system
{

    public static void SaveAccountData(Account account)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data1.kappa";
        Debug.Log(path);
        FileStream stream = new FileStream(path, FileMode.Create);

        AccountData data = new AccountData(account);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static AccountData LoadAccountData()
    {
        string path = Application.persistentDataPath + "/data1.kappa";
        if (File.Exists(path))
        {
            Debug.Log(path);
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            AccountData data = formatter.Deserialize(stream) as AccountData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("no file present in  path: "+path);
            return null;
        }
    }
}
