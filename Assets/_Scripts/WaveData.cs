using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/WaveData", order = 1)]
public class WaveData : ScriptableObject
{
    public int enemyAmount;
    public GameObject enemyPrefab;

}
