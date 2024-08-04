using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _deathScreen;
    [SerializeField] private GameObject _continueButton;
    private EventSystem _eventSystem;

    private void Awake()
    {
        _eventSystem = EventSystem.current;
        _player.OnDead(ActivateDeathScreen);
    }

    private void ActivateDeathScreen()
    {
        _deathScreen.SetActive(true);
        _eventSystem.SetSelectedGameObject(_continueButton);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene("Scenes/Rooms/MainHub");
    }
}
