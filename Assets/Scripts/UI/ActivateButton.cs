using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateButton : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    public void ActivateObject()
    {
        _gameObject.SetActive(true);
    }
}
