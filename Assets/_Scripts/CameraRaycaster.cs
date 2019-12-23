using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CameraRaycaster : MonoBehaviour
{

    [SerializeField] GameObject PointerPrefab;
    GameObject Pointer = null;
    Camera cam;
    public Vector3 pointerLocation;

    private GameObject[] cannons = null;
    public MouseLook mouseLook = new MouseLook();

    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        cannons = GameObject.FindGameObjectsWithTag("Turret");
        mouseLook.Init(transform, cam.transform);
        Pointer = Instantiate(PointerPrefab);
    }

    void Update()
    {
        
        RotateView();
        RaycastHit hit;
        if (Physics.Raycast(transform.position, cam.transform.forward, out hit, Mathf.Infinity))
        {
            Pointer.transform.position = hit.point;
        }
        else
        {
        }
    }

    private void RotateView()
    {
        //avoids the mouse looking if the game is effectively paused
        if (Mathf.Abs(Time.timeScale) < float.Epsilon) return;


        mouseLook.LookRotation(transform, cam.transform);
    }
}

