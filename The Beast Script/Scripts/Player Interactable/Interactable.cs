using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour
{
    Weapon_Manager WeaponsManage;

    public HintText HintRef;
    public TMP_Text Hint;
    public GameObject HintObject;
    public int WpId;
    public bool AxeIsCollected { get; set; }
    public bool SwordIsCollected { get; set; }
    public bool GSwordIsCollected { get; set; }


    //Collect Player Weapons
    private void OnTriggerEnter(Collider other)
    {
        WeaponsManage = other.GetComponent<Weapon_Manager>();

        if(WeaponsManage != null)
        {
            //WeaponManager is a component attached to player
            //Checks the Game object name, adds and activates the specified object in player
            if(gameObject.name == "Axe")
            {
                //Adds weapon to player
                WeaponsManage.GetAxe(0);
                WeaponsManage.AxeCollected = true;
                AxeIsCollected = true;

                //shows key hint to player on ui
                Hint.text = HintRef.Hint;
                HintObject.SetActive(true);
            }

            if (gameObject.name == "Sword")
            {
                //Adds weapon to player
                WeaponsManage.GetSword(0);
                WeaponsManage.SwordCollected = true;
                SwordIsCollected = true;

                //shows key to player on ui
                Hint.text = HintRef.Hint;
                HintObject.SetActive(true);
            }

            if (gameObject.name == "DH_Sword")
            {
                //Add weapon to player
                WeaponsManage.GetDHSword(0);
                WeaponsManage.DHSwordCollected = true;
                GSwordIsCollected = true;

                //Show key to player on ui
                Hint.text = HintRef.Hint;
                HintObject.SetActive(true);
            }

            //Destroys Game Object after collecting
            Destroy(gameObject);
        }
    }
}
