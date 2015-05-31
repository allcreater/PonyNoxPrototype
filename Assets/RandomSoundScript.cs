using UnityEngine;
using System.Collections;

public class RandomSoundScript : MonoBehaviour {
	public float m_MinInterval;
	public float m_MaxInterval;
	private AudioSource m_as;
	// Use this for initialization
	void Start () {
		m_as = GetComponent<AudioSource> ();
		StartCoroutine  (PlaySound());
	}
	
	// Update is called once per frame
	IEnumerator PlaySound () {
		m_as.Play ();
		yield return new WaitForSeconds (Random.Range (m_MinInterval, m_MaxInterval));
	}
}