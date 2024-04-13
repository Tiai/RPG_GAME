using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    [SerializeField] private string fileName;
    [SerializeField] private bool isEncrypt;

    public GameData gameData;
    private List<ISaveManager> saveManagers;
    private FileDataHandler dataHandler;

    [ContextMenu("Delete save file")]
    public void DeleteSavedData()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, isEncrypt);
        dataHandler.Delete();
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, isEncrypt);
        saveManagers = FindAllSaveManagers();

        LoadGame();
    }

    public void NewGame()
    {
        gameData = new GameData();
    }

    //public void LoadGame()
    //{
    //    gameData = dataHandler.Load();

    //    if (this.gameData == null)
    //    {
    //        Debug.Log("No save data");
    //        NewGame();
    //    }

    //    foreach (ISaveManager saveManager in saveManagers)
    //    {
    //        saveManager.LoadData(gameData);
    //    }
    //}

    //public void LoadGame()
    //{
    //    gameData = dataHandler.Load();

    //    if (gameData == null)
    //    {
    //        Debug.Log("No save data");
    //        NewGame();
    //    }
    //    else if (gameData.skillTree == null || gameData.skillTree.Count == 0)
    //    {
    //        Debug.Log("No skill data found");
    //        NewGame();
    //    }
    //    else
    //    {
    //        foreach (ISaveManager saveManager in saveManagers)
    //        {
    //            saveManager.LoadData(gameData);
    //        }
    //    }
    //}

    public GameData LoadGame()
    {
        gameData = dataHandler.Load();

        if (gameData == null)
        {
            Debug.Log("No save data");
            NewGame();
        }
        else if (gameData.skillTree == null || gameData.skillTree.Count == 0)
        {
            Debug.Log("No skill data found");
            NewGame();
        }
        else
        {
            foreach (ISaveManager saveManager in saveManagers)
            {
                saveManager.LoadData(gameData);
            }
        }

        return gameData;
    }

    public void SaveGame()
    {
        foreach (ISaveManager saveManager in saveManagers)
        {
            saveManager.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<ISaveManager> FindAllSaveManagers()
    {
        IEnumerable<ISaveManager> saveManagers = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();

        return new List<ISaveManager>(saveManagers);
    }

    public bool HasSaveData()
    {
        if (dataHandler.Load() != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
