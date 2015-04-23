using UnityEngine;
using System.Collections.Generic;

public class ThrowableSpellBehaviour : MonoBehaviour
{
    public int m_LayerMask = 1 << 0 | 1 << 8;
    public CasterBehaviour m_Caster;

    public Vector3 Velocity
    {
        get { return m_Velocity; }
        set { m_Velocity = value; }
    }

    public float m_ColliderRadius = 0.1f;

    public GameObject m_explosionEffect;

    protected Vector3 m_Velocity;

    protected void Explode (Vector3 position)
    {
        //самоуничтожаемся
        GameObject.DestroyObject(gameObject, 2.0f);
        enabled = false;

        if (m_explosionEffect != null)
        {
            var obj = GameObject.Instantiate(m_explosionEffect, position, transform.rotation) as GameObject;
            obj.SendMessage("Explode", position);

            GameObject.Destroy(obj, 5.0f);
        }
    }
}


public class FireballBehaviour : ThrowableSpellBehaviour
{
	void FixedUpdate ()
	{
        var ray = new Ray(transform.position, m_Velocity.normalized);
        float scalarVelocity = m_Velocity.magnitude * Time.fixedDeltaTime;

        transform.position = transform.position + m_Velocity * Time.fixedDeltaTime;

        //файрболл задел кого-то
        if ((scalarVelocity > 0 && Physics.SphereCast(ray, m_ColliderRadius, scalarVelocity, m_LayerMask)) ||
            (scalarVelocity == 0.0f && Physics.CheckSphere(ray.origin, m_ColliderRadius, m_LayerMask)))
        {
            Vector3 effectLocation = transform.position;
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, scalarVelocity * 2.0f))
                effectLocation = (hitInfo.point + hitInfo.normal * 0.1f);
            Explode(effectLocation);
        }
	}
}
