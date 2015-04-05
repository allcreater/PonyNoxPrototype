using UnityEngine;
using System.Collections.Generic;

public class MagicArrowBehaviour : ThrowableSpellBehaviour
{
    public Transform m_Target;

    public float m_Speed;

    // Use this for initialization
    void Start()
    {
    }

    void FixedUpdate()
    {
        var dir = m_Target.position - transform.position;
        float acceleration = m_Speed * Time.fixedDeltaTime;

        m_Velocity += (dir.normalized * acceleration);
        var vel = m_Velocity.magnitude;
        if (vel > m_Speed)
            m_Velocity *= (m_Speed / vel);

        transform.position = transform.position + m_Velocity * Time.fixedDeltaTime;

        //файрболл задел кого-то
        var ray = new Ray(transform.position, m_Velocity.normalized);
        if (Physics.SphereCast(ray, m_ColliderRadius, acceleration, m_LayerMask))
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

            GameObject.DestroyObject(gameObject, 2.0f);
            enabled = false;

            if (m_explosionEffect != null)
            {
                Vector3 effectLocation = transform.position;

                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo, acceleration * 2.0f))
                    effectLocation += (hitInfo.normal * 0.5f);

                var obj = GameObject.Instantiate(m_explosionEffect, effectLocation, transform.rotation);
                GameObject.Destroy(obj, 5.0f);
            }
        }
    }
}
