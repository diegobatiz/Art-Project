using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    private static List<string> _currentSceneGroup = new();
    private static int _currentGroup;
    private static string _currentPath;
    private static int _scenesEntered;
    private static int _minBossScene;
    private static int _bossScene;

    public static void ChangeSceneGroup(int group, string path, int miniBossScene = 0, int bossScene = 0)
    {
        if (_currentGroup == group)
        {
            return;
        }

        _currentGroup = group;
        _currentPath = path;
        _minBossScene = miniBossScene;
        _bossScene = bossScene;

        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string name = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            if (name.StartsWith(group.ToString()))
            {
                _currentSceneGroup.Add(name);
            }
        }
    }

    public static List<string> GetSceneList()
    {
        return _currentSceneGroup;
    }

    public static void LoadNextScene()
    {
        if (_scenesEntered + 1 == _minBossScene)
        {
            SceneManager.LoadSceneAsync(_currentPath + "Mini" + _currentGroup);
            return;
        }
        else if (_scenesEntered + 1 == _bossScene)
        {
            SceneManager.LoadSceneAsync(_currentPath + "Boss" + _currentGroup); ;
            return;
        }
        else
        {
            int sceneNum = Random.Range(0, _currentSceneGroup.Count);
            SceneManager.LoadSceneAsync(_currentPath + _currentSceneGroup[sceneNum]);
            _currentSceneGroup.Remove(_currentSceneGroup[sceneNum]);
        }

        _scenesEntered++;
        Debug.Log(_scenesEntered);
    }

    public static void ResetLoader()
    {
        _scenesEntered = 0;
        _currentSceneGroup.Clear();
    }
}
