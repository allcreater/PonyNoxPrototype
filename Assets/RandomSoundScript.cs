using UnityEngine;
using System.Collections;

public class RandomSoundScript : MonoBehaviour {
	public float m_MinInterval;
	public float m_MaxInterval;

	public AudioClip[] m_SoundsList;
	public bool m_ConsiderSoundLength;

	private AudioSource m_as;
	
	// Use this for initialization
	void Start ()
	{
		m_as = GetComponent<AudioSource> ();
		StartCoroutine  (PlaySound());
	}
	
	// Update is called once per frame
	IEnumerator PlaySound ()
	{
		while (true)
		{
			if (m_SoundsList != null && m_SoundsList.Length > 0)
			{
				var soundClip = m_SoundsList[Random.Range(0, m_SoundsList.Length)];
				m_as.PlayOneShot(soundClip);

				if (m_ConsiderSoundLength)
					yield return new WaitForSeconds (soundClip.length);
			}
			else
			{
				m_as.Play ();

				if (m_ConsiderSoundLength)
					yield return new WaitForSeconds (m_as.clip.length);
			}

            yield return new WaitForSeconds(Random.Range(m_MinInterval, m_MaxInterval));
		}
	}
}