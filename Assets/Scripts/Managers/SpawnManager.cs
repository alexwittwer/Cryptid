using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject player;
    public GlobalPlayerPosition GlobalPlayerPosition;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        if (GlobalPlayerPosition.PlayerPosition != Vector3.zero)
        {
            player.transform.position = GlobalPlayerPosition.PlayerPosition;
        }
    }
}
