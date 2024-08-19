using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private string _sceneToLoad;
    [SerializeField] private int _sceneGroup;
    private const string path = "Scenes/Rooms/";

    [SerializeField] private bool RandomLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Play animation then load scene

        if (RandomLevel)
        {
            SceneLoader.ChangeSceneGroup(_sceneGroup, path, 3);
            SceneLoader.LoadNextScene();
            return;
        }

        if (SceneManager.GetSceneByName(path + _sceneToLoad) == null)
        {
            Debug.LogError($"Could not load scene {path + _sceneToLoad}");
        }

        SceneManager.LoadScene(path + _sceneToLoad);
    }
}
