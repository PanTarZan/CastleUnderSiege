using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> Routes = null;
    public GameObject EnemyPrefab = null;
    public List<Transform> Waypoints;
    public HeadQuaters HQ;
    public Transform enemyTarget;

    // Start is called before the first frame update
    void Start()
    {
        HQ = GetComponent<HeadQuaters>();
        foreach (Transform w in Routes[0].transform.GetComponentInChildren<Transform>())
        {
            Waypoints.Add(w);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            var enemy = Instantiate(EnemyPrefab, Waypoints[0].position, Waypoints[0].rotation);
            var enemyAI = enemy.GetComponent<AICharacterControl>();
            enemyAI.SetTarget(enemyTarget);

        }
    }
}
