using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerControllerMix : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    private Animator playerAnimator;
    private Rigidbody2D rigidbody2d;
    private AudioSource audioSource;

    public float speed = 2;
    public float hitGoal = 3;
    public float waitTime = 3;
    public float ammo = 3;
    private bool isWalking = false;
    private GameObject pushing;
    private string direction;
    private int animationDirection;
    private Vector2 startLocation;
    private Vector3 startTargetLocation;

    private Vector3 shootDirection;

    private Vector3 gps;
    private Vector3 gpsd;
    private Vector3 orign;
    private int moveCounter = 0;
    public int maxMoves = 0;
    private int hitCounter = 0;
    private List<GameObject> hitTagets = new List<GameObject>();
    private int layerMask = 1 << 8;
    public GameObject ammoTxt;
    public GameObject targetHitText;
    public GameObject movesTxt;
    public GameObject goalTxt;
    public GameObject winMenu;
    public GameObject controllMenu;
    public GameObject loseMenu;
    bool won = false;

    // Start is called before the first frame update
    void Start()
    {
        loseMenu.SetActive(false);
        winMenu.SetActive(false);
        boxCollider2D = this.gameObject.GetComponent<BoxCollider2D>();
        rigidbody2d = this.gameObject.GetComponent<Rigidbody2D>();
        playerAnimator = this.gameObject.GetComponent<Animator>();
        audioSource = this.gameObject.GetComponent<AudioSource>();
        startLocation = gameObject.transform.position;
        shootDirection.Set(0, 0, 0);
        orign = gameObject.transform.position;


    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine (winCheck());
        losecheck();
        if(Input.GetKey(KeyCode.C)){
        controllMenu.SetActive(true);
     }   
     else{
        controllMenu.SetActive(false);
     }
        print(gpsd);
        ammoTxt.GetComponent<UnityEngine.UI.Text>().text = ammo.ToString();
        targetHitText.GetComponent<UnityEngine.UI.Text>().text = hitCounter.ToString();
        goalTxt.GetComponent<UnityEngine.UI.Text>().text = hitGoal.ToString();
        movesTxt.GetComponent<UnityEngine.UI.Text>().text = (maxMoves - moveCounter).ToString();
        playerAnimator.SetBool("isShooting", Input.GetKeyDown(KeyCode.Space) && !isWalking);
        gpsd = new Vector2(16 * gps.x, 16 * gps.y);

        if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
        
        

        movement();

        if (Input.GetKeyDown(KeyCode.Space) && !isWalking)
        {
            ammo--;
            audioSource.Play();
            
            
            RaycastHit2D[] hits;
            hits = Physics2D.RaycastAll(boxCollider2D.bounds.center, shootDirection, Mathf.Infinity, layerMask);
            print(hits.Length);
            foreach(RaycastHit2D i in hits)
            {
                RaycastHit2D hit = i;

                if (hit.collider != null)
                {
                    bulletHitCheck(hit.transform.tag, hit.transform.gameObject);
                }


            }

        }
        playerAnimator.SetInteger("direction", animationDirection);
        playerAnimator.SetBool("isWalking", isWalking);
    }
    void FixedUpdate()
    {


    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            pushing = collision.gameObject;
            startTargetLocation = pushing.transform.position;
        }
        else
        {
            pushing = null;
            isWalking = false;
            gameObject.transform.position = startLocation;
            startTargetLocation = new Vector3(0, 0, 0);
        }
    }
    void movement()
    {
        if (isWalking)
        {
            switch (direction)
            {
                case "north":
                    if (gameObject.transform.position.y - startLocation.y >= 16)
                    {
                        isWalking = false;
                        gps.y++;
                        updategpsd();
                        setTargetPush();
                        StartCoroutine (winCheck());
                        losecheck();

                        gameObject.transform.position = orign + gpsd;

                        startLocation = gameObject.transform.position;
                        pushing = null;


                    }
                    else
                    {
                        gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + speed * Time.deltaTime);

                    }
                    break;
                case "south":
                    if (gameObject.transform.position.y - startLocation.y <= -16)
                    {
                        isWalking = false;
                        gps.y--;
                        updategpsd();
                        setTargetPush();
                        StartCoroutine (winCheck());
                        losecheck();
                        gameObject.transform.position = orign + gpsd;
                        //gameObject.transform.position = new Vector2(startLocation.x, startLocation.y - 16);
                        startLocation = gameObject.transform.position;
                        pushing = null;


                    }
                    else
                    {
                        gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + -speed * Time.deltaTime);


                    }
                    break;
                case "east":
                    if (gameObject.transform.position.x - startLocation.x >= 16)
                    {
                        isWalking = false;
                        gps.x++;
                        updategpsd();
                        setTargetPush();
                        StartCoroutine (winCheck());
                        losecheck();
                        gameObject.transform.position = orign + gpsd;
                        //gameObject.transform.position = new Vector2(startLocation.x + 16, startLocation.y);
                        startLocation = gameObject.transform.position;
                        pushing = null;

                    }
                    else
                    {
                        gameObject.transform.position = new Vector2(gameObject.transform.position.x + speed * Time.deltaTime, gameObject.transform.position.y);
                    }
                    break;
                case "west":
                    if (gameObject.transform.position.x - startLocation.x <= -16)
                    {
                        isWalking = false;
                        gps.x--;
                        updategpsd();
                        setTargetPush();
                        StartCoroutine (winCheck());
                        losecheck();
                        gameObject.transform.position = orign + gpsd;
                        // gameObject.transform.position = new Vector2(startLocation.x - 16, startLocation.y);
                        startLocation = gameObject.transform.position;
                        pushing = null;

                    }
                    else
                    {
                        gameObject.transform.position = new Vector2(gameObject.transform.position.x + -speed * Time.deltaTime, gameObject.transform.position.y);

                    }
                    break;
            }
            pushObject();

        }
        else
        {


            if (Input.GetKeyDown(KeyCode.W))
            {
                direction = "north";
                animationDirection = 0;
                isWalking = true;
                shootDirection.Set(0, 1, 0);
                moveCounter++;

            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                direction = "south";
                animationDirection = 2;
                isWalking = true;
                shootDirection.Set(0, -1, 0);
                moveCounter++;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                direction = "west";
                animationDirection = 3;
                isWalking = true;
                shootDirection.Set(-1, 0, 0);
                moveCounter++;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                direction = "east";
                animationDirection = 1;
                isWalking = true;
                shootDirection.Set(1, 0, 0);
                moveCounter++;
            }
        }
    }
    void bulletHitCheck(string direction, GameObject gameObject)
    {
        if (this.direction == direction && !hitTagets.Contains(gameObject))
        {
            hitCounter++;
            hitTagets.Add(gameObject);
            StartCoroutine (winCheck());
            losecheck();
        }
        print(hitCounter);

    }
    void pushObject()
    {
        if (pushing == null)
        {
            return;
        }
        else
        {

            Vector3 moveDistance = new Vector3(startLocation.x - gameObject.transform.position.x, startLocation.y - gameObject.transform.position.y, 0);
            pushing.transform.position = new Vector3(Mathf.Abs(moveDistance.x) * shootDirection.x, Mathf.Abs(moveDistance.y) * shootDirection.y, 0) + startTargetLocation;

        }
    }
    public void setTargetPush()
    {
        if (pushing != null)
        {
            pushing.transform.position = startTargetLocation + new Vector3(shootDirection.x * 16, shootDirection.y * 16, 0);


        }

    }
    public void stop()
    {
        gameObject.transform.position = gpsd + orign;
        if (pushing != null)
        {
            pushing.transform.position = startTargetLocation;
        }
        pushing = null;
        isWalking = false;

        startTargetLocation = new Vector3(0, 0, 0);


    }

    public Vector2 getShootDirection()
    {
        return shootDirection;
    }
    public bool isShoot(GameObject target)
    {
        return hitTagets.Contains(target);
    }
    void lose()
    
    {
        
        loseMenu.SetActive(true);

    }
    
    void win(){
        winMenu.SetActive(true);
    }
    public bool getIsWalking()
    {
        return isWalking;
    }
    public bool getIspushing()
    {
        return pushing != null;
    }
    void updategpsd()
    {
        gpsd = new Vector2(16 * gps.x, 16 * gps.y);
    }
     IEnumerator winCheck()
     {
        
        yield return new WaitForSeconds(waitTime);
        
        if(hitCounter >= hitGoal){
            win();
            won = true;
        }

        
     }
     void losecheck(){
         if((moveCounter >= maxMoves + 1 || ammo <= -1) && !won){
            lose();
        }
     }
        

}
