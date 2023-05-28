using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PoolingObj[] EnemyObjs;
    public GameObject DeathPanel;
    public GameObject Credits;
    Player_Controller Player;


    private void Start()
    {
        //Credits.SetActive(false);
        Player = FindObjectOfType<Player_Controller>();
    }

    //From Player Controller on collision Checks the string tags and gets index using below code
    public GameObject GetObjFromPooling(string Tag, Vector3 Pos, Quaternion Rot)
    {
        return GetObjFromPooling(GetIndexFromTag(Tag), Pos, Rot);
    }


    //With the string tags collected from storedData checkss and switches between the gameobjects and gets the object index 
    public int GetIndexFromTag(string Tag)
    {
        switch (Tag)
        {
            case "Enemy 1":
                return 0;
            case "Enemy 2":
                return 1;
        }

        return 0;
    }
    
    //with the collected index from the above code uses it and returns to the GetObjFromPool part again
    public GameObject GetObjFromPooling(int Index, Vector3 Pos, Quaternion Rot)
    {
        int QueCount = EnemyObjs[Index].EnemyObjts.Count;

        GameObject QueGameObject;
        if(QueCount > 0)
        {
            QueGameObject = EnemyObjs[Index].EnemyObjts.Dequeue();
            QueGameObject.SetActive(true);
            QueGameObject.transform.position = Pos;
            QueGameObject.transform.rotation = Rot;
        }
        else
        {
            QueGameObject = Instantiate(EnemyObjs[Index].Reference, Pos, Rot);
        }

        return QueGameObject;
    }

    public void EnqueToQue(GameObject gameobject, string tag)
    {
        EnemyObjs[GetIndexFromTag(tag)].EnemyObjts.Enqueue(gameobject);
    }

    public void EndGame()
    {
        if(Player.IsDead)
        {
            //Restart from Checkpoint Panel
            DeathPanel.SetActive(true);

            //With Retry button - open the game scene in last save point
            //and A exit to main menu button - exits to main menu
            //With Quit Button - Quit Game
        }        
    }

    public void GameComplete()
    {
        //var PlayCredits = GameObject.FindGameObjectWithTag("Credits");
        //PlayCredits.gameObject.SetActive(true);

        Credits.SetActive(true);
        Player.gameObject.SetActive(false);
    }
}
