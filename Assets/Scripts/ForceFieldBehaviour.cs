using UnityEngine;
using System.Collections;

public class ForceFieldBehaviour : MonoBehaviour
{
    public float m_GrowTime = 0.5f;
    public float m_LifeTime = 1.0f;
    
    private float m_startTime;
    private float m_scale = 0.0f;
	// Use this for initialization
	void Start ()
    {
        m_startTime = Time.time;
        transform.localScale = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_scale = Mathf.Min(Time.time - m_startTime, m_GrowTime) / m_GrowTime;
        transform.localScale = new Vector3(m_scale, m_scale, m_scale);

        if (Time.time > m_startTime + m_LifeTime)
            GameObject.Destroy(gameObject);
	}

    void OnTriggerStay(Collider otherCollider)
    {
        var collider = GetComponent<Collider>();
        
        var rb = otherCollider.GetComponent<Rigidbody>();
        if (rb)
        {
            var direction = otherCollider.transform.position - transform.position;
            var magnitude = direction.magnitude;

            RaycastHit hitInfo;
            //collider
            Ray ray = new Ray(transform.position + direction*20.0f, -direction);
            if (magnitude >= 0.0f && collider.Raycast(ray, out hitInfo, 20.0f))
            {
                Debug.DrawLine(ray.origin, ray.origin + ray.direction*20.0f, Color.green, 0.1f);

                float vel = (Vector3.Dot(rb.velocity, hitInfo.normal) - 10.0f) * -1.1f;
                rb.AddForce(hitInfo.normal * vel, ForceMode.VelocityChange);
            }
        }
        
    }
}
