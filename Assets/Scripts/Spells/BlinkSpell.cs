using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlinkSpell : SpellBehaviour
{
    public float m_CastDelay = 0.3f;
    public string m_AnimationTriggerName;

    private bool m_inProgress = false;
    public override bool IsInProgress
    {
        get
        {
            return m_inProgress;
        }
    }

    public override void BeginCastImpl(Vector3 target)
    {
        m_inProgress = true;

        CasterAnimator.SetTrigger(m_AnimationTriggerName);
        StartCoroutine(CastProcess(target));
    }

    IEnumerator CastProcess(Vector3 target)
    {
        
        yield return new WaitForSeconds(m_CastDelay);

        m_Caster.transform.position = target;

        m_inProgress = false;
    }
}
