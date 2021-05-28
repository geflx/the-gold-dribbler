using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    #region Singleton
	public static CanvasManager instance;

	void Awake() {
		if(instance != null) {
			Debug.LogWarning ("More than one instance of Inventory found!");
			return;
		}
		instance = this;
	}
	#endregion

    public TMP_Text hordeLabel;

    public void ShowHorde ()
    {
        hordeLabel.text = "Horde " + HordeManager.instance.horde;
    }

    public void HideHorde ()
    {
        hordeLabel.text = "";
    }
}
