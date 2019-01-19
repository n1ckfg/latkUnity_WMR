using UnityEngine;
using System.Collections;

public class SoundLooper : MonoBehaviour {

	public SteamVR_NewController steamController;
	public Transform target;
	public AudioSource audio;
	public float loopIn = 0f;
	public float loopOut = 0f;

	[HideInInspector] public bool trigger = false;

	private float intro = 0f;
	private float outro = 0f;

	void Awake() {
		if (audio == null) audio = GetComponent<AudioSource>();
	}

	void Start() {
		outro = audio.clip.length;
		if (loopIn <= intro || loopIn >= loopOut || loopIn >= outro) loopIn = outro * 0.25f;
		if (loopOut <= intro || loopOut <= loopIn || loopOut >= outro) loopOut = outro * 0.75f;
		// TODO: find zero crossings

		if (target != null) {
			transform.SetParent(target);
			transform.position = Vector3.zero;
		}
	}

	void Update() {
		trigger = steamController.triggerPressed;

		if (Input.GetKeyDown(KeyCode.Space)) {
			trigger = true;
		} else if (Input.GetKeyUp(KeyCode.Space)) {
			trigger = false;
		}

		if (trigger && !audio.isPlaying) {
			audio.time = intro;
			audio.Play();
		}

		if (trigger && audio.isPlaying && audio.time >= loopOut) {
			audio.time = loopIn;
		}

		if (!trigger && audio.isPlaying && audio.time >= outro) {
			audio.Stop();
		}
	}

}
