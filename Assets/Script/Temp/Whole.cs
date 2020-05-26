using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whole : MonoBehaviour
{
    GameObject o1;
    GameObject o2;

    Receiver receiver;
    ShootTemp shootTemp;

    void Start()
    {
        o1 = GameObject.Find("1").gameObject;
        o2 = GameObject.Find("2").gameObject;

        receiver = new Receiver();
        shootTemp = new ShootTemp();
    }

    void Update()
    {

        for (int i = 0; i < 10; i++) {

            receiver.Update();
            shootTemp.Update(o1, receiver);
        }
    }
}
