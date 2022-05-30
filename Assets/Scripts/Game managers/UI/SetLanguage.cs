using System;
using UnityEngine;
using UnityEngine.UI;

public class SetLanguage : MonoBehaviour
{
    [SerializeField] private GameObject _tutParentObject;

    private bool _isEng;
    private string ppStrName = "language";

    public event EventHandler OnToggleLanguage;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs management with a int: 0 = Spanish, 1 = English
        if (PlayerPrefs.GetInt(ppStrName, 0) == 0)
        {
            _isEng = false;
        }
        else if (PlayerPrefs.GetInt(ppStrName, 0) == 1)
        {
            _isEng = true;
        }
        else Debug.Log("Error, PlayerPrefs for " + this.transform.name + " not found or unexpected value (not 1-Eng or 0-ESP)");

        
        OnToggleLanguage?.Invoke(this,EventArgs.Empty);
    }

    public void ToggleLang()
    {
        OnToggleLanguage?.Invoke(this,EventArgs.Empty);
        int valLang;

        SetEng();
        _tutParentObject.SetActive(false);

        //if spanish (!_isEng) set to 0 (spanish PPref value = 0), else it's english so set to 1
        if (!_isEng)
        {
            valLang = 0;

            PlayerPrefs.SetInt(ppStrName, valLang);
        }
        else
        {
            valLang = 1;

            PlayerPrefs.SetInt(ppStrName, valLang);
        }
        _tutParentObject.SetActive(true);
    }

    public bool IsEng()
    {
        return _isEng;
    }

    private void SetEng()
    {
        _isEng = !_isEng;
    }
}
