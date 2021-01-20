using Komino.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiter : MonoBehaviour
{
    [SerializeField] Transform hitPoint = null;
    Vector3 startPos;
    //Storages
    [SerializeField] private ParticlesStorage particlesStorage = null;
    


    public float speed = 0.4f;
    bool isGoingUp = true;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        Move();

    }

     void Move()
    {

        
        if (transform.position.y == hitPoint.position.y)
        {
            Vector2 newPos = transform.position;
            newPos.y -= 5;
            GameObject bombParticle = Instantiate(particlesStorage.GetPrizeParticle(),newPos,Quaternion.identity);
            isGoingUp = false;
        }

        if (transform.position.y == startPos.y)
        {
            isGoingUp = true;
        }

        if (!isGoingUp)
        {
            MoveBack();
        }

        else
        {
            MoveToHitPoint();

        }
    }

    private void MoveToHitPoint()
    {
         transform.position =  Vector2.MoveTowards(transform.position, hitPoint.position, speed);

       
    }

    private void MoveBack()
    {
        transform.position = Vector2.MoveTowards(transform.position, startPos, speed);
    }
}
