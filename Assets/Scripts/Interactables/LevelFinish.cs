using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinish : MonoBehaviour
{
    //scene 0 is the tutorial scene
    [SerializeField] private int _indexSceneToLoad = 1;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (_gameManager == null) Debug.Log("Game Manager is NULL");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController pc = other.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.enabled = false;

                pc.PlayGongSFX();
                
                if (_indexSceneToLoad < 0)
                {
                    _gameManager.SetGameOver();
                }
                else StartCoroutine(LoadNext());
            }
        }
    }

    IEnumerator LoadNext()
    {
        yield return new WaitForSeconds(3f);
        
        SceneManager.LoadScene(_indexSceneToLoad);
    }
}