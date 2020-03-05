using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float thrust;
    [SerializeField] float damageMIN;
    [SerializeField] float damageMAX;
    [SerializeField] float explosionRadius;
    public GameObject explosionEffect;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * thrust);
    }

    void OnCollisionEnter(Collision col)
    {
        Explode();
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        RaycastHit[] hit = Physics.SphereCastAll(transform.position, explosionRadius, Vector3.up);
        DamageAllObjects(hit);
        Destroy(gameObject);
    }

    private void DamageAllObjects(RaycastHit[] hit)
    {
        foreach (var h in hit)
        {
            if (h.transform.GetComponent<CUS_Enemy_AI>())
            {
                float damage = Random.Range(damageMIN, damageMAX);
                h.transform.GetComponent<CUS_Enemy_AI>().TakeDamage(damage);
            }
        }
    }
}
