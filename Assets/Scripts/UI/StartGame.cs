using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void GameStart()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int sceneToLoad = currentScene.buildIndex + 1;
        SceneManager.LoadScene(sceneToLoad);
    }
}