using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private UIManager _uiManager;
    private PlayerController _player;
    
    [SerializeField] private bool _isTutorial = false;
    [SerializeField] private Transform _tutorialTransform;
    
    private bool _isPaused = false;

    private void Start()
    {
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        if (_uiManager == null) Debug.Log("UI Manager is NULL");

        Cursor.lockState = CursorLockMode.Locked;
    }
    
    //This was imported from Unity 2020LTS.
    //For it to work I needed to add a "EventSystem" as a child to the "Canvas", then upgrade it so that it uses the new input (on the inspector).
    public void PauseGame()
    {
        //Debug.Log("Pressing Esc!");
        if (_isTutorial)
        {
            _tutorialTransform.gameObject.SetActive(_isPaused);
        }
        
        //makes the cursor invisible and if paused, unlocks it and shows it.
        //Lockstate.None should do it already but just in case forcing it.
        //When setting the lock mode to Locked, and then returning it to None, the cursor stops interacting with UI...
        
        _isPaused = !_isPaused;
        
        if (!_isPaused)
        {
            //Debug.Log("Game has been resumed");
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (_isPaused)
        {
            //Debug.Log("Game has been paused");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
        _uiManager.ToggleMenu();
    }

    public void SetGameOver()
    {
        StartCoroutine(RestartGame());
    }
    IEnumerator RestartGame()
    {
        _uiManager.ShowGameOver();
        yield return new WaitForSeconds(10f);

        SceneManager.LoadScene(0);
    }
}