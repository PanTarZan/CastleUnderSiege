using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadQuaters : MonoBehaviour
{
    [SerializeField] float max_health =100;
    [SerializeField] int money;
    [SerializeField] public GameObject centerOfBase = null;
    [SerializeField] float checkIfEnemyRadius=1;
    public float currentHealth;
    public int currentMoney;


    [SerializeField] GameObject health_display_bar = null;
    [SerializeField] GameObject health_display_value = null;
    [SerializeField] GameObject money_display = null;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = max_health;
        LoadSomeStatsFromAccountData();
        currentMoney = money;
    }

    public void LoadSomeStatsFromAccountData()
    {
        if (FindObjectOfType<CurrentPlayerAccount>())
        {
            var _acc = FindObjectOfType<CurrentPlayerAccount>();
            money = _acc.gold;
            Debug.Log("Getting Muni" + _acc.AccountName);
            Debug.Log("Getting Muni" + _acc.gold);
        }
    }

    // Update is called once per frame
    void Update()
    {


        checkIfEnemiesAreInBase(checkIfEnemyRadius);
        DisplayHealth();
        if (money_display)
            money_display.GetComponent<Text>().text = currentMoney.ToString();
        if (currentHealth <= 0)
        {
            FindObjectOfType<LevelManagement>().TurnOnGameOver();
        }

    }

    private void DisplayHealth()
    {
        health_display_value.GetComponent<Text>().text = currentHealth.ToString();
        health_display_bar.GetComponent<Image>().fillAmount = currentHealth / max_health;
    }

    private void checkIfEnemiesAreInBase(float radius)
    {
        var raycastRadius = radius;
        var checkRadius = radius;

        RaycastHit hit;

        if (Physics.SphereCast(centerOfBase.transform.position, checkRadius, centerOfBase.transform.forward, out hit, raycastRadius))
        {
            if (hit.collider.gameObject.tag == "Enemy")
            {
            Destroy(hit.collider.gameObject);
            currentHealth -= 10;
            }
        }
    }

    

}
