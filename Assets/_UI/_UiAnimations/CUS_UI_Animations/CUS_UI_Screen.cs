using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CanvasGroup))]
public class CUS_UI_Screen : MonoBehaviour
{
    #region variables
    [Header("Main Properties")]

    public Selectable m_StartSelectable;

    [Header("Screen Events")]
    public UnityEvent onScreenStart = new UnityEvent();
    public UnityEvent onScreenClose = new UnityEvent();

    private Animator animator;
    #endregion

    #region MainMethods

    void Awake()
    {
        animator = GetComponent<Animator>();

    }
    // Start is called before the first frame update
    void Start()
    {

        if (m_StartSelectable)
        {
            EventSystem.current.SetSelectedGameObject(m_StartSelectable.gameObject);
        }
    }
    #endregion

    #region HelperMethods

    public virtual void StartScreen()
    {
        if (onScreenStart != null)
        {
            onScreenStart.Invoke();
        }
        HandleAnimator("show");
    }
    public virtual void CloseScreen()
    {
        if (onScreenClose != null)
        {
            onScreenClose.Invoke();
        }
        HandleAnimator("hide");
    }

    void HandleAnimator(string aTrigger)
    {
        if (animator)
        {
            animator.SetTrigger(aTrigger);
        }
    }
    #endregion
}
