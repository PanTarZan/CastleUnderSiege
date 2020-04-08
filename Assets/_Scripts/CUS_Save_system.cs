using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class CUS_Save_system
{

    public static void SaveAccountData(Account account, string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        Debug.Log(path);
        FileStream stream = new FileStream(path, FileMode.Create);

        AccountData data = new AccountData(account);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static AccountData LoadAccountData(string path)
    {
        if (File.Exists(path))
        {
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
