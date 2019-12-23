using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float thrust;
    public Rigidbody rb;
    public float damage;
    public float explosionRadius;
    public GameObject explosionEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * thrust);
    }

    void FixedUpdate()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        Explode();
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        RaycastHit[] hit = Physics.SphereCastAll(transform.position, explosionRadius, new Vector3(0,0.1f,0));
        foreach (var h in hit)
        {
            if (h.transform.GetComponent<Health>())
            {
                Debug.Log(h.transform.gameObject.name);
                h.transform.GetComponent<Health>().TakeDamage(damage);
            }
        }
        Destroy(gameObject);

    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
