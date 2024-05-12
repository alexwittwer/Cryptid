using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        if (GlobalPlayerPosition.PlayerPositionLocation != Vector3.zero)
        {
            player.transform.position = GlobalPlayerPosition.PlayerPositionLocation;
        }
    }
}
