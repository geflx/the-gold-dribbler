using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeManager : MonoBehaviour
{
    #region Singleton

	public static HordeManager instance;

	void Awake() {
		if(instance != null) {
			Debug.LogWarning ("More than one instance of Inventory found!");
			return;
		}
		instance = this;
	}

	#endregion

    public int horde;
    public bool hordeOn, intervalOn;

    void Start()
    {
        horde = 1;
        hordeOn = false;
        intervalOn = true;
    }

    void Update()
    {
        
    }
}
