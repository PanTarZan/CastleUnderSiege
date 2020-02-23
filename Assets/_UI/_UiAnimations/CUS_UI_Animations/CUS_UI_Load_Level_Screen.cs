using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CanvasGroup))]
public class CUS_UI_Load_Level_Screen : CUS_UI_Screen
{
    #region variables
    [Header("Timed Screen Properties")]

    public float m_ScreenTime = 0.1f;
    public UnityEvent onTimeCompleted = new UnityEvent();

    private float startTime;
    #endregion

    #region MainMethods
    #endregion

    #region HelperMethods

    public override void StartScreen()
    {
        base.StartScreen();

        startTime = Time.time;
        StartCoroutine(WaitForTime());
    }

    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(m_ScreenTime);


        if (onTimeCompleted != null)
        {
            onTimeCompleted.Invoke();
        }
        
    }
    #endregion
}
