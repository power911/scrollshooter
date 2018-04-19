using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Technical : MonoBehaviour {
    public static Technical Instance { get; private set; }

    public delegate void EventDel();
    
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(this.gameObject);
        }
    }

    public static void TimerEvent(float time, EventDel eventDel, bool isInfinity)
    {

    }
    
    public IEnumerator TimerCor(float time, EventDel eventDel, bool isInfinity)
    {
        float timeCurrent;
        do
        {
            timeCurrent = time;
            while ((timeCurrent -= Time.deltaTime) >= 0)
            {
               yield return null;
            }
            eventDel();
            yield return null;
        } while (isInfinity);
    }
}
