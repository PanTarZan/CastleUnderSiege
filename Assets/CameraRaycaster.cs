using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CameraRaycaster : MonoBehaviour
{

    Camera cam;
    //[SerializeField] GameObject miniSphere = null;

    private GameObject[] cannons = null;
    public MouseLook mouseLook = new MouseLook();
    //public GameObject characterObject;

    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        cannons = GameObject.FindGameObjectsWithTag("Turret");
        mouseLook.Init(transform, cam.transform);
    }

    void Update()
    {
        RotateView();
        //RaycastHit hit;
        // Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        // if (Physics.Raycast(ray, out hit))
        // {
        //miniSphere.transform.position = hit.point;

        //  foreach (var cannon in cannons)
        //   {
        //      cannon.transform.LookAt(hit.point, Vector3.up);
        // }

        /// }
    }

    private void RotateView()
    {
        //avoids the mouse looking if the game is effectively paused
        if (Mathf.Abs(Time.timeScale) < float.Epsilon) return;


        mouseLook.LookRotation(transform, cam.transform);
    }
}

