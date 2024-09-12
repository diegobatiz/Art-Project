using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrine : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject message;
    [SerializeField] private GameObject upgradeMenu;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        message.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        message.SetActive(false);
    }

    public void Interact()
    {
        upgradeMenu.SetActive(true);
    }
}
