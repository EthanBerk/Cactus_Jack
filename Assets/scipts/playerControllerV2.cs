using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControllerV2 : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    public float speed = 2;
    private bool isWalking = false;
    private bool canWalkB;

    // Start is called before the first frame update
    private Vector2 walkingDirection = new Vector2(0, 1);

    void Start()
    {
        boxCollider2D = this.gameObject.GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    private Vector3 facingDirection = new Vector3(0, 1, 0);
    Vector3 startLoaction;
    void Update()
    {
        print("test");
        Collider[] hitColliders = Physics.OverlapBox(boxCollider2D.bounds.center, boxCollider2D.bounds.extents, transform.rotation);
        Debug.DrawLine(boxCollider2D.bounds.center, boxCollider2D.bounds.extents);
        print(hitColliders.Length);
        foreach(Collider contact in hitColliders){
            print(contact.gameObject);
        } 
        if (isWalking)
        {
            MoveObject(facingDirection, gameObject, isWalking, 16, startLoaction);
        }
        else if (WASDpressed() && !isWalking && canWalkB) {
            isWalking = true;
            startLoaction = gameObject.transform.position;
        }
    }
    private ContactPoint2D[] contacts;
    void OnCollisionStay2D(Collision2D collision)
    {
        if (WASDpressed() && !isWalking){
        canWalkB = canWalk(facingDirection, collision.contacts);
        }
    }
    
    private Vector2 playerPosition = Vector2.zero;
    public void MoveObject(Vector2 direction, GameObject gameObject, bool inputIsWalking, float distance, Vector3 startLoaction)
    {
        Vector3 goalLocation = new Vector3(direction.x * distance, direction.y * distance, 0) + startLoaction;
        if (inputIsWalking)
        {
            if (goalLocation == gameObject.transform.position)
            {
                isWalking = false;
            }
            else
            {
                gameObject.transform.position = Vector2.MoveTowards(transform.localPosition, goalLocation, Time.fixedDeltaTime * speed);
            }
        }
    }
    public bool WASDpressed()
    {


        if (Input.GetKeyDown(KeyCode.W))
        {
            facingDirection = new Vector2(0, 1);
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            facingDirection = new Vector2(0, -1);
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            facingDirection = new Vector2(1, 0);
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            facingDirection = new Vector2(-1, 0);
            return true;
        }
        else
        {
            return false;
        }

    }
    public bool canWalk(Vector2 direction, ContactPoint2D[] contacts)
    {
        if(contacts == null){
            return true;
        }
        
        foreach (ContactPoint2D contact in contacts)
        {
        
            if (contact.collider.transform.tag == "walls" && -contact.normal == direction)
            {
                return false;
            }
            else if (contact.collider.gameObject.layer == 8 && !contact.collider.gameObject.GetComponent<targetControllerV2>().canMove(direction))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        return true;
    }
    public bool isContactRistricting(ContactPoint2D contact, GameObject currentObject, Vector2 direction)
    {
        Vector2 contactPosition = contact.collider.transform.position;
        Vector2 CenterPosition = currentObject.GetComponent<BoxCollider2D>().bounds.center;
        if (-contact.normal == direction && (CenterPosition.x == contactPosition.x || CenterPosition.y == contactPosition.x))
        {
            return true;
        }
        return false;

    }
    public Vector2 getPlayerDirection(){
        return walkingDirection;
    }
    public bool getIsWalking(){
        return isWalking;
    }
}

