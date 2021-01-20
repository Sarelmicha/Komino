//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Nammie : MonoBehaviour
//{
//    private Rigidbody2D rigid;

//    private void Awake()
//    {
//        rigid = GetComponent<Rigidbody2D>();
//    }

//    void OnCollisionEnter2D(Collision2D collision)
//    {
        
//        if (collision.gameObject.tag == "Ball")
//        {
//            JoystickBall ball = collision.gameObject.GetComponentInParent<JoystickBall>();
//            rigid.AddForce( new Vector2(Random.Range(-60,60),Random.Range(5,15)) * ball.GetDamage().GetDamage());
//        }
//    }
//}
