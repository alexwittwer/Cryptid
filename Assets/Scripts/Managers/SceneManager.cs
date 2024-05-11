using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
    * This script is used to manage the scene transitions.
    * It is used to load new scenes and to animate the transition between scenes.
*/
public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;
    [SerializeField] private GameObject sceneTransition;
    private Animator anim;

    private void Awake()
    {
        anim = sceneTransition.GetComponentInChildren<Animator>();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Instance of Scene Manager already exists.");
        }
    }

    public void LoadNewScene(string sceneName)
    {
        StartCoroutine(loadSceneRoutine(sceneName));
    }

    IEnumerator loadSceneRoutine(string sceneName)
    {
        Debug.Log("Loading scene: " + sceneName);
        anim.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        anim.SetTrigger("End");
    }
}
