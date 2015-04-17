using UnityEngine;
using System.Collections.Generic;

public class PointDamageExplosion : MonoBehaviour
{
    public float m_ExplosionRadius = 0.1f;
    public float m_Damage = 4.0f;

    void Explode(Vector3 location)
    {
        //взрываемся
        var objects = Physics.OverlapSphere(transform.position, m_ExplosionRadius);
        foreach (var collider in objects)
        {
            var lc = collider.GetComponent<LivingCreatureBehaviour>();
            if (lc != null)
            {
                lc.m_HitPoints.ChangeValue(-m_Damage);
            }
        }
    }
}
