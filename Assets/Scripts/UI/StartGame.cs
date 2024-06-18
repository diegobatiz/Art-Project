using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private int _sceneToLoad;

    public void GameStart()
    {
        SceneManager.LoadScene(_sceneToLoad);
    }
}