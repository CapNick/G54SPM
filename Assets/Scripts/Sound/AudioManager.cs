using UnityEngine.Audio;
using System;
using UnityEngine;



public class AudioManager : MonoBehaviour {

	public Sound[] sounds;

	public static AudioManager instance;

	// Use this for initialization
	void Awake () {

		if (instance == null)
			instance = this;
		else {
			Destroy (gameObject);
			return;
		}

		DontDestroyOnLoad (gameObject);
		foreach (Sound s in sounds) 
		{
			s.source = gameObject.AddComponent<AudioSource> ();
			s.source.clip = s.clip;

			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
	}

	void Start ()
	{
		play("Background_Music");
	}

	public void play (string name)
	{
		Sound s = Array.Find (sounds, sound => sound.name == name);
		if (s == null)
			return;
		s.source.Play();
	}
	//public void UpdateVolume (float volume){
	//	foreach (Sound s in sounds) {
	//		s.volume = volume;
	//	}
	//}
}
	