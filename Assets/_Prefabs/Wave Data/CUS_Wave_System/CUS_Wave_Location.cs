
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CUS_Wave_Location : MonoBehaviour
{
    #region variables
    

    [Header("System Events")]
    public UnityEvent onEnemySpawn = new UnityEvent();
    public List<GameObject> enemyPath = new List<GameObject>();

    
    #endregion

    #region MainMethods
    // Start is called before the first frame update
    void Start()
    {
        
    }
    #endregion

    #region HelperMethods

    public void SpawnMonster(GameObject prefab)
    {
        onEnemySpawn.Invoke();

        var monster = Instantiate(prefab, transform.position, Quaternion.identity);
        SetPathForEnemy(monster);
    }

    private void SetPathForEnemy(GameObject monster)
    {
        foreach (var point in enemyPath)
        {
            monster.GetComponent<CUS_Enemy_AI>().routeElements.Enqueue(point);
        }
    }
    #endregion
}
