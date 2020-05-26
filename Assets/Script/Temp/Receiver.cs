using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receiver 
{
    public int Life { get; set; } = 100;

    // Update is called once per frame
    public void Update()
    {
        Debug.Log(Life);
        if (Life < 0) Debug.Log("Died");

    }
}
