using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Credits : MonoBehaviour
{
    public GameObject ShowKey;


    // Start is called before the first frame update
    void Start()
    {
        if(this.gameObject.activeSelf)
        {
            ShowKey.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

    }
}
