using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton
	public static AudioManager instance;

	void Awake() {
		if(instance != null) {
			Debug.LogWarning ("More than one instance of Inventory found!");
			return;
		}
		instance = this;
	}
	#endregion

    public AudioSource audioSource;
    public List<AudioClip> audioClips;

    public void PlayGameplaySound()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }

    public void PlayGameOverSound()
    {
        audioSource.clip = audioClips[1];
        audioSource.Play();
    }
}
