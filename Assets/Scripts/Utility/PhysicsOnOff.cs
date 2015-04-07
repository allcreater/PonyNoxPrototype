using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;
//using System.Runtime.Serialization.Formatters.Binary;

public class PhysicsOnOff : MonoBehaviour
{
    public bool IsPhysicsActive
    {
        get
        {
            return m_isActive;
        }
        set
        {
            if (m_isActive == value)
                return;
            
            if (m_isActive)
                RemovePhysics();
            else
                RestorePhysics();

            m_isActive = value;
        }
    }

    private bool m_isActive = true;

    private SerializedData m_savedJoint;
    private SerializedData m_savedRigidbody;

    // Use this for initialization
    void Start()
    {
    }

    public static T GetOrCreateComponent<T>(GameObject obj) where T : Component
    {
        T result = obj.GetComponent<T>();
        if (result == null)
            result = obj.AddComponent<T>();

        return result;
    }
    public static Component GetOrCreateComponent(GameObject obj, Type type)
    {
        Component result = obj.GetComponent(type);
        if (result == null)
            result = obj.AddComponent(type);

        return result;
    }


    private void RestorePhysics()
    {
        if (m_savedRigidbody != null)
        {
            var newRigidBody = gameObject.AddComponent<Rigidbody>();
            m_savedRigidbody.DeserializeTo(newRigidBody);
        }

        if (m_savedJoint != null)
        {
            var newJoint = gameObject.AddComponent(m_savedJoint.Type);
            m_savedJoint.DeserializeTo(newJoint);
        }
    }

    private void RemovePhysics()
    {
        //BinaryFormatter bf = new BinaryFormatter();
        //bf.




        var joint = GetComponent<Joint>();
        if (joint != null)
        {
            m_savedJoint = new SerializedData(joint);

            Component.Destroy(joint);
        }

        var rigidBody = GetComponent<Rigidbody>();
        if (rigidBody != null)
        {
            m_savedRigidbody = new SerializedData(rigidBody);

            Component.Destroy(rigidBody);
        }
    }

    class SerializedData
    {
        public Type Type { get; private set; }
        public Dictionary<string, object> FieldsValue { get; private set; }
        public Dictionary<string, object> PropertiesValue { get; private set; }

        public SerializedData(object obj)
        {
            FieldsValue = new Dictionary<string, object>();
            PropertiesValue = new Dictionary<string, object>();
            Type = obj.GetType();

            foreach (var property in Type.GetProperties())
            {
                if (property.CanRead && property.CanWrite && property.GetCustomAttributes(typeof(ObsoleteAttribute), false).Length == 0)
                {
                    object value = property.GetValue(obj, null);

                    if (property.PropertyType.IsSubclassOf(typeof(Component)))
                        value = (value as Component).gameObject;
                    
                    PropertiesValue.Add(property.Name, value);
                }
            }

            foreach (var field in Type.GetFields())
            {
                FieldsValue.Add(field.Name, field.GetValue(obj));
            }
        }

        public void DeserializeTo(object target)
        {
            if (target.GetType() != Type)
                return;

            foreach (var property in Type.GetProperties())
            {
                if (PropertiesValue.ContainsKey(property.Name))
                {
                    object value = PropertiesValue[property.Name];

                    if (property.PropertyType.IsSubclassOf(typeof(Component)))
                        value = PhysicsOnOff.GetOrCreateComponent(PropertiesValue[property.Name] as GameObject, property.PropertyType);

                    property.SetValue(target, value, null);
                }
            }

            foreach (var field in Type.GetFields())
            {
                field.SetValue(target, FieldsValue[field.Name]);
            }
        }
    }
}
