using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private UIManager _uiManager;
    private PlayerController _player;
    
    //unused for now due to lack of time
    private bool _isGameOver = false;
    
    [SerializeField] private bool _isTutorial = false;
    [SerializeField] private Transform _tutorialTransform;
    
    private bool _isPaused = false;

    private void Start()
    {
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        // _player = GameObject.Find("Pino").GetComponent<PlayerController>();

        if (_uiManager == null) Debug.Log("UI Manager is NULL");
    }
    
    //Currently bugged, when calling _uiManager.ToggleMenu, it freezes the game.
    //This was imported from Unity 2020LTS and worked there just fine.
    //It might be because i'm using the new input system (unity package)?
    
    /*public void PauseGame()
    {
        Debug.Log("Pressing Esc!");
        _isPaused = !_isPaused;
        if (_isTutorial)
        {
            _tutorialTransform.gameObject.SetActive(!_isPaused);
        }
        _uiManager.ToggleMenu();
    }*/

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