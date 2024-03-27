using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Jump Data")]
public class JumpData : ScriptableObject
{
    [Header("Customizable")]
    [SerializeField]
    private float _jumpHeight;
    [SerializeField]
    private float _jumpTime;

    public float FallFasterGravityFactor;
    public float EarlyExitGravityFactor;
    public float MaxJumpBufferTime;
    public float MaxCoyoteTime;

    [HideInInspector] public float GravityScaleFactor;
    [HideInInspector] public float JumpGravity;
    [HideInInspector] public float InitJumpSpeed;

    private void OnValidate()
    {
        JumpGravity = -2 * _jumpHeight / Mathf.Pow(_jumpTime, 2);
        GravityScaleFactor = JumpGravity / Physics2D.gravity.y;
        InitJumpSpeed = -JumpGravity * _jumpTime;
    }

}
