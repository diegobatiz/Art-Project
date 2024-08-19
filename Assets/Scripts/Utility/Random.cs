using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSeed
{
    public static int Seed { get; private set; }

    public void SetNewSeed()
    {
        Seed = Environment.TickCount & Int32.MaxValue;
    }
}
