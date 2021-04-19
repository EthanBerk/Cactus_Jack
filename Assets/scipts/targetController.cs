// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class targetController : MonoBehaviour
// {
//     // Start is called before the first frame update
//     private BoxCollider2D boxCollider2D;
//     private Rigidbody2D rigidbody2d;
//     public GameObject player;
//     private playerControllerMix playerScript;
//     private Vector3 startTargetLocationThis;

//     private Vector3 orgin;
//     private Vector2 postionByBlocks;
//     private ContactFilter2D contactFilter2D;
//     private int layerMask = 1 << 9;
//     private Animator targetAnimator;
//     bool hitPlayer = false;
//     Vector2 playerdirection;

//     void Start()
//     {
//         // contactFilter2D.useTriggers = false;
//         // contactFilter2D.SetLayerMask(layerMask);
//         // contactFilter2D.useLayerMask = true;
//         // boxCollider2D = this.gameObject.GetComponent<BoxCollider2D>();
//         // rigidbody2d = this.gameObject.GetComponent<Rigidbody2D>();
//         // playerScript = player.GetComponent<playerControllerMix>();
//         // targetAnimator = this.gameObject.GetComponent<Animator>();
//         // startTargetLocationThis = this.gameObject.transform.position;


//     }
//     void Update()
//     {


//         targetAnimator.SetBool("isShot", playerScript.isShoot(this.gameObject));
//         gameObject.transform.position = new Vector3(Mathf.Round(gameObject.transform.position.x), Mathf.Round(gameObject.transform.position.y), 0);





//     }
//     void OnCollisionStay2D(Collision2D collision)

//     {

//         //    Vector3 shootDirection = playerScript.getShootDirection();
//         //     Vector3 moveDistance = playerScript.getMoveDistance();
//         //     collision.transform.position = new Vector3(Mathf.Abs(moveDistance.x) * shootDirection.x, Mathf.Abs(moveDistance.y) * shootDirection.y, 0) + startTargetLocation;
//         foreach (ContactPoint2D contact in collision.contacts)
//         {

//             if (contact.collider.gameObject.tag == "Player")
//             {
//                 print("player");
//                 playerdirection = -contact.normal;
//             }
//             else if (contact.collider.gameObject.layer == 8 && contact.collider.gameObject != this.gameObject)
//             {
//                 Vector3 shootDirection = -contact.normal;
//                 Vector3 moveDistance = new Vector3(shootDirection.x * 16, shootDirection.y * 16, 0);
//                 if (-contact.normal == playerScript.getShootDirection())
//                 {
//                     contact.collider.transform.position = moveDistance + this.gameObject.transform.position;

//                 }



//             }
//             else if (contact.collider.gameObject.tag == "walls")
//             {
//                 if (-contact.normal == playerScript.getShootDirection() && playerScript.getIspushing())
//                 {
//                     print("stop");
//                     playerScript.stop();
//                 }

//             }



//         }







//         // print(collision.transform.gameObject);
//         // if (collision.gameObject.layer == 8)
//         // {
//         //     print("hitTarget");

//         // }
//         // else if(collision.gameObject.tag == "Player")
//         // {
//         //     print("player");


//         // }
//         // else{

//         //     playerScript.stop();
//         // }
//     }




//     // Update is called once per frame

// }

