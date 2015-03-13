using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TemplateCollection : MonoBehaviour
{
    public GameObject[] m_Templates;

    public static TemplateCollection Instance
    {
        get { return m_instance; }
    }

    public GameObject FindTemplate (string name)
    {
        return m_Templates.FirstOrDefault(item => item.name == name);
    }


    private static TemplateCollection m_instance;
    void Awake()
    {
        m_instance = this;
    }
}
