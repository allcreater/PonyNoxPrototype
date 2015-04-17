using UnityEngine;
using System.Collections.Generic;

public class NavMeshNavigator : MonoBehaviour
{
    public int m_AreaMask;

    public Vector3 Target
    {
        get
        {
            return m_actualTarget;
        }
        set
        {
            m_actualTarget = value;
        }
    }
    private Vector3 m_actualTarget;
    private NavMeshPath m_calculatedPath;
    private bool m_pathDirtyFlag;

    public void ForcePathRerouting()
    {
        m_pathDirtyFlag = true;
    }

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (m_pathDirtyFlag)
        {
            m_pathDirtyFlag = false;
            RecalculatePath();
        }
	}

    private void RecalculatePath()
    {
        m_calculatedPath = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, m_actualTarget, m_AreaMask, m_calculatedPath);
    }
}
