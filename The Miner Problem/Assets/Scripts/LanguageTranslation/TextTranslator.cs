using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextTranslator : MonoBehaviour {
    
    public string sentenceKey;
    private TMP_Text label;

    void Start() {
        label = GetComponent<TMP_Text>();
        SetText();
        LanguageManager.instance.m_updateLabels.AddListener(SetText);
    }

    public void SetText() {
        label.text = LanguageTranslation.fields[sentenceKey]; // What if sentenceKey doesn't exists?
    }
}
