using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        print("im here22!");
        if (collision.gameObject.tag == "Hiter")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }

}
