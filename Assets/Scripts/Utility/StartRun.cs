using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRun : MonoBehaviour
{
    private int result;
    private RandomSeed seed = new RandomSeed();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        seed.SetNewSeed();
        result = RandomSeed.Seed;
        Random.InitState(RandomSeed.Seed);
        Debug.Log(result);
    }
}
