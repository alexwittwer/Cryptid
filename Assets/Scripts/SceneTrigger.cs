using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Vector2 playerPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.instance.LoadNewScene(sceneName);
        }
    }
}
