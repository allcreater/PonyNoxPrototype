using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PickableItem))]
public class WeaponBehaviour : MonoBehaviour
{
    private PickableItem m_item;
    public PickableItem Item
    {
        get
        {
            if (m_item == null)
            {
                m_item = GetComponent<PickableItem>();
                if (m_item == null) Debug.LogWarning("missing item component");
            }
            return m_item;
        }
        protected set
        {
            m_item = value;
        }
    }

    public string AnimationTrigger;

    protected virtual void FireImpl() { }

    public void Fire(GameObject owner)
    {
        var animator = owner.GetComponentInChildren<Animator>();
        animator.SetTrigger(AnimationTrigger);

        FireImpl();
    }
}

public class MeleeWeapon : WeaponBehaviour
{
    public float m_Damage = 1.0f;
    public float m_AttackTime = 0.3f;

    private Collider m_collider;
	void Start ()
	{
        m_collider = GetComponent<Collider>();
	}

    IEnumerator FireUpdate()
    {
        for (float startTime = Time.time; Time.time < startTime + m_AttackTime; )
        {
            if (TestHit())
            {
                break;
            }
            else
                yield return new WaitForEndOfFrame();
        }
    }

    bool TestHit()
    {
        var ray = new Ray(transform.position, Vector3.down);
        
        RaycastHit hitInfo;
        return m_collider.Raycast(ray, out hitInfo, 1.0f);
    }

    protected override void FireImpl()
    {
        StartCoroutine(FireUpdate());
    }
}
