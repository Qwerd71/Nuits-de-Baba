using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{
    // Start is called before the first frame update
    public bool End;
    public void Start_Filling()
    {
        Debug.Log("Bouh");
        End = false;
    }
    public void End_Filling()
    {
        Debug.Log("Cest bon");
        End = true;
    }
}
