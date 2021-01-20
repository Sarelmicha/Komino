//using Komino.CampaignBattle.Cards.BattleCards;
//using Komino.Core;
//using Komino.GameEvents.Events;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class JoystickBall : MonoBehaviour
//{
//    [SerializeField] private GameObject ball = null;
//    //Transforms
//    [SerializeField] private Transform hitPoint = null;
//    //Storages
//    [SerializeField] private ParticlesStorage particlesStorage = null;

//    [SerializeField] private  float speed = 0.4f;

//    private bool isHit = false;
//    private bool returnedToPlace = false;
//    private Vector2 startPosition;
//    private Damage damage = null;

//    [SerializeField] private VoidEvent onJoystickBallFinishedAttack = null;
//    [SerializeField] private NammiesMaker nammiesMaker = null;


//    private void Start()
//    {
//        startPosition = transform.position;
//    }

//    public void Hit(Damage damage)
//    {
//        this.damage = damage;
//        StartCoroutine(Move());

//    }

//    private IEnumerator Move()
//    {

//        print("in Move");

//        while (!isHit)
//        {
//            print("in first while");


//            MoveToHitPoint();

//            print("joystickBall pos " + transform.position.y);
//            print("joystickBall hitPoint pos" + hitPoint.position.y);

//            if (Mathf.Approximately(transform.position.y,hitPoint.position.y))
//            {
//                //nammiesMaker.MakeNammies(Random.Range(10, 25));
//                isHit = true;
//                Vector2 newPos = transform.position;
//                GameObject bombParticle = Instantiate(particlesStorage.GetPrizeParticle(), newPos, Quaternion.identity);
//            }

//            yield return new WaitForEndOfFrame();
//        }



//        while (true)
//        {
//            print("in second while");
//            MoveBack();

//            print("my pos is " + transform.position.y);
//            print("start pos is " + startPosition.y);


//            if (Mathf.Approximately(transform.position.y,startPosition.y))
//            {
//                print("im eqaul bitch");
//                returnedToPlace = true;
//                isHit = false;
//                returnedToPlace = false;
//                onJoystickBallFinishedAttack.Raise();
//                yield break;
//            }

//            yield return new WaitForEndOfFrame();
//        }



      
//    }



//    private void MoveToHitPoint()
//    {
//        transform.position = Vector2.MoveTowards(transform.position, hitPoint.position, speed  * Time.deltaTime);


//    }

//    private void MoveBack()
//    {
//        transform.position = Vector2.MoveTowards(transform.position, startPosition, speed  * Time.deltaTime );
//    }
//    public float GetSpeed()
//    {
//        return this.speed;
//    }

//    public Damage GetDamage()
//    {
//        return this.damage;
//    }
//}
