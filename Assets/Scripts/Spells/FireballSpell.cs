using UnityEngine;
using System.Collections.Generic;

public class FireballSpell : SpellBehaviour
{
    public float m_FlySpeed = 20.0f;
    public GameObject m_prefab;

    public override bool IsInProgress
    {
        get
        {
            return false;
        }
    }

    public override void BeginCastImpl()
    {
        if (m_prefab.GetComponent<ThrowableSpellBehaviour>() == null)
            return;

        var origin = m_Caster.transform.position + m_Caster.transform.TransformDirection(new Vector3(0.0f, 1.0f, 1.0f));

        Debug.DrawLine(origin, m_Caster.Target.point, Color.red, 10.0f);

        var fireballObject = GameObject.Instantiate(m_prefab, origin, m_Caster.transform.rotation) as GameObject;
        var fireball = fireballObject.GetComponent<ThrowableSpellBehaviour>();

        fireball.Velocity = (m_Caster.Target.point - origin).normalized * m_FlySpeed;
        fireball.m_Caster = m_Caster;
    }
}
