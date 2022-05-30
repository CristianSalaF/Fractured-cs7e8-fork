using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TranslationText : MonoBehaviour
{
    [TextArea(3, 14)]
    [SerializeField] private string _textES;
    
    [TextArea(3, 14)]
    [SerializeField] private string _textEN;

    [SerializeField] private Text _textBox;
    
    [SerializeField] private SetLanguage _setLanguage;

    private void Start()
    {
        _setLanguage.OnToggleLanguage += ToggleLang_OnToggleLanguageTrans;

        if (_setLanguage == null)
        {
            Debug.Log("SetLanguage in " + this.transform.name + " could not be found");
        }

        if (!_setLanguage.IsEng())
        {
            SetTextLanguage(_textBox,_textES);
        }
        else
        {
            SetTextLanguage(_textBox,_textEN);
        }
    }

    private void ToggleLang_OnToggleLanguageTrans(object sender, EventArgs e)
    {
        if (!_setLanguage.IsEng())
        {
            SetTextLanguage(_textBox, _textEN);
        }
        else
        {
            SetTextLanguage(_textBox, _textES);
        }
    }


    public void SetTextLanguage(Text textObj, string transText)
    {
        textObj.text = transText;
    }
}
