using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public GameObject projectilePrefab;
    public GameObject cannonStartPoint;
    public float shootCooldown;
    private float nextFire = 0.1f;
    public GameObject partToRotate;

    // Start is called before the first frame update
    void Start()
    {
}

    // Update is called once per frame
    void Update()
    {
        LookAtPointer();
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFire)
        {
            Shoot();
            nextFire = Time.time + shootCooldown;
        }
    }

    private void LookAtPointer()
    {
        GameObject pointer = FindObjectOfType<Pointer>().gameObject;
        partToRotate.transform.LookAt(pointer.transform);
        cannonStartPoint.transform.LookAt(pointer.transform);
        Debug.DrawLine(transform.position, pointer.transform.position);
    }

    private void Shoot()
    {
        Instantiate(projectilePrefab, cannonStartPoint.transform.position, cannonStartPoint.transform.rotation);
    }
}
