using UnityEngine;
[System.Serializable]
public class CUS_Wave_Location_Data
{
    public CUS_Wave_Data[] waveData;
}

[System.Serializable]
public class CUS_Wave_Data
{
    public GameObject[] enemies;
    public CUS_Wave_Location location;
}