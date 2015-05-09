using UnityEngine;
using System.Collections.Generic;

public class PointDamageExplosion : MonoBehaviour
{
    public float m_ExplosionRadius = 0.1f;
    public float m_Damage = 4.0f;
    public int m_LayerMask = 1 | 1 << 8;
    void Explode(Vector3 location)
    {
        Debug.Log("Бомбануло");
        //взрываемся
        var objects = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_LayerMask);
        Debug.Log(objects.Length);
        foreach (var collider in objects)
        {
            Debug.Log(collider.name);
            var lc = collider.GetComponent<LivingCreatureBehaviour>();
            if (lc != null)
            {
                lc.m_HitPoints.ChangeValue(-m_Damage);
            }
        }
    }
}
