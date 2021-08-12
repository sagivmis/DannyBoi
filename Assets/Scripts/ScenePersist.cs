using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{
    int startingSceneIndex; 

    private void Awake() {
        int numScenePersist = FindObjectsOfType<ScenePersist>().Length;
        if(numScenePersist>1) {
            gameObject.SetActive(false);
            Destroy(gameObject);
            }
        else {
            DontDestroyOnLoad(gameObject);
            }
    }


    void Start()
    {
        startingSceneIndex = SceneManager.GetActiveScene().buildIndex;

    }


    void Update()
    {   int currentScene = SceneManager.GetActiveScene().buildIndex;
        if( currentScene != startingSceneIndex) {
            Destroy(gameObject);
        }    
    }

    public void DestroySelf(){
        Destroy(gameObject);
    }
    
}
