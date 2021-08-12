using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void StartFirstLevel(){
        FindObjectOfType<GameSession>().refreshButtons();
        SceneManager.LoadScene(1);
    }
     public void LoadMainMenu(){
        FindObjectOfType<GameSession>().refreshButtons();
        SceneManager.LoadScene(0);
    }
}
