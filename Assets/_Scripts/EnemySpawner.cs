using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> locations = new List<GameObject>();
    public List<WaveData> waves = new List<WaveData>();
    public Transform baseToAttackObject;

    private int waveNumber = 0;
    GameObject target;
    public bool startedSpawningWaves = false;


    [SerializeField] GameObject enemy_display = null;
    [SerializeField] GameObject wave_display = null;

    void Update()
    {
        ManageWaveSpawning();
    }

    public void SpawnWave(int waveNumber)
    {
        foreach (var loc in locations)
        {
            if (loc == null)
            {
                continue;
            }

            for (int i=0; i<= waves[waveNumber].enemyAmount-1; i++)
            {
                var enemy = Instantiate(waves[waveNumber].enemyPrefab, loc.transform.position, Quaternion.identity);
                var enemyAI = enemy.GetComponent<CUS_Enemy_AI>();
                enemyAI.SetTarget(baseToAttackObject);
            }
        }
    }

    public void SetSpawningWavges(bool value)
    {
        startedSpawningWaves = value;
    }

    private void ManageWaveSpawning()
    {
        if (startedSpawningWaves)
        {
            int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (enemyCount == 0)
            {
                if (waveNumber >= waves.Capacity)
                {
                    FindObjectOfType<LevelManagement>().ShowVictoryScreen();
                    return;
                }
                SpawnWave(waveNumber);
                waveNumber += 1;
            }

            enemy_display.GetComponent<Text>().text = enemyCount.ToString();
            wave_display.GetComponent<Text>().text = waveNumber.ToString() + " / " + waves.Capacity.ToString();
        }
        else
        {
            enemy_display.GetComponent<Text>().text = "N/A";
        }

    }
}
