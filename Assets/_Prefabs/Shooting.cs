﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
public class Shooting : MonoBehaviour
{
    [Header("Others")]
    [SerializeField] Image shootColdown = null;
    public string shootSound;
    public UnityEvent OnCannonShoot;
    public LayerMask layer;
    public LineRenderer lineVisual;
    public int lineSegment = 10;
    public GameObject projectilePrefab;
    public GameObject cannonStartPoint;
    public float nextFire = 0.1f;
    public GameObject CannonHead;
    public GameObject CannonBase;
    GameObject pointer;
    private Quaternion m_CharacterTargetRot;
    private Quaternion m_CameraTargetRot;
    public UpgradeSystem uSystem;

    [Header("Attributes")]
    public float shootCooldown;
    

    // Start is called before the first frame update
    void Start()
    {
        lineVisual = GetComponent<LineRenderer>();
        lineVisual.positionCount = lineSegment;
        pointer = FindObjectOfType<Pointer>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vo = CalculateVelocty(pointer.transform.position, cannonStartPoint.transform.position, 1f);

        LookRotation(pointer,vo);
        Visualize(vo);

        if (shootColdown)
            shootColdown.fillAmount = 1 - ((nextFire - Time.time) / (shootCooldown * uSystem.shootingCooldownMultiplier));

        if (Input.GetKey(KeyCode.Mouse0) && (nextFire <= Time.time))
        {
            nextFire = Time.time + (shootCooldown * uSystem.shootingCooldownMultiplier);
            Shoot(vo);
        }
    }

    private void Shoot(Vector3 vo)
    {
        var ball = Instantiate(projectilePrefab, cannonStartPoint.transform.position, cannonStartPoint.transform.rotation);
        ball.GetComponent<Rigidbody>().velocity = vo;
        ApplyUpgrades(ball);
        OnCannonShoot.Invoke();
    }

    private void ApplyUpgrades(GameObject ball)
    {
        var b_projectile = ball.GetComponent<Projectile>();
        b_projectile.uSystem = uSystem;
        b_projectile.damageMIN = b_projectile.damageMIN * uSystem.damageMultiplier;
        b_projectile.damageMAX = b_projectile.damageMAX * uSystem.damageMultiplier;
        b_projectile.explosionRadius = b_projectile.explosionRadius * uSystem.radiusMultiplier;
    }

    public void LookRotation(GameObject pointer, Vector3 vo)
    {
        var lookDir = pointer.transform.position - transform.position;
        lookDir.y = 0; // keep only the horizontal direction
        CannonBase.transform.rotation = Quaternion.LookRotation(lookDir);
        CannonHead.transform.rotation = Quaternion.LookRotation(vo);


    }

    void Visualize(Vector3 vo)
    {
        for (int i = 0; i < lineSegment; i++)
        {
            Vector3 pos = CalculatePosInTime(vo, i / (float)lineSegment);
            lineVisual.SetPosition(i, pos);
        }
    }

    Vector3 CalculateVelocty(Vector3 target, Vector3 origin, float time)
    {
        Vector3 distance = target - origin;
        Vector3 distanceXz = distance;
        distanceXz.y = 0f;

        float sY = distance.y;
        float sXz = distanceXz.magnitude;

        float Vxz = sXz * time;
        float Vy = (sY / time) + (0.5f * Mathf.Abs(Physics.gravity.y) * time);

        Vector3 result = distanceXz.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }

    Vector3 CalculatePosInTime(Vector3 vo, float time)
    {
        Vector3 Vxz = vo;
        Vxz.y = 0f;

        Vector3 result = cannonStartPoint.transform.position + vo * time;
        float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (vo.y * time) + cannonStartPoint.transform.position.y;

        result.y = sY;

        return result;
    }
}