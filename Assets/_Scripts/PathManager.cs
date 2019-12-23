using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class PathManager : MonoBehaviour
{
    AICharacterControl AiController = null;
    public List<GameObject> Waypoints = null;
    public int WaypointIndex = 0;
    public float Distance;
    public int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        AiController = GetComponent<AICharacterControl>();
        AiController.SetTarget(Waypoints[i].transform);
        i++;
     }

    // Update is called once per frame
    void Update()
    {
        Distance = Vector3.Distance(transform.position, AiController.target.position);
        if (Distance <= 0.5)
        {
            SetNextTarget(i);
            i++;
        }
    }

    private void SetNextTarget(int x)
    {
        WaypointIndex = x % Waypoints.Capacity;
        AiController.SetTarget(Waypoints[WaypointIndex].transform);
    }
}
