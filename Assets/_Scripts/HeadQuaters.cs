using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadQuaters : MonoBehaviour
{
    [SerializeField] GameObject health_display;
    [SerializeField] GameObject money_display;
    [SerializeField] GameObject enemy_display;
    [SerializeField] GameObject wave_display;
    [SerializeField] float health =100;
    [SerializeField] public GameObject centerOfBase = null;
    [SerializeField] float checkIfEnemyRadius=1;
    [SerializeField] public AudioClip StageMusic;
    public EnemySpawner spawner;

    private int waveNumber = 0;
    Vector3 target;
    public float currentHealth;
    public float currentMoney;

    // Start is called before the first frame update
    void Start()
    {
        spawner = gameObject.GetComponent<EnemySpawner>();
        currentHealth = health;
        target = centerOfBase.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        

        checkIfEnemiesAreInBase(checkIfEnemyRadius);
        health_display.GetComponent<Text>().text = currentHealth.ToString();
        money_display.GetComponent<Text>().text = currentMoney.ToString();

        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        
        if (enemyCount == 0)
        {
            if (waveNumber >= spawner.waves.Capacity)
            {
                ShowVictoryScreen();
                return;
            }
            spawner.SpawnWave(waveNumber);
            waveNumber +=1;
        }

        enemy_display.GetComponent<Text>().text = enemyCount.ToString();
        wave_display.GetComponent<Text>().text = waveNumber.ToString() + " / " + spawner.waves.Capacity.ToString();


    }

    private void ShowVictoryScreen()
    {
        Debug.Log("Victorey");
    }

    private void checkIfEnemiesAreInBase(float radius)
    {
        var raycastRadius = radius;
        var checkRadius = radius;

        RaycastHit hit;

        if (Physics.SphereCast(target, checkRadius, centerOfBase.transform.forward, out hit, raycastRadius))
        {
            Destroy(hit.collider.gameObject);
            currentHealth -= 10;
            //money -= 10;
        }
    }

}
