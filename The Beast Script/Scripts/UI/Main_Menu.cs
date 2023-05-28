using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_Menu : MonoBehaviour
{
    public GameObject LoadScreen;
    public Image ProgressBar;
    public string FileName = "SaveGame";

    //Load Scene when clicking start btn.
    public void StartGame(int i)
    {
        GLoadScreen();
        StartCoroutine(LoadMainScene(i));
    }  

    //Quit Application to desktop on clicking exit
    public void ExitApplication()
    {
        Application.Quit();
    }

    //Loading main menu
    public void loadMainMenu(int i)
    {
        string SaveFIlePath = Application.persistentDataPath + FileName + ".json";
        string metaFilePath = Application.persistentDataPath + FileName + ".json.meta";

        if (File.Exists(SaveFIlePath))
        {
            File.Delete(SaveFIlePath);
            File.Delete(metaFilePath);
        }
        GLoadScreen();
        StartCoroutine(LoadMainScene(i));
    }

    //set load screen active
    void GLoadScreen()
    {
        LoadScreen.SetActive(true);
    }

    //loading bar co routine
    IEnumerator LoadMainScene(int i)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(i);

        while(!operation.isDone)
        {
            float progressValue;
            progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            ProgressBar.fillAmount = progressValue;

            yield return null;
        }
    }
}
