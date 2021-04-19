using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private LayerMask platformLayerMask;
    private BoxCollider2D boxCollider;

    private Animator animatorController;
    private Rigidbody2D r2d;

    private static float speed = 20;
    
    
    [Range(.001f, 1f)]
    public float boxOfset = 0.05f;
    [Range(1f, 100000f)]
    public float jumpforce = 1000;
    
    private bool grounded = false;
    public float walkAccleration = 0.5f;
    public float walkDeceleration = 0.1f;
    

    private Vector2 velocityTarget;
    private Vector2 currentVelocity;
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animatorController = GetComponent<Animator>();

    }

    // Update is called once per frame
    public bool isGrounded(){

       RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, boxOfset, platformLayerMask);
       print(raycastHit.collider);
       
       return raycastHit.collider != null;
       

   }
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.W) && isGrounded()){
            print("jump");
            r2d.AddForce(new Vector2(0, jumpforce));
            
        }
        
   

    }
        

    private void FixedUpdate()
    {
        
        velocityTarget.x = Input.GetAxis("Horizontal") * speed;
        r2d.velocity = Vector2.SmoothDamp(r2d.velocity, velocityTarget, ref currentVelocity, walkAccleration);
      
    
        if(r2d.velocity.x < -0.1 || r2d.velocity.x > 0.1){
            print("walking");
            animatorController.SetBool ("isWalk", true);
        }
        else{
            animatorController.SetBool("isWalk", false);
        }
        if(isGrounded()){
            animatorController.SetBool("isMidair", false);
        }
        else
        {
            animatorController.SetBool("isMidair", true);
        }
        
    }


}
