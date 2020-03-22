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
    public float timeBetweenWaves = 30f;

    private float nextWaveTime = 0f;
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
                StartCoroutine(waveData.location.SpawnOverTime(enemy));
            }
        }
    }

    public void SetSpawningWaves(bool value)
    {
        startedSpawningWaves = value;
        FindObjectOfType<LevelManagement>().isGamePaused = false;
    }

    private void ManageWaveSpawning()
    {
        if (startedSpawningWaves)
        {
            var currentEnemies = FindObjectsOfType<CUS_Enemy_AI>();
            if (nextWaveTime < Time.time)
            {
                if (waveIndex >= waves.Capacity)
                {
                    if (currentEnemies.Length <= 0)
                    {
                        FindObjectOfType<LevelManagement>().ShowVictoryScreen();
                        return;
                    }
                    return;
                }
                SpawnWave(waveIndex);
                nextWaveTime = Time.time + timeBetweenWaves;
                waveIndex += 1;
            }

            enemy_display.GetComponent<Text>().text = currentEnemies.Length.ToString();
            wave_display.GetComponent<Text>().text = waveIndex.ToString() + " / " + waves.Capacity.ToString();
        }
        else
        {
            enemy_display.GetComponent<Text>().text = "N/A";
        }

    }

    #endregion
}
