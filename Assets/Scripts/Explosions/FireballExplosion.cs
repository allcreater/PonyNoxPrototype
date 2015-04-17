using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireballExplosion : MonoBehaviour
{
    public float m_ExplosionRadius = 5.0f;
    public float m_ExplosionBaseDamage = 10.0f;
    public float m_ShockwaveBaseImpulse = 100.0f;

    IEnumerator Explode(Vector3 location)
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

            /*
            var rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                float impulse = m_ShockwaveBaseImpulse * influence;
                rb.AddExplosionForce(impulse, transform.position, m_ExplosionRadius);
            }
            */

            var lc = collider.GetComponent<LivingCreatureBehaviour>();
            if (lc != null)
            {
                lc.m_HitPoints.ChangeValue(-m_ExplosionBaseDamage * influence);
            }
        }

        yield return new WaitForSeconds(0.1f);

        objects = Physics.OverlapSphere(transform.position, m_ExplosionRadius);
        foreach (var collider in objects)
        {
            var direction = collider.transform.position - transform.position;
            float influence = Mathf.Clamp(1.0f - direction.magnitude / m_ExplosionRadius, 0, 1);
            var rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                float impulse = m_ShockwaveBaseImpulse * influence;
                rb.AddExplosionForce(impulse, transform.position, m_ExplosionRadius);
            }
        }
    }
}
