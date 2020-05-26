using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTemp 
{

    public void Update(GameObject gameObject, Receiver receiver)
    {
        CheckCollision(gameObject, receiver);
        Debug.Log("runs");
    }

    private void CheckCollision(GameObject gameObject, Receiver receiver) {
        RaycastHit hit;

        float length = 30f;

        if (Physics.Raycast(
                gameObject.transform.position, 
                gameObject.transform.TransformDirection(Vector3.forward),
                out hit,    
                length
            )
        ) {
            Debug.Log("hit");
            receiver.Life -= 25;
        }
    }

}
