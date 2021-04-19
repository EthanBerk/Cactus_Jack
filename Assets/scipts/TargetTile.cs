using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTile : MonoBehaviour
{
    private bool isPressed = false;
    private woldplayercontroller worldplayer;

    private Animator Animator;
    // Start is called before the first frame update
    void Start()
    {
        Animator = this.gameObject.GetComponent<Animator>();
        worldplayer = GameObject.Find("Player").GetComponent<woldplayercontroller>();
    }

    // Update is called once per frame
    void Update()
    {
         
    }
    public void setIspressed(bool n){
        Animator.SetBool("isPressed", n);
    }

    
}
