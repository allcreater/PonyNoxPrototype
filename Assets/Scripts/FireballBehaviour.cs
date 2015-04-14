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
    public float m_ExplosionRadius = 5.0f;
    public float m_ExplosionBaseDamage = 10.0f;
    public float m_ShockwaveBaseImpulse = 100.0f;

    public GameObject m_explosionEffect;

    protected Vector3 m_Velocity;

    protected void Explode (Vector3 position)
    {
        //взрываемся
        var objects = Physics.OverlapSphere(transform.position, m_ExplosionRadius);
        foreach (var collider in objects)
        {
            /*
            if (collider.isTrigger)
                continue; //триггеры нематериальны, нас они не пока интересуют
            */
            var direction = collider.transform.position - transform.position;
            if (direction.magnitude == 0.0f) Debug.LogError("WTF?!");

            float influence = Mathf.Clamp(1.0f - direction.magnitude / m_ExplosionRadius, 0, 1);

            var rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                float impulse = m_ShockwaveBaseImpulse * influence;
                rb.AddExplosionForce(impulse, transform.position, m_ExplosionRadius);
            }

            var lc = collider.GetComponent<LivingCreatureBehaviour>();
            if (lc != null)
            {
                lc.m_HitPoints.ChangeValue(-m_ExplosionBaseDamage * influence);
            }
        }

        //самоуничтожаемся
        GameObject.DestroyObject(gameObject, 2.0f);
        enabled = false;

        if (m_explosionEffect != null)
        {
            var obj = GameObject.Instantiate(m_explosionEffect, position, transform.rotation);
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
                effectLocation += (hitInfo.normal * 0.5f);
            Explode(effectLocation);
        }
	}
}
