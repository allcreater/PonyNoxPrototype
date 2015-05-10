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

	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}

    public static AttributeBehaviour GetAttributeComponent (GameObject gameObject, string name)
    {
        var attribute = gameObject.GetComponents<AttributeBehaviour>().FirstOrDefault(x => x.m_AttributeName == name);
        return attribute;
    }
}
