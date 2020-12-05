using UnityEngine;

public class Projectile : MonoBehaviour
{
    public string explosionSound;
    [SerializeField] public float damageMIN=1;
    [SerializeField] public float damageMAX = 2 ;
    [SerializeField] public float explosionRadius =1;
    public GameObject explosionEffect;
    public bool destroyOnImpact = true;
    public UpgradeSystem uSystem;

    void Start()
    {
        
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
        //uSystem = FindObjectOfType<UpgradeSystem>();
        foreach (var h in hit)
        {
            if (h.transform.GetComponent<CUS_Enemy_AI>())
            {
                Debug.Log(uSystem.damageMultiplier);
                float damage = Random.Range(damageMIN, damageMAX);
                Debug.Log(uSystem.damageMultiplier);
                h.transform.GetComponent<CUS_Enemy_AI>().TakeDamage(damage*uSystem.damageMultiplier);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
