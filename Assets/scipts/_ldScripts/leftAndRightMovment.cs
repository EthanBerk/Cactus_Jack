using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftAndRightMovement : MonoBehaviour
{
    public float speed = 3;
    public float minHeight = 0.1f;
    public float maxHeight = 4;
    private bool up = true;
    GameObject gameObjectGoal;
    
    // Start is called before the first frame update
    void Start()
    {
       minHeight += gameObject.transform.position.x;
       maxHeight += gameObject.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void FixedUpdate(){
        if(up){
            if(gameObject.transform.position.x >= maxHeight){
                up = false;
                
            }
            else{
               gameObject.transform.position = new Vector2(gameObject.transform.position.x + speed, gameObject.transform.position.y );
               //print(Mathf.MoveTowards(gameObject.transform.position.x, maxHeight, speed));
               

            }
        }
        if(!up){
            if(gameObject.transform.position.x <= minHeight){
                up = true;
                
            }
            else{
                gameObject.transform.position = new Vector2(gameObject.transform.position.x - speed, gameObject.transform.position.y);
                //print("pos" + -gameObject.transform.position.y * speed * Time.deltaTime);
                
            }
        } 
    }

    
}
