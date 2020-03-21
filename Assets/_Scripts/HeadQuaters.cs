using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadQuaters : MonoBehaviour
{
    [SerializeField] float health =100;
    [SerializeField] float money = 0;
    [SerializeField] public GameObject centerOfBase = null;
    [SerializeField] float checkIfEnemyRadius=1;
    public float currentHealth;
    public float currentMoney;


    [SerializeField] GameObject health_display = null;
    [SerializeField] GameObject money_display = null;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {


        checkIfEnemiesAreInBase(checkIfEnemyRadius);
        health_display.GetComponent<Text>().text = currentHealth.ToString();
        money_display.GetComponent<Text>().text = currentMoney.ToString();
        if (currentHealth <= 0)
        {
            FindObjectOfType<LevelManagement>().TurnOnGameOver();
        }
        
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
