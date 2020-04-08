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
        currentScale = percentage;
        currentHealthImage.rectTransform.localScale = new Vector3(currentScale, 1, 1);
    }
}
