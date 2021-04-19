using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMovment : MonoBehaviour
{
    //Components
    private Rigidbody2D rigidbody2d;
    private CapsuleCollider2D CapsuleCollider2D;
    [SerializeField] private LayerMask platformLayerMask;
    private Animator animatorController;
    private SpriteRenderer spriteRenderer;
    public GameObject winScreen;
    //Public constants
    public float movmentScalar;
    public float maxSpeed;
    public float jumpHight = 250;
    //constants 
    private static float boxCastOfset = 0.1f;
    

    //Varbiles 
    private RaycastHit2D playerStandingOn = new RaycastHit2D();
    private float direction;
    private bool canMove = true;
    private bool canJump = true;
    

    // Start is called before the first frame update
    void Start()
    {
        //Components 
        rigidbody2d = this.gameObject.GetComponent<Rigidbody2D>();
        CapsuleCollider2D = this.gameObject.GetComponent<CapsuleCollider2D>();
        animatorController = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        winScreen.SetActive(false);
        
    }
    private void OnTriggerEnter2D(Collider2D other){
        print("tag" + other.gameObject.tag);
        print("test");
        if(other.gameObject.CompareTag("Goal")){
            Destroy(other.gameObject);
            winScreen.SetActive(true);
            canMove = false;
            canJump = false;

        }

    }
    


    // Update is called once per frame
    void Update()
    {
        if(canJump){Jumpcontroller();}
        //Animations
        animatorController.SetBool("isMidair", !isGrounded());
        animatorController.SetBool("isWalk", rigidbody2d.velocity.x < -0.1 || rigidbody2d.velocity.x > 0.1);
        spriteRenderer.flipX = !(direction == -1);
    }

    void FixedUpdate()
    {
        
        if(canMove){Movmentcontroller();}


    }
    //Methods

    private void Movmentcontroller(){
        float x_movment = Input.GetAxis("Horizontal");
        if (rigidbody2d.velocity.magnitude < maxSpeed)
        {
            Vector2 targetVelocity = new Vector2(x_movment, 0);
            rigidbody2d.AddForce(targetVelocity * movmentScalar);
            if (targetVelocity.x != 0)
            {
                direction = (targetVelocity.x > 0) ? -1 : 1;
            }
        }
    }
    private void Jumpcontroller(){
        
        if (isGrounded() && Input.GetKeyDown(KeyCode.W))
        {
             RaycastHit2D raycastHit = Physics2D.BoxCast(CapsuleCollider2D.bounds.center, CapsuleCollider2D.bounds.size, 0f, Vector2.down, boxCastOfset, platformLayerMask);
             print("tag: " + raycastHit.collider.tag.ToString()); 
            Vector2 jumpForce = new Vector2(0, jumpHight);
            rigidbody2d.AddForce(jumpForce);
        }
    }
    public bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(CapsuleCollider2D.bounds.center, CapsuleCollider2D.bounds.size, 0f, Vector2.down, boxCastOfset, platformLayerMask);
        
        return raycastHit.collider != null;
        
    }

}
