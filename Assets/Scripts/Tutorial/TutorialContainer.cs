using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialContainer : MonoBehaviour
{
    [TextArea(10, 14)] [SerializeField] 
    private String _tutorialText;

    [TextArea(10, 14)] [SerializeField] 
    private String _tutorialTextEN;

    [SerializeField] 
    private SetLanguage _setLanguage;

    [SerializeField] 
    private Text _tutText;

    [SerializeField] 
    private Image _tutImage;

    private void Start()
    {
        if (_setLanguage == null)
        {
            Debug.Log("SetLanguage in " + this.transform.name + " could not be found");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _tutText.gameObject.SetActive(true);
            _tutImage.gameObject.SetActive(true);

            if (!_setLanguage.IsEng()) _tutText.text = _tutorialText;
            else _tutText.text = _tutorialTextEN;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _tutText.gameObject.SetActive(false);
            _tutImage.gameObject.SetActive(false);
            _tutText.text = "";
        }
    }
}
