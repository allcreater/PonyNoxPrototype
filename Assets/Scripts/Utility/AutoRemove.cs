using UnityEngine;
using System.Collections.Generic;

public class AutoRemove : MonoBehaviour
{
    public float m_RemoveDelay = 100.0f;
	void Start ()
	{
        GameObject.Destroy(gameObject, m_RemoveDelay);
	}
	
}
