using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay = 2f;
    [SerializeField] private float levelExitSlowMoForce = 0.2f;

    private void OnTriggerEnter2D(Collider2D other) {
        StartCoroutine(LoadNextLevel());
    }


IEnumerator LoadNextLevel()
    {
        Time.timeScale = levelExitSlowMoForce;
        // PlayerMovement.Win();

        FindObjectOfType<ScenePersist>().DestroySelf();
        FindObjectOfType<GameSession>().refreshButtons();
        
        yield return new WaitForSeconds(levelLoadDelay);
        
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex+1);
        Time.timeScale = 1;
        
    }
}