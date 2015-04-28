using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MagicArrowsSpell : SpellBehaviour
{
    public int m_NumberOfArrows = 1;
    public float m_ArrowSpeed = 20.0f;
    public float m_ArrowAcceleration = 100.0f;
    public GameObject m_prefab;

    private List<MagicArrowBehaviour> m_arrowsList;

    public override bool IsInProgress
    {
        get
        {
            return (m_arrowsList != null) && m_arrowsList.Any(x => x);
        }
    }

    private static Vector3 RndOnHemisphere()
    {
        var vec = Random.insideUnitSphere;
        if (vec.y < 0.0f)
            vec.y *= -1;

        return vec;
    }

    public override void BeginCastImpl(Vector3 target)
    {
        if (m_prefab.GetComponent<MagicArrowBehaviour>() == null)
            return;

        m_arrowsList = new List<MagicArrowBehaviour>(m_NumberOfArrows);

        for (int i = 0; i < m_NumberOfArrows; ++i)
        {
            var velocity = RndOnHemisphere() * m_ArrowSpeed * 0.1f;//(target - origin).normalized * m_ArrowSpeed;
            var origin = m_Caster.transform.position + velocity * 0.2f;
            Debug.DrawLine(origin, origin + velocity, Color.red, 0.5f);


            var missileObject = GameObject.Instantiate(m_prefab, origin , m_Caster.transform.rotation) as GameObject;
            var missile = missileObject.GetComponent<MagicArrowBehaviour>();

            missile.m_TargetTeam = "Enemy"; //TODO
            missile.m_Speed = m_ArrowSpeed;
            missile.Velocity = velocity;
            missile.m_Caster = m_Caster;
            missile.m_Acceleration = m_ArrowAcceleration;

            m_arrowsList.Add(missile);
        }
    }

    private static bool IsObjectAlive(GameObject go)
    {
        var lcb = go.GetComponent<LivingCreatureBehaviour>();
        return lcb != null && lcb.IsAlive;
    }
}
