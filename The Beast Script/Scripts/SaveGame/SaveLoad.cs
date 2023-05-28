using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using TMPro;

public class SaveLoad : MonoBehaviour
{
    public GameDataS GData = new GameDataS();
    
    public string FileName = "SaveGame";

    Player_Controller player;

    public List<CollectibleData> BCollData = new List<CollectibleData>();

    private void Awake()
    {
        player = FindObjectOfType<Player_Controller>();
    }

    public void SaveGame()
    {
        SavedPlayerData();

        BCollData.Clear();
        GData.CollData.Clear();

        foreach (var Collect in FindObjectsOfType<Collectible>(includeInactive: true))
        {
            BCollData.Add(Collect.C_Data);
            GData.CollData.Add(Collect.C_Data);
        }
        
        string Savedata = JsonUtility.ToJson(GData);
        string SaveFIle = Application.persistentDataPath + FileName + ".json";
        File.WriteAllText(Application.persistentDataPath + FileName + ".json", Savedata);

        if (SaveFIle.Length > 0)
        {
            Debug.Log("Save Successful");
        }
    }

    private void SavedPlayerData()
    {
        GData.PlayerPosition = player.Transform.position;
        GData.PlRotation = player.Transform.rotation;
        GData.Health = player.CurrentHealth;
        GData.QuestActive = player.quest.IsActive;
        GData.QuestId = player.quest.QuestId;
        GData.QuestTitle = player.quest.Title;
        GData.CollectibleCount = player.Cubes;
    }

    public void LoadGame()
    {        
        string FilePath = Application.persistentDataPath + FileName + ".json";

        if (File.Exists(FilePath))
        {
            Debug.Log("Load File Present");
        }

        string LoadFile = File.ReadAllText(FilePath);
        GData = JsonUtility.FromJson<GameDataS>(LoadFile);
        LoadGameData();

        //Works for storing and loading collectibles - when game loads collected data is restored and 
        // object is not active in scene --working!
        foreach (var Collect in FindObjectsOfType<Collectible>(includeInactive: true))
        {
            var CollData = GData.CollData.FirstOrDefault(x => x.Coll_Id == Collect.C_Id);
            Collect.LoadGame(CollData);
            Debug.Log(CollData);            
        }  
    }

    private void LoadGameData()
    {
        //Player Data
        player.Transform.position = GData.PlayerPosition;
        player.Transform.rotation = GData.PlRotation;
        player.CurrentHealth = GData.Health;
        player.quest.IsActive = GData.QuestActive;
        player.quest.QuestId = GData.QuestId;
        player.quest.Title = GData.QuestTitle;
        player.Cubes = GData.CollectibleCount;

        if (player.IsDead)
        {
            player.IsDead = false;
            player.gameObject.SetActive(true);
        }

        Debug.Log("Load Successful");
    }

    //Delete save file on quitting game
    private void OnApplicationQuit()
    {
        string SaveFIlePath = Application.persistentDataPath + FileName + ".json";
        string metaFilePath = Application.persistentDataPath + FileName + ".json.meta";

        if (File.Exists(SaveFIlePath))
        {
            File.Delete(SaveFIlePath);
            File.Delete(metaFilePath);
            Debug.Log("Deleted File");
        }
    }
}

public interface ISaveAble
{
    void SaveGame();
    void LoadGame();
}
