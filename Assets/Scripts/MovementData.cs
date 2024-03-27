using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Movement Data")]
public class MovementData : ScriptableObject
{
    public float MaxSpeed;
    public float Acceleration;
    [HideInInspector] public float AccelAmount;
    public float Deceleration;
    [HideInInspector] public float DecelAmount;

    private void OnValidate()
    {
        AccelAmount = (50 * Acceleration) / MaxSpeed;
        DecelAmount = (50 * Deceleration) / MaxSpeed;
    }
}
