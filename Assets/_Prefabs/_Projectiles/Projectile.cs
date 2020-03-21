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
        AudioManager _am = AudioManager.instance;
        _am.PlaySound(explosionSound);
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
