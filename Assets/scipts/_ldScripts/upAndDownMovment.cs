using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upAndDownMovment : MonoBehaviour
{
    public float speed = 3;
    public float minHeight = 0.1f;
    public float maxHeight = 4;
    private bool up = true;
    GameObject gameObjectGoal;
    
    // Start is called before the first frame update
    void Start()
    {
       minHeight += gameObject.transform.position.y;
       maxHeight += gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void FixedUpdate(){
        if(up){
            if(gameObject.transform.position.y >= maxHeight){
                up = false;
                
            }
            else{
               gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + speed);
               //print(Mathf.MoveTowards(gameObject.transform.position.x, maxHeight, speed));
               

            }
        }
        if(!up){
            if(gameObject.transform.position.y <= minHeight){
                up = true;
                
            }
            else{
                gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - speed);
                //print("pos" + -gameObject.transform.position.y * speed * Time.deltaTime);
                
            }
        } 
    }

    private void upDown(){
       
    }
}
