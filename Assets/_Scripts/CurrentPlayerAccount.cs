using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentPlayerAccount : Account
{
    public CurrentPlayerAccount(string _name) : base(_name)
    {
    }

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("SaveSystem");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
