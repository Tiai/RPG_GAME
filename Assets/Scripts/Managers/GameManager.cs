//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class GameManager : MonoBehaviour, ISaveManager
//{
//    public static GameManager instance;

//    private Transform player;

//    [SerializeField] private Checkpoint[] checkpoints;
//    [SerializeField] private string closestCheckpointId;

//    [Header("Lost souls")]
//    [SerializeField] private GameObject lostSoulsPrefab;
//    public int lostCurrencyAmount;
//    [SerializeField] private float lostCurrencyX;
//    [SerializeField] private float lostCurrencyY;

//    private void Awake()
//    {
//        if (instance != null)
//        {
//            Destroy(instance.gameObject);
//        }
//        else
//        {
//            instance = this;
//        }
//    }

//    private void Start()
//    {
//        checkpoints = FindObjectsOfType<Checkpoint>();

//        player = PlayerManager.instance.player.transform;
//    }

//    //public void RestartScene()
//    //{
//    //    SaveManager.instance.SaveGame();

//    //    SaveManager.instance.LoadGame();

//    //    Scene scene = SceneManager.GetActiveScene();

//    //    SceneManager.LoadScene(scene.name);
//    //}

//    public void RestartScene()
//    {
//        SaveManager.instance.SaveGame();

//        Scene scene = SceneManager.GetActiveScene();
//        SceneManager.LoadScene(scene.name);

//        StartCoroutine(LoadGameData());
//    }


//    private IEnumerator LoadGameData()
//    {
//        yield return new WaitForEndOfFrame();

//        GameData loadedGameData = SaveManager.instance.LoadGame();
//        LoadData(loadedGameData);
//    }

//    public void LoadData(GameData _data) => StartCoroutine(LoadWithDelay(_data));

//    private void LoadCheckpoints(GameData _data)
//    {
//        foreach (KeyValuePair<string, bool> pair in _data.checkpoints)
//        {
//            foreach (Checkpoint checkpoint in checkpoints)
//            {
//                if (checkpoint.id == pair.Key && pair.Value)
//                {
//                    checkpoint.ActivateCheckpoint();
//                }
//            }
//        }
//    }

//    private void LoadLostCurrency(GameData _data)
//    {
//        lostCurrencyAmount = _data.lostCurrencyAmount;
//        lostCurrencyX = _data.lostCurrencyX;
//        lostCurrencyY = _data.lostCurrencyY;

//        if (lostCurrencyAmount > 0)
//        {
//            GameObject newLostCurrency = Instantiate(lostSoulsPrefab, new Vector3(lostCurrencyX, lostCurrencyY), Quaternion.identity);
//            newLostCurrency.GetComponent<LostSoulsController>().currency = lostCurrencyAmount;
//        }

//        lostCurrencyAmount = 0;
//    }

//    private IEnumerator LoadWithDelay(GameData _data)
//    {
//        yield return new WaitForSeconds(.1f);

//        LoadCheckpoints(_data);
//        LoadClosestCheckpoint(_data);
//        LoadLostCurrency(_data);
//    }

//    public void SaveData(ref GameData _data)
//    {
//        _data.lostCurrencyAmount = lostCurrencyAmount;
//        _data.lostCurrencyX = player.position.x;
//        _data.lostCurrencyY = player.position.y;

//        if(FindClosestCheckpoint()!= null)
//        {
//            _data.closestCheckpointId = FindClosestCheckpoint().id;
//        }

//        _data.checkpoints.Clear();

//        foreach (Checkpoint checkpoint in checkpoints)
//        {
//            _data.checkpoints.Add(checkpoint.id, checkpoint.isActive);
//        }
//    }

//    private void LoadClosestCheckpoint(GameData _data)
//    {
//        if(_data.closestCheckpointId == null)
//        {
//            return;
//        }

//        closestCheckpointId = _data.closestCheckpointId;

//        foreach (Checkpoint checkpoint in checkpoints)
//        {
//            if (closestCheckpointId == checkpoint.id)
//            {
//                player.position = checkpoint.transform.position;
//            }
//        }
//    }

//    private Checkpoint FindClosestCheckpoint()
//    {
//        float closestDistance = Mathf.Infinity;
//        Checkpoint closestCheckpoint = null;

//        foreach (Checkpoint checkpoint in checkpoints)
//        {
//            float distanceToCheckpoint = Vector2.Distance(player.position, checkpoint.transform.position);

//            if (distanceToCheckpoint < closestDistance && checkpoint.isActive)
//            {
//                closestDistance = distanceToCheckpoint;
//                closestCheckpoint = checkpoint;
//            }
//        }

//        return closestCheckpoint;
//    }

//    public void PauseGame(bool _pause)
//    {
//        if(_pause)
//        {
//            Time.timeScale = 0;
//        }
//        else
//        {
//            Time.timeScale = 1;
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISaveManager
{
    public static GameManager instance;

    private Transform player;

    [SerializeField] private Checkpoint[] checkpoints;
    [SerializeField] private string closestCheckpointId;

    [Header("Lost souls")]
    [SerializeField] private GameObject lostSoulsPrefab;
    public int lostCurrencyAmount;
    [SerializeField] private float lostCurrencyX;
    [SerializeField] private float lostCurrencyY;

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
        checkpoints = FindObjectsOfType<Checkpoint>();

        player = PlayerManager.instance.player.transform;
    }

    public void RestartScene()
    {
        SaveManager.instance.SaveGame();

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);

        StartCoroutine(LoadGameData());
    }

    private IEnumerator LoadGameData()
    {
        yield return null;

        GameData loadedGameData = SaveManager.instance.LoadGame();
        LoadData(loadedGameData);
    }

    public void LoadData(GameData _data)
    {
        StartCoroutine(LoadWithDelay(_data));
    }

    private IEnumerator LoadWithDelay(GameData _data)
    {
        yield return new WaitForSeconds(.1f);

        LoadCheckpoints(_data);
        LoadClosestCheckpoint(_data);
        LoadLostCurrency(_data);
    }

    private void LoadCheckpoints(GameData _data)
    {
        foreach (KeyValuePair<string, bool> pair in _data.checkpoints)
        {
            foreach (Checkpoint checkpoint in checkpoints)
            {
                if (checkpoint.id == pair.Key && pair.Value)
                {
                    checkpoint.ActivateCheckpoint();
                }
            }
        }
    }

    private void LoadLostCurrency(GameData _data)
    {
        lostCurrencyAmount = _data.lostCurrencyAmount;
        lostCurrencyX = _data.lostCurrencyX;
        lostCurrencyY = _data.lostCurrencyY;

        if (lostCurrencyAmount > 0)
        {
            GameObject newLostCurrency = Instantiate(lostSoulsPrefab, new Vector3(lostCurrencyX, lostCurrencyY), Quaternion.identity);
            newLostCurrency.GetComponent<LostSoulsController>().currency = lostCurrencyAmount;
        }

        lostCurrencyAmount = 0;
    }

    public void SaveData(ref GameData _data)
    {
        _data.lostCurrencyAmount = lostCurrencyAmount;
        _data.lostCurrencyX = player.position.x;
        _data.lostCurrencyY = player.position.y;

        if (FindClosestCheckpoint() != null)
        {
            _data.closestCheckpointId = FindClosestCheckpoint().id;
        }

        _data.checkpoints.Clear();

        foreach (Checkpoint checkpoint in checkpoints)
        {
            _data.checkpoints.Add(checkpoint.id, checkpoint.isActive);
        }
    }

    private void LoadClosestCheckpoint(GameData _data)
    {
        if (_data.closestCheckpointId == null)
        {
            return;
        }

        closestCheckpointId = _data.closestCheckpointId;

        foreach (Checkpoint checkpoint in checkpoints)
        {
            if (closestCheckpointId == checkpoint.id)
            {
                player.position = checkpoint.transform.position;
            }
        }
    }

    private Checkpoint FindClosestCheckpoint()
    {
        float closestDistance = Mathf.Infinity;
        Checkpoint closestCheckpoint = null;

        foreach (Checkpoint checkpoint in checkpoints)
        {
            float distanceToCheckpoint = Vector2.Distance(player.position, checkpoint.transform.position);

            if (distanceToCheckpoint < closestDistance && checkpoint.isActive)
            {
                closestDistance = distanceToCheckpoint;
                closestCheckpoint = checkpoint;
            }
        }

        return closestCheckpoint;
    }

    public void PauseGame(bool _pause)
    {
        if (_pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
