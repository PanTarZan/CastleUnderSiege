using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> locations = new List<GameObject>();
    public List<WaveData> waves = new List<WaveData>();
    public Transform baseToAttackObject;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnWave(int waveNumber)
    {
        foreach (var loc in locations)
        {
            for (int i=0; i<= waves[waveNumber].enemyAmount; i++)
            {
                var enemy = Instantiate(waves[waveNumber].enemyPrefab, loc.transform.position, Quaternion.identity);
                var enemyAI = enemy.GetComponent<AICharacterControl>();
                enemyAI.SetTarget(baseToAttackObject);
            }
        }
    }
}
