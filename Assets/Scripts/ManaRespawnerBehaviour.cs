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
        var attribute = AttributeBehaviour.GetAttributeComponent(other.gameObject, "ManaPoints");
        if (attribute != null)
        {
            attribute.m_Amount.ChangeValue(m_ManaPointsPerSecond * Time.deltaTime);

            var direction = (attribute.transform.position + Random.onUnitSphere * 0.4f) - m_ps.transform.position;
            var rb = other.GetComponent<Rigidbody>();
            if (rb)
                direction += rb.velocity * 0.4f;

            m_ps.Emit(m_ps.transform.localPosition, direction.normalized * 10.0f, 1.0f, 1.0f, new Color32(0, 0, 255, 128));
        }
    }
}
