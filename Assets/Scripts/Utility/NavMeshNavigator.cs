using UnityEngine;
using System.Collections.Generic;

public class NavMeshNavigator : MonoBehaviour
{
    public int m_AreaMask = NavMesh.AllAreas;

    public float m_StopRadius;

    public Vector3 Target
    {
        get
        {
            return m_actualTarget;
        }
        set
        {
            ForcePathRerouting();
            m_actualTarget = value;
        }
    }

    public Vector3 DesiredDirection { get; private set; }

    public NavMeshPathStatus PathStatus
    {
        get { return m_calculatedPath.status; }
    }

    private Vector3 m_actualTarget;
    private NavMeshPath m_calculatedPath;
    private bool m_pathDirtyFlag;

    private int m_currentPointIndex = 0;

    void Start()
    {
        m_calculatedPath = new NavMeshPath();
    }

    public void ForcePathRerouting()
    {
        m_pathDirtyFlag = true;
    }
	
	// Update is called once per frame
	void Update ()
	{
        if (m_pathDirtyFlag)
        {
            m_pathDirtyFlag = false;
            RecalculatePath();
        }

        if (PathStatus != NavMeshPathStatus.PathInvalid && (m_currentPointIndex < m_calculatedPath.corners.Length))
        {
            var dir = m_calculatedPath.corners[m_currentPointIndex] - transform.position;
            DesiredDirection = Vector3.ClampMagnitude(dir, 1.0f);

            if (dir.magnitude <= m_StopRadius)
            {
				DesiredDirection = DesiredDirection * 0.1f;
                m_currentPointIndex++;
                Debug.Log(m_currentPointIndex);
            }
        }

        Debug.DrawLine(transform.position, transform.position + DesiredDirection, Color.cyan);

        for (int i = m_currentPointIndex; i < m_calculatedPath.corners.Length - 1; ++i)
            Debug.DrawLine(m_calculatedPath.corners[i], m_calculatedPath.corners[i + 1], Color.gray);
        
	}

    private void RecalculatePath()
    {
        DesiredDirection = Vector3.zero;

        m_calculatedPath = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, m_actualTarget, m_AreaMask, m_calculatedPath);
        
        m_currentPointIndex = 1;
    }
}
