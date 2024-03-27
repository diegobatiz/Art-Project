using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask groundLayer;

    private float checkDistance = 0.2f;

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, checkDistance, groundLayer);
    }
}
