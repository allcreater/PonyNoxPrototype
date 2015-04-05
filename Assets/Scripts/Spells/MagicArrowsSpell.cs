using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MagicArrowsSpell : SpellBehaviour
{
    public int m_NumberOfArrows = 1;
    public float m_ArrowSpeed = 20.0f;
    public GameObject m_prefab;

    private List<GameObject> m_arrowsList;

    public override bool IsInProgress
    {
        get
        {
            return false;
        }
    }

    private static Vector3 RndOnHemisphere()
    {
        var vec = Random.insideUnitSphere;
        if (vec.z < 0.0f)
            vec.z *= -1;

        return vec;
    }

    public override void BeginCastImpl(Vector3 target)
    {
        if (m_prefab.GetComponent<MagicArrowBehaviour>() == null)
            return;

        m_arrowsList = new List<GameObject>(m_NumberOfArrows);

        for (int i = 0; i < m_NumberOfArrows; ++i)
        {
            var velocity = RndOnHemisphere() * m_ArrowSpeed;//(target - origin).normalized * m_ArrowSpeed;
            var origin = m_Caster.transform.position + velocity * 0.05f;
            Debug.DrawLine(origin, origin + velocity, Color.red, 10.0f);


            var missileObject = GameObject.Instantiate(m_prefab, origin , m_Caster.transform.rotation) as GameObject;
            var missile = missileObject.GetComponent<MagicArrowBehaviour>();

            missile.m_Speed = m_ArrowSpeed;
            missile.Velocity = velocity;
            missile.m_Caster = m_Caster;
            missile.m_Target = FindTarget();
        }
    }

    private static bool IsObjectAlive(GameObject go)
    {
        var lcb = go.GetComponent<LivingCreatureBehaviour>();
        return lcb != null && lcb.IsAlive;
    }

    private Transform FindTarget()
    {
        var aliveObjects = GameObject.FindGameObjectsWithTag("Enemy").Where(IsObjectAlive);

        var target = aliveObjects.FirstOrDefault();
        if (target != null)
            return target.transform;
        return null;
    }
}
