using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class ManaRespawnerBehaviour : MonoBehaviour
{
    public float m_ManaPointsPerSecond = 1.0f;

    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        var caster = other.GetComponent<CasterBehaviour>();
        if (caster != null)
        {
            caster.m_ManaPoints.ChangeValue(m_ManaPointsPerSecond * Time.deltaTime);
        }
    }
}
