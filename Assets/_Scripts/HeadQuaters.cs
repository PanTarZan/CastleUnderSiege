using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadQuaters : MonoBehaviour
{
    [SerializeField] GameObject health_display;
    [SerializeField] float health =1;
    [SerializeField] float money = 1;
    [SerializeField] public GameObject centerOfBase = null;
    [SerializeField] float checkIfEnemyRadius=1;




    Vector3 target;
    public float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        target = centerOfBase.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        checkIfEnemiesAreInBase(checkIfEnemyRadius);
        health_display.GetComponent<Text>().text = currentHealth.ToString();
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
            money -= 10;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(target, checkIfEnemyRadius);
    }
}
