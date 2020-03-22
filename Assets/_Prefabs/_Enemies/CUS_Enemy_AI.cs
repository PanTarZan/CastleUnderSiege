using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class CUS_Enemy_AI : MonoBehaviour
{
    [Header("Health Part")]
    [SerializeField] CUS_Health_Display healthBarPrefab = null;
    [SerializeField] GameObject damagePopupPrefab = null;
    public float starting_health;
    public float dieTime;
    float current_health;
    bool is_dead = false;

    [Header("AI Part")]
    public Transform target;
    public UnityEngine.AI.NavMeshAgent agent { get; private set; }
    public Queue<GameObject> routeElements = new Queue<GameObject>();

    [Header("TPC Part")]
    [SerializeField] float m_MovingTurnSpeed = 360;
    [SerializeField] float m_StationaryTurnSpeed = 180;
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    float m_TurnAmount;
    float m_ForwardAmount;
    Vector3 m_GroundNormal = Vector3.zero;

    [Header("Audio Part")]
    public string stepSound = "EnemySteps";
    

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        current_health = starting_health;
        

        //AI
        
        agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updatePosition = true;

        //TPC
        
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
        UpdateAIAgent();

    }

    private void UpdateAIAgent()
    {
        
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
        else
        { SetTarget(routeElements.Dequeue().transform);
        }

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            Move(agent.desiredVelocity);
        }
        else
        {
            Move(Vector3.zero);
        }
        if (Mathf.Abs(Vector3.Distance(target.position, transform.position)) <= agent.stoppingDistance)
            SetTarget(routeElements.Dequeue().transform);

    }

    private void UpdateHealth()
    {
        healthBarPrefab.DisplayHealth(Mathf.Clamp01(current_health / starting_health));
        if (current_health <= 0)
        {
            current_health = 0;
            if (!is_dead)
                StartCoroutine("Die");
            is_dead = true;
        }
    }

    private IEnumerator Die()
    {
        agent.speed = 0;
        m_Animator.SetTrigger("Die");
        yield return new WaitForSeconds(dieTime);
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        current_health -= damage;
        m_Animator.SetTrigger("GetHit");
        damagePopupPrefab.GetComponent<DamagePopup>().Create(transform.position, damage, damagePopupPrefab);
    }

    public void Move(Vector3 move)
    {
        if (move.magnitude > 1f) move.Normalize();
        move = transform.InverseTransformDirection(move);
        move = Vector3.ProjectOnPlane(move, m_GroundNormal);
        m_TurnAmount = Mathf.Atan2(move.x, move.z);
        m_ForwardAmount = move.z;

        ApplyExtraTurnRotation();
    }

    void ApplyExtraTurnRotation()
    {
        float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
        transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
    }

    public void OnAnimatorMove()
    {
        if (m_Animator)
        {
            if (Time.deltaTime != 0)
            {
                Vector3 v = (m_Animator.deltaPosition) / Time.deltaTime;
                v.y = m_Rigidbody.velocity.y;
                m_Rigidbody.velocity = v;
            }
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void Step()
    {
        AudioManager _am = AudioManager.instance;
        _am.PlaySound(stepSound);
    }
}
