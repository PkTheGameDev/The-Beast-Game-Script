using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class RestartChpT : MonoBehaviour
{
    public Button RestartBtn;
    public SaveLoad LoadG;
    string FileName = "SaveGame";

    private void Update()
    {
        //Checks whether save game is present in the folder
        string FilePath = Application.persistentDataPath + FileName + ".json";

        if (File.Exists(FilePath))
        {
            //if present sets the button as interactable
            RestartBtn.interactable = true;
        }
        else
        {
            //if not its not interactable
            RestartBtn.interactable = false;
        }
    }

    public void LoadGameFromChpt()
    {
        //Loads game from save file when button is clicked
        LoadG.LoadGame();
    }
}
