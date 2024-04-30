using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyAI Data")]
public class EnemyData : ScriptableObject
{
    [Header("Customizable")]
    public float Range;
    public LayerMask LayerMask;
    public float FocusDuration;
}
