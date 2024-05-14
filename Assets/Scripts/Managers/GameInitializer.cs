using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    public PlayerStats playerStats;
    private static GameInitializer _instance;
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            playerStats.InitStats();
        }
        else
        {
            Destroy(this.gameObject);
            Debug.LogWarning("There is already an instance of GameInitializer in the scene");
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
