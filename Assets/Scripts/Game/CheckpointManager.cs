using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    public string PreviousSceneName;
    

    public static CheckpointManager Instance
    {
        get;
        set;
    }

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        Instance = this;
    }

    public void SaveScene()
    {
        PreviousSceneName = SceneManager.GetActiveScene().name;
    }
}
