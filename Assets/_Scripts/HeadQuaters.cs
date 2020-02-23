using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadQuaters : MonoBehaviour
{
    [SerializeField] GameObject health_display = null;
    [SerializeField] GameObject money_display = null;
    [SerializeField] GameObject enemy_display = null;
    [SerializeField] GameObject wave_display = null;
    [SerializeField] float health =100;
    [SerializeField] public GameObject centerOfBase = null;
    [SerializeField] float checkIfEnemyRadius=1;
    [SerializeField] public AudioClip StageMusic;
    [SerializeField] LevelManagement lm;
    public EnemySpawner spawner;

    private int waveNumber = 0;
    Vector3 target;
    public float currentHealth;
    public float currentMoney;

    // Start is called before the first frame update
    void Start()
    {
        spawner = gameObject.GetComponent<EnemySpawner>();
        lm = gameObject.GetComponent<LevelManagement>();
        currentHealth = health;
        target = centerOfBase.transform.position;
    }

    // Update is called once per frame
    void Update()
    {


        checkIfEnemiesAreInBase(checkIfEnemyRadius);
        ManageWaveSpawning();

    }

    public void SwitchSpawningWavges()
    {
        lm.startedSpawningWaves = !lm.startedSpawningWaves;
    }

    private void ManageWaveSpawning()
    {
        health_display.GetComponent<Text>().text = currentHealth.ToString();
        money_display.GetComponent<Text>().text = currentMoney.ToString();


        if (lm.startedSpawningWaves)
        {
            int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (enemyCount == 0)
            {
                if (waveNumber >= spawner.waves.Capacity)
                {
                    lm.ShowVictoryScreen();
                    return;
                }
                spawner.SpawnWave(waveNumber);
                waveNumber += 1;
            }

            enemy_display.GetComponent<Text>().text = enemyCount.ToString();
            wave_display.GetComponent<Text>().text = waveNumber.ToString() + " / " + spawner.waves.Capacity.ToString();
        }
        
    }

    private void checkIfEnemiesAreInBase(float radius)
    {
        var raycastRadius = radius;
        var checkRadius = radius;

        RaycastHit hit;

        if (Physics.SphereCast(target, checkRadius, centerOfBase.transform.forward, out hit, raycastRadius))
        {
            if (hit.collider.gameObject.tag == "Enemy")
            {
            Destroy(hit.collider.gameObject);
            currentHealth -= 10;
            if (currentHealth <= 0)
                {
                    TurnOnGameOver();
                }

            }
        }
    }

    private void TurnOnGameOver()
    {
        lm.ShowGameOverScreen();
        FindObjectOfType<Shooting>().enabled = false;
    }
}
