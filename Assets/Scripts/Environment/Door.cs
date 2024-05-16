using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Vector3 _startPoint;
    [SerializeField] private Vector3 _endPoint;
    [SerializeField] private float _duration;
    private float _pos = 0;

    private bool _closeDoor;
    private bool _openDoor;

    private void Awake()
    {
        _startPoint = transform.localPosition;
        Debug.Log(_startPoint);
    }

    private void Update()
    {
        if (_closeDoor && transform.position != _endPoint)
        {
            _pos += Time.deltaTime / _duration;
            transform.localPosition = Vector3.Lerp(_startPoint, _endPoint, _pos);
        }
        else if (_openDoor && transform.position != _startPoint)
        {
            _pos += Time.deltaTime / _duration;
            transform.localPosition = Vector3.Lerp(_endPoint, _startPoint, _pos);
        }
    }

    public void Open()
    {
        _pos = 0;
        _closeDoor = false;
        _openDoor = true;
    }

    public void Close()
    {
        _pos = 0;
        _closeDoor = true;
        _openDoor = false;
        Debug.Log("closing door");
    }
}
