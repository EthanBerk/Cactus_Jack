using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetControllerV2 : MonoBehaviour
{
    playerControllerV2 playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<playerControllerV2>();
    }

    // Update is called once per frame
    private bool isMoving;
    private Vector3 startLocationTarget = Vector3.zero;
    private bool firstLoopWalking;
    void Update()
    {

        if (playerController.getIsWalking() && isPushed())
        {
            isMoving = true;
        }
        if (isMoving)
        {
            playerController.MoveObject(playerController.getPlayerDirection(), this.gameObject, isMoving, 16, startLocationTarget);
        }

    }
    private ContactPoint2D[] contacts;
    void OnCollisionStay2D(Collision2D collision)
    {
        contacts = collision.contacts;
    }
    public bool canMove(Vector2 direction)
    {

        foreach (ContactPoint2D contact in contacts)
        {
            if (contact.collider.transform.CompareTag("walls") && playerController.isContactRistricting(contact, this.gameObject, playerController.getPlayerDirection()))
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
    public bool isPushed()
    {
        foreach (ContactPoint2D contact in contacts)
        {
            if (contact.collider.gameObject == GameObject.Find("Player") && playerController.isContactRistricting(contact, this.gameObject, playerController.getPlayerDirection()))
            {
                return true;
            }
            else if (contact.collider.gameObject.layer.Equals(8) && playerController.isContactRistricting(contact, this.gameObject, playerController.getPlayerDirection()))
            {
                return contact.collider.gameObject.GetComponent<targetControllerV2>().isPushed();
            }
        }
        return false;
    }
}
