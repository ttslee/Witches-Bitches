using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Timer Info
    private string tName;
    private float t;
    private bool done;

    public string TName
    {
        get
        {
            return tName;
        }

        set
        {
            tName = value;
        }
    }

    public float T
    {
        get
        {
            return t;
        }

        set
        {
            t = value;
        }
    }

    public bool Done
    {
        get
        {
            return done;
        }

        set
        {
            done = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!Done)
        {
            T -= Time.deltaTime;
            if (T <= 0)
            {
                Done = true;
            }
        }         
    }

    public void SetTime(float time, string nm)
    {
        tName = nm;
        T = time;
        Done = false;
    }
    
}
