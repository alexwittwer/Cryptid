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

        player.transform.position = GlobalPlayerPosition.PlayerPositionLocation;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
