using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class ManaRespawnerBehaviour : MonoBehaviour
{
    public float m_ManaPointsPerSecond = 1.0f;

    private ParticleSystem m_ps;


    void Start()
    {
        m_ps = GetComponent<ParticleSystem>();
    }

    void OnTriggerStay(Collider other)
    {
        var caster = other.GetComponent<CasterBehaviour>();
        if (caster != null)
        {
            caster.m_ManaPoints.ChangeValue(m_ManaPointsPerSecond * Time.deltaTime);


            var direction = (caster.transform.position + Random.onUnitSphere*0.4f) - m_ps.transform.position;
            m_ps.Emit(m_ps.transform.localPosition, direction.normalized * 10.0f, 1.0f, 1.0f, new Color32(0, 0, 255, 128));
        }
    }
}
