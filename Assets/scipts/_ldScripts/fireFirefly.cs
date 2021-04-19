using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireFirefly : MonoBehaviour
{
    private Vector3 mousePosition;
    private Quaternion mouseDirection;
    public float fireFlySpawnDistance;

    public GameObject fireFly; 

    private Vector2 spawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10; // select distance = 10 units from the camera
        mousePosition = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
        mouseDirection.z = 0;
        
        mouseDirection.SetLookRotation(mousePosition, Vector2.up);
        spawn.Set(Mathf.Sin(Mathf.Rad2Deg * Mathf.Atan(mousePosition.x / mousePosition.y)) / fireFlySpawnDistance, Mathf.Cos(Mathf.Rad2Deg * Mathf.Atan(mousePosition.x / mousePosition.y)) / fireFlySpawnDistance); 

        if(Input.GetKeyDown(KeyCode.E)){
            Instantiate(fireFly, spawn, mouseDirection);
            print(mousePos);
            
        }
            
    }

}
