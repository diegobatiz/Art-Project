using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    void Damage(float amt);

    void Death();
}
