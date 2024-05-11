using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBoss : MonoBehaviour
{
    [Tooltip("NOTE: OBJECT MUST HAVE INTERFACE IBoss")]
    [SerializeField] private GameObject boss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            IBoss theBoss = boss.GetComponent<IBoss>();
            theBoss.ActivateBoss(player);
        }
    }
}
