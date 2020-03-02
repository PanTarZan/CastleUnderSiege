using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(Animator))]
public class Health : MonoBehaviour
{
    [SerializeField] GameObject damagePopupPrefab = null;
    public float starting_health;
    public float dieTime;
    float current_health;
    bool is_dead = false;

    Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        current_health = starting_health;   
    }

    // Update is called once per frame
    void Update()
    {
        if (current_health <= 0)
        {

            if (!is_dead)
            {
                StartCoroutine("Die");
            }
            
            is_dead = true;
        }
    }

    private IEnumerator Die()
    {
        GetComponent<AICharacterControl>().SetTarget(transform);
        _animator.SetTrigger("Die");
        yield return new WaitForSeconds(dieTime);
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        current_health -= damage;
        _animator.SetTrigger("GetHit");
        damagePopupPrefab.GetComponent<DamagePopup>().Create(transform.position, damage, damagePopupPrefab);
    }
}
