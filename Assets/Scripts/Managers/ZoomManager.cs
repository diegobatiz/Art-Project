using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomManager : MonoBehaviour
{
    [SerializeField] private List<ZoomCheckpoint> _zoomCheckpoints;
    [SerializeField] private Transform _player;

    private ZoomCheckpoint _lastCheckpoint;
    private ZoomCheckpoint _currentCheckpoint;
    private ZoomCheckpoint _nextCheckpoint;
    private ZoomCheckpoint _compareCheckpoint;

    private float _t;

    private void Awake()
    {
        _lastCheckpoint = _zoomCheckpoints[0];
        _currentCheckpoint = _zoomCheckpoints[1];
        _nextCheckpoint = _zoomCheckpoints[2];
    }

    private void Update()
    {
        if (IsInRange(_nextCheckpoint.transform.position, _player.position))
        {
            _lastCheckpoint = _currentCheckpoint;
            Debug.Log($"{_lastCheckpoint.gameObject.name}");
            _currentCheckpoint = _nextCheckpoint;
            Debug.Log($"{_currentCheckpoint.gameObject.name}");

            for (int i = 0; i < _zoomCheckpoints.Count; i++)
            {
                if (_zoomCheckpoints[i] == _currentCheckpoint)
                {
                    if (_zoomCheckpoints[i + 1] != null)
                    {
                        _nextCheckpoint = _zoomCheckpoints[i + 1];
                        Debug.Log($"{_nextCheckpoint.gameObject.name}");
                    }
                    break;
                }
            }
        }
        else if (IsInRange(_lastCheckpoint.transform.position, _player.position))
        {
            _nextCheckpoint = _currentCheckpoint;
            Debug.Log($"{_nextCheckpoint.gameObject.name}");
            _currentCheckpoint = _lastCheckpoint;
            Debug.Log($"{_currentCheckpoint.gameObject.name}");

            for (int i = 0; i < _zoomCheckpoints.Count; i++)
            {
                if (_zoomCheckpoints[i] == _currentCheckpoint)
                {
                    if (_zoomCheckpoints[i - 1] != null)
                    {
                        _lastCheckpoint = _zoomCheckpoints[i - 1];
                        Debug.Log($"{_lastCheckpoint.gameObject.name}");
                    }
                    break;
                }
            }
        }
    }

    public float GetCameraZoom()
    {
        _compareCheckpoint = FindCloser(_nextCheckpoint, _lastCheckpoint);

        float totalDistance = Vector2.Distance(_compareCheckpoint.transform.position, _currentCheckpoint.transform.position);
        Debug.Log(totalDistance);
        float playerDistance = Vector2.Distance(_compareCheckpoint.transform.position, _player.position);
        Debug.Log(playerDistance);
        _t = Mathf.Clamp01(playerDistance/totalDistance);

        if (_compareCheckpoint == _nextCheckpoint)
        {
            return Mathf.Lerp(_compareCheckpoint.Zoom, _currentCheckpoint.Zoom, _t);
        }
        else
        {
            return Mathf.Lerp(_currentCheckpoint.Zoom, _compareCheckpoint.Zoom, _t);
        }
    }

    public float GetCameraOffset()
    {
        if (_compareCheckpoint == _nextCheckpoint)
        {
            return Mathf.Lerp(_compareCheckpoint.Offset, _currentCheckpoint.Offset, _t);
        }
        else
        {
            return Mathf.Lerp(_currentCheckpoint.Offset, _compareCheckpoint.Offset, _t);
        }
    }

    private bool IsInRange(Vector2 NextZoom, Vector2 PlayerPos)
    {
        return Vector2.Distance(NextZoom, PlayerPos) < 1.5f;
    }

    private ZoomCheckpoint FindCloser(ZoomCheckpoint next, ZoomCheckpoint last)
    {
        Vector2 midpointNext = Vector2.Lerp(next.transform.position, _currentCheckpoint.transform.position, 0.5f);
        float radiusNext = Vector2.Distance(midpointNext, next.transform.position);
        Vector2 midpointLast = Vector2.Lerp(last.transform.position, _currentCheckpoint.transform.position, 0.5f);
        float radiusLast = Vector2.Distance(midpointLast, last.transform.position);

        if (Vector2.Distance(midpointNext, _player.transform.position) < radiusNext)
        {
            return next;
        }
        else if (Vector2.Distance(midpointLast, _player.transform.position) < radiusLast)
        {
            return last;
        }

        return next;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (int i = 0; i < _zoomCheckpoints.Count; i++)
        {
            Gizmos.DrawWireSphere(_zoomCheckpoints[i].transform.position, 1.5f);
        }
    }
}
