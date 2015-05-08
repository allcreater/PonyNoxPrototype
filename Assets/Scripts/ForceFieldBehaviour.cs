using UnityEngine;
using System.Collections;

public class ForceFieldBehaviour : MonoBehaviour
{
    public float m_GrowTime = 0.5f;
    public float m_LifeTime = 1.0f;
    
    private float m_startTime;

	// Use this for initialization
	void Start ()
    {
        m_startTime = Time.time;
        transform.localScale = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float scale = Mathf.Min(Time.time - m_startTime, m_GrowTime) / m_GrowTime;
        transform.localScale = new Vector3(scale, scale, scale);

        if (Time.time > m_startTime + m_LifeTime)
            GameObject.Destroy(gameObject);
	}
}
