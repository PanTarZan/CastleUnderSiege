using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float health;
    public float current_health;
    // Start is called before the first frame update
    void Start()
    {
        current_health = health;   
    }

    // Update is called once per frame
    void Update()
    {
        if (current_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        current_health -= damage;
    }
}
