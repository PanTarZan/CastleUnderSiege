using UnityEngine;

public class Projectile : MonoBehaviour
{
    public string explosionSound;
    [SerializeField] float thrust=1;
    [SerializeField] float damageMIN=1;
    [SerializeField] float damageMAX = 2 ;
    [SerializeField] float explosionRadius =1;
    public GameObject explosionEffect;
    Rigidbody rb;
    public bool destroyOnImpact = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb)
            rb.AddForce(transform.forward * thrust);
    }

    void OnCollisionEnter(Collision col)
    {
        Explode();
    }

    public void Explode()
    {
        if (explosionEffect)
            Instantiate(explosionEffect, transform.position, transform.rotation);
        AudioManager _am = AudioManager.instance;
        if (explosionSound != string.Empty)
        {
            _am.PlaySound(explosionSound);
        }
        Collider[] hit = Physics.OverlapSphere(transform.position, explosionRadius);
        DamageAllObjects(hit);
        if (destroyOnImpact)
            Destroy(gameObject);
    }

    private void DamageAllObjects(Collider[] hit)
    {
        foreach (var h in hit)
        {
            Debug.Log(h);
            if (h.transform.GetComponent<CUS_Enemy_AI>())
            {
                float damage = Random.Range(damageMIN, damageMAX);
                h.transform.GetComponent<CUS_Enemy_AI>().TakeDamage(damage);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
