using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private List<Transform> _extents;
    [SerializeField] private float _smoothTime;
    [SerializeField] private float _zoom;

    private List<float> _extentPositions = new List<float>(4);
    private float _aspect;
    private float _cameraSize;
    private Vector3 _velocity;
    //N, S, E, W

    private void Awake()
    {
        Camera camera = Camera.main;
        _cameraSize = camera.orthographicSize;
        _aspect = camera.aspect;

        _extentPositions.Add(_extents[0].position.y - _cameraSize);
        _extentPositions.Add(_extents[1].position.y + _cameraSize);
        _extentPositions.Add(_extents[2].position.x - _cameraSize * _aspect);
        _extentPositions.Add(_extents[3].position.x + _cameraSize * _aspect);


        //On Scene Change Move camera to where player is entering room
    }

    private void LateUpdate()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        Vector3 newPos = new Vector3();
        newPos = _player.position;
        newPos.z = _zoom;

        if (newPos.y >= _extentPositions[0])
        {
            newPos.y = _extentPositions[0];
        }
        else if (newPos.y <= _extentPositions[1])
        {
            newPos.y = _extentPositions[1];
        }

        if (newPos.x >= _extentPositions[2])
        {
            newPos.x = _extentPositions[2];
        }
        else if (newPos.x <= _extentPositions[3])
        {
            newPos.x = _extentPositions[3];
        }

        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref _velocity, _smoothTime);
    }
}
