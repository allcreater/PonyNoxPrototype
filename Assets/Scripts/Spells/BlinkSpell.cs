using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlinkSpell : SpellBehaviour
{
    public float m_CastDelay = 0.3f;
    public string m_AnimationTriggerName;
    public float m_MaxDistance = 30.0f;

    private bool m_inProgress = false;
    public override bool IsInProgress
    {
        get
        {
            return m_inProgress;
        }
    }

    public override void BeginCastImpl()
    {
        m_inProgress = true;

        CasterAnimator.SetTrigger(m_AnimationTriggerName);
        StartCoroutine(CastProcess(m_Caster.Target));
    }

    IEnumerator CastProcess(TargetInfo target)
    {
        
        yield return new WaitForSeconds(m_CastDelay);

        RaycastHit hitInfo;
        var direction = target.point - m_Caster.transform.position;

        if (target.normal.y > 0.7f && direction.magnitude <= m_MaxDistance)
            m_Caster.transform.position = target.point + target.normal;

        m_inProgress = false;
    }
}
