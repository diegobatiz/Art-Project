using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    [SerializeField] private GameObject _objectToDeactivate;

    public void DeactivateObject()
    {
        _objectToDeactivate.SetActive(false);
    }
}
