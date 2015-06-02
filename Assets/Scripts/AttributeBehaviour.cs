using UnityEngine;
using System.Linq;
using System.Collections;

[System.Serializable]
public struct Segment
{
    public float currentValue;
    public float maximumValue;

    //отношение текущего значения к максимальному
    public float Rate
    {
        get { return currentValue / maximumValue; }
    }

    //Значение остаётся в диапазоне [0, maximumValue]
    public void ChangeValue(float value)
    {
        currentValue += value;
        currentValue = Mathf.Clamp(currentValue, 0.0f, maximumValue);
    }

    public override string ToString()
    {
        return string.Format("{0:0.0}/{1:0.0}", currentValue, maximumValue);
    }
}

public class AttributeBehaviour : MonoBehaviour
{
    public string m_AttributeName;
    public Segment m_Amount;
    public float m_RegenerationPerSecond;

	void Start ()
    {
	
	}
	
	void Update ()
    {
        m_Amount.ChangeValue(m_RegenerationPerSecond * Time.deltaTime);
	}

    public static AttributeBehaviour GetAttributeComponent (GameObject gameObject, string name)
    {
        var attribute = gameObject.GetComponentsInChildren<AttributeBehaviour>().FirstOrDefault(x => x.m_AttributeName == name);
        return attribute;
    }
}
