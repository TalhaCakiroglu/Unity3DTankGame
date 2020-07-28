using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float can = 100;
   
    private void Start()
    {
        Destroy(gameObject, 5);
    }
    private void OnCollisionEnter(Collision collision)
    {
        HealtBehaviour hBehaviour = collision.gameObject.GetComponent<HealtBehaviour>();

        if (hBehaviour!= null)
        {
            hBehaviour.HasarYap(20);
        }
        Destroy(gameObject, 1);
    }
}
