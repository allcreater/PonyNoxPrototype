using UnityEngine;
using System.Collections.Generic;

public class FireballLogic : MonoBehaviour
{
    public CasterBehaviour m_Caster;

    public Vector3 m_Velocity;

    public float m_ColliderRadius = 0.1f;
    public float m_ExplosionRadius = 5.0f;
    public float m_ExplosionBaseDamage = 10.0f;
    public float m_ShockwaveBaseImpulse = 100.0f;

    public GameObject m_explosionEffect;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	void FixedUpdate ()
	{
        transform.position = transform.position + m_Velocity * Time.fixedDeltaTime;

        //файрболл задел кого-то
        if (Physics.CheckSphere(transform.position, m_ColliderRadius, 1))
        {
            //взрываемся
            var objects = Physics.OverlapSphere(transform.position, m_ExplosionRadius);
            foreach (var collider in objects)
            {
                if (collider.isTrigger)
                    continue; //триггеры нематериальны, нас они не пока интересуют

                var direction = transform.position - collider.transform.position;
                float influence = Mathf.Clamp(1.0f - direction.magnitude/m_ExplosionRadius, 0, 1);

                var rb = collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    float impulse = m_ShockwaveBaseImpulse * influence;
                    rb.AddForce(direction.normalized * impulse, ForceMode.Impulse);
                }

                var lc = collider.GetComponent<LivingCreatureBehaviour>();
                if (lc != null)
                {
                    lc.m_HitPoints.ChangeValue(-m_ExplosionBaseDamage * influence);
                }
            }

            GameObject.DestroyObject(gameObject);

            if (m_explosionEffect != null)
            {
                var obj = GameObject.Instantiate(m_explosionEffect, transform.position, transform.rotation);
                GameObject.Destroy(obj, 10.0f);
            }
        }
	}
}
