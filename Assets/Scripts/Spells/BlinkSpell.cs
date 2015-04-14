using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlinkSpell : SpellBehaviour
{
    public Transform m_VisualEffectOrigin;
    public float m_CastDelay = 0.3f;

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
        if (!m_VisualEffectOrigin)
            m_VisualEffectOrigin = m_Caster.transform;

        m_inProgress = true;

        StartCoroutine(CastProcess(target));
    }

    IEnumerator CastProcess(Vector3 target)
    {
        GameObject visualEffect = GameObject.Instantiate(TemplateCollection.Instance.FindTemplate("CurePlayerEffect"));
        visualEffect.transform.SetParent(m_VisualEffectOrigin);
        visualEffect.transform.localPosition = TemplateCollection.Instance.FindTemplate("CurePlayerEffect").transform.localPosition;
        yield return new WaitForSeconds(m_CastDelay);
        GameObject.Destroy(visualEffect);

        m_Caster.transform.position = target;

        visualEffect = GameObject.Instantiate(TemplateCollection.Instance.FindTemplate("CurePlayerEffect"));
        visualEffect.transform.SetParent(m_VisualEffectOrigin);

        m_inProgress = false;
    }
}
