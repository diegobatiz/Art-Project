using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBoss : MonoBehaviour
{
    [SerializeField] private MainLight _light;
    [Header("OBJECT MUST HAVE INTERFACE IBoss")]
    [SerializeField] private GameObject boss;
    private bool _isStart = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isStart)
        {
            return;
        }

        if (collision.gameObject.tag == "Player" && !_isStart)
        {
            _light.MakeLightScary();
            Player player = collision.GetComponent<Player>();
            IBoss theBoss = boss.GetComponent<IBoss>();
            theBoss.ActivateBoss(player);
            _isStart = true;
        }
    }
}
