using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class LivingCreatureBehaviour : MonoBehaviour
{
	public Segment m_HitPoints;
	public bool m_ReceiveFallDamage;

	private Rigidbody m_rigidBody;
	private Vector3 m_lastVelocity;
    public bool IsAlive
    {
        get { return m_HitPoints.currentValue > 0.0f; }
    }

    public Animator m_Animator;

	void Start ()
	{
		m_rigidBody = GetComponent<Rigidbody>();
	}

	void Update ()
	{
        if (m_Animator != null)
        {
            m_Animator.SetBool("IsDead", !IsAlive);
        }
	}

	void FixedUpdate()
	{
		if (m_ReceiveFallDamage)
		{
			Vector3 acceleration = (m_rigidBody.velocity - m_lastVelocity)/Time.fixedDeltaTime;
			
			float damage = Mathf.Max(acceleration.sqrMagnitude * m_rigidBody.mass  * 0.001f - 10000.0f, 0.0f) * 0.001f;
			if (damage > 0.0f)
			{
				Debug.Log ("Fall damage is " + damage);
				m_HitPoints.ChangeValue(-damage);
			}
		}
		m_lastVelocity = m_rigidBody.velocity;
	}
}