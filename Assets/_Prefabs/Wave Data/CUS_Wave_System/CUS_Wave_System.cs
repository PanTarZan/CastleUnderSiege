using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CUS_Wave_System : MonoBehaviour
{
    #region variables


    [Header("System Events")]
    public UnityEvent onWaveSpawned = new UnityEvent();
    public List<CUS_Wave_Location_Data> waves = new List<CUS_Wave_Location_Data>();

    [Header("UI")]
    [SerializeField] GameObject enemy_display = null;
    [SerializeField] GameObject wave_display = null;

    public int waveIndex = 0;
    public bool startedSpawningWaves = false;
    #endregion

    #region MainMethods
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        ManageWaveSpawning();
    }
    #endregion

    #region HelperMethods
    public void SpawnWave(int waveIndex)
    {
        onWaveSpawned.Invoke();
        foreach (var waveData in waves[waveIndex].waveData)
        {
            foreach (var enemy in waveData.enemies)
            {
                waveData.location.SpawnMonster(enemy);
            }
        }
    }

    public void SetSpawningWaves(bool value)
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
                if (waveIndex >= waves.Capacity)
                {
                    FindObjectOfType<LevelManagement>().ShowVictoryScreen();
                    return;
                }
                SpawnWave(waveIndex);
                waveIndex += 1;
            }

            enemy_display.GetComponent<Text>().text = enemyCount.ToString();
            wave_display.GetComponent<Text>().text = waveIndex.ToString() + " / " + waves.Capacity.ToString();
        }
        else
        {
            enemy_display.GetComponent<Text>().text = "N/A";
        }

    }

    #endregion
}
