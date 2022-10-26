using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _gameoverText;
    
    [SerializeField] 
    private Transform _menuTransform;


    // Start is called before the first frame update
    void Start()
    {
        _gameoverText.gameObject.SetActive(false);
    }

    public void ToggleMenu()
    {
        if (_menuTransform.gameObject.activeSelf)
        {
            Disable(_menuTransform);
            Time.timeScale = 1;
            // Cursor.visible = false;
        }
        else
        {
            Enable(_menuTransform);
            Time.timeScale = 0;
            // Cursor.visible = true;
        }
    }

    private void Disable(Transform objTransform)
    {
        objTransform.gameObject.SetActive(false);
    }

    private void Enable(Transform objTransform)
    {
        objTransform.gameObject.SetActive(true);
    }

    public void ShowGameOver()
    {
        _gameoverText.gameObject.SetActive(true);
    }
}