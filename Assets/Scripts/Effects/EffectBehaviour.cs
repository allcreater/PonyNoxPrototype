using UnityEngine;
using System.Collections.Generic;

//[UnityEngine.DisallowMultipleComponent]
public abstract class EffectBehaviour : MonoBehaviour
{
    public virtual float Power
    {
        get { return 1.0f; }
    }

    public Segment m_Timer;
    [HideInInspector]
    public GameObject m_Target;
    public string m_SpellID;

    protected T GetTargetsComponent<T>(bool testChildren = false)
    {
        if (testChildren)
            return m_Target.GetComponentInChildren<T>();
        
        return m_Target.GetComponent<T>();
    }

    private void Update()
    {
        m_Timer.currentValue += Time.deltaTime;
        if (m_Timer.Rate >= 1.0f)
            GameObject.Destroy(gameObject);

        EffectUpdate();
    }

    protected abstract void EffectUpdate();

    /*
    public static GameObject InstantiateEffect(GameObject reference, GameObject target)
    {
        var instance = GameObject.Instantiate(reference, target.transform.position, target.transform.rotation) as GameObject;
        instance.transform.SetParent(target.transform);

        foreach (var effect in instance.GetComponents<EffectBehaviour>())
            effect.m_Target = target;

        return instance;
    }
     */
}