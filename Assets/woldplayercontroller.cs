using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class woldplayercontroller : MonoBehaviour
{
    List <Vector2> levelist;
    List <GameObject> selfaware;
    //List<int> 
    bool moving = false;
    public float speed = 15;
    int levelnum;
    public int levelAmount = 2;
    public int currentLevel = 0;
    public GameObject tileGroup;
    void Start()
    {
        levelAmount--;
       levelist = new List <Vector2>();
       selfaware = new List <GameObject>();
       for (int i = 0; i <= levelAmount; i++){
           string level = "Level" + i;
            levelist.Add(tileGroup.transform.Find(level).position); 
            selfaware.Add(tileGroup.transform.Find(level).gameObject);
       }
       levelnum = currentLevel;
       gameObject.transform.position = levelist[currentLevel];

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)){

            if (levelnum < levelAmount){
                selfaware[levelnum].GetComponent<TargetTile>().setIspressed(false);
                levelnum++;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D)){

            if (levelnum > 0){
                selfaware[levelnum].GetComponent<TargetTile>().setIspressed(false);
                levelnum--;
            }
        }

        gameObject.transform.position = Vector2.MoveTowards(transform.position, levelist[levelnum], speed);
        selfaware[levelnum].GetComponent<TargetTile>().setIspressed(true); 
        GameObject.Find("LevelNum").GetComponent<UnityEngine.UI.Text>().text = "Level " + (levelnum + 1);

    }
    public void EnterLevel(){
        SceneManager.LoadScene("Level" + (levelnum + 1));
    }

}
