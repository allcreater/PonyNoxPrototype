using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class NearestTargetSelector : MonoBehaviour
{
    public float m_UpdateInterval = 1.0f;
    public float m_VisionRadius = 20.0f;
    
    public float m_TargetForgiveRadius = 50.0f;
    public System.Func<SeekableTarget, bool> TargetFilter
    {
        get { return m_targetFilterCallback; }
        set { m_targetFilterCallback = value; Target = null; }
    }
    
    public IList<SeekableTarget> AllTargets { get; private set; }
    public SeekableTarget Target { get; private set; }
    public IEnumerable<SeekableTarget> this [string team]
    {
        get
        {
            return AllTargets.Where(x => x.m_Team == team);
        }
    }

    private float m_lastUpdateTime;
    private System.Func<SeekableTarget, bool> m_targetFilterCallback;

    void Start()
    {
        m_lastUpdateTime = Time.time;
        AllTargets = new List<SeekableTarget>();

        //TargetFilter = (x) => x.gameObject != gameObject;
    }

    void Update()
    {
        float sqrVisionRadius = m_VisionRadius * m_VisionRadius;
        float sqrTargetForgiveRadius = m_TargetForgiveRadius * m_TargetForgiveRadius;

        if (Time.time > m_lastUpdateTime + m_UpdateInterval)
        {
            var targets = GameObject.FindObjectsOfType<SeekableTarget>();
            AllTargets = (from x in targets where (transform.position - x.transform.position).sqrMagnitude < sqrVisionRadius orderby (transform.position - x.transform.position).sqrMagnitude select x).ToList();

            m_lastUpdateTime = Time.time;
        }

        if (!Target && TargetFilter != null)
            Target = AllTargets.Where(TargetFilter).FirstOrDefault();
        else if (Target)
        {
            var dir = transform.position - Target.transform.position;
            if (dir.sqrMagnitude > sqrTargetForgiveRadius)
                Target = null;
        }
    }
}
