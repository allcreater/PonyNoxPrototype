using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwordAttack : SpellBehaviour
{
    public float m_Damage = 1.0f;

    public float m_MaxDuracity = 1.0f;

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

        StartCoroutine(CastProcess());
    }

    private IEnumerator CastProcess()
    {
        for (float maxTime = Time.time + m_MaxDuracity; Time.time < maxTime; )
        {
            if (TestHit())
                break;
            else
                yield return new WaitForFixedUpdate();
        }
        EndCastProcess();
    }
    
    private bool TestHit()
    {
        var ray = new Ray(m_Caster.transform.position + Vector3.up*0.6f, m_Caster.transform.TransformVector(Vector3.forward));
        Debug.DrawRay(ray.origin, ray.direction, Color.red);

        var hitsArray = Physics.SphereCastAll(ray, 1.0f, 1.0f, 1);
        foreach (var hit in hitsArray)
        {
            var lcb = hit.collider.GetComponent<LivingCreatureBehaviour>();
            if (lcb != null && hit.collider.gameObject != m_Caster)
            {
                lcb.m_HitPoints.ChangeValue(-m_Damage);

                if (hit.rigidbody != null)
                    hit.rigidbody.AddForce(ray.direction * 1000.0f, ForceMode.Impulse);

                return true;
            }
        }

        return false;
    }

    private void EndCastProcess()
    {
        m_inProgress = false;
    }
}
