using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LanguageManager : MonoBehaviour {    
    
    #region Singleton
	public static LanguageManager instance;
	void Awake() {
		if(instance != null) {
			Debug.LogWarning ("More than one instance of LanguageManager found!");
			return;
		}
		instance = this;
	}
	#endregion

	public UnityEvent m_updateLabels = new UnityEvent();

    public void SetLanguage(string language) {
        PlayerPrefs.SetString("language", language);
		
		LanguageTranslation.LoadLanguage();
		m_updateLabels.Invoke();
    }
}
