

/*public class ExplosionHandler : MonoBehaviour
{
    ParticleSystem ps;
    float dissappearTimer;
    void Awake()
    {
        ps = gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ps.isPlaying)
        {
            Destroy(gameObject);
        }
    }

    public void Create(Vector3 poisiton, GameObject explosion_prefab)
    {
        Instantiate(explosion_prefab, poisiton, Quaternion.identity);
        GetComponent<AudioSource>().Play();
    }
} */ 
