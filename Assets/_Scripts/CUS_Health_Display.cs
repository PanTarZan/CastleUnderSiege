using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CUS_Health_Display : MonoBehaviour
{
    [SerializeField] Image currentHealthImage = null;
    float currentScale = 1;

    void Update()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);
    }

    public void DisplayHealth(float percentage)
    {
        currentHealthImage.fillAmount = percentage;
    }
}
