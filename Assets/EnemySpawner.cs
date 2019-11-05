using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> Routes = null;
    public GameObject EnemyPrefab = null;
    public List<Transform> Waypoints;
    public HeadQuaters HQ;

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
    }
}
