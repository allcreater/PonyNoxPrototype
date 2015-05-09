using UnityEngine;
using System.Collections;

public class ForceFieldSpell : SpellBehaviour
{
    public float m_SpellDuracity = 0.3f;
    public string m_AnimationTriggerName;
    public GameObject m_Prefab;

    private GameObject m_ForceFieldObjectInstance;

    public override bool IsInProgress
    {
        get
        {
            return m_ForceFieldObjectInstance;
        }
    }

    public override void BeginCastImpl(TargetInfo target)
    {
        CasterAnimator.SetTrigger(m_AnimationTriggerName);

        m_ForceFieldObjectInstance = GameObject.Instantiate(m_Prefab, m_Caster.transform.position, m_Caster.transform.rotation) as GameObject;
        m_ForceFieldObjectInstance.transform.SetParent(m_Caster.transform);
    }

}
