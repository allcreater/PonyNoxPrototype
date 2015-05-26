using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EffectsReceiver : MonoBehaviour
{
	public string[] m_ResistsList; //TODO по-хорошему нужно уметь их ослаблять, а возможно и вообще вынести резисты в отдельную логику

    private Dictionary<string, EffectBehaviour> m_registeredEffects = new Dictionary<string,EffectBehaviour>();

	private HashSet<string> m_ResistsSet;

    public GameObject AddEffect (GameObject effectTemplate)
    {
        //var effectInstance = EffectBehaviour.InstantiateEffect(effectTemplate, gameObject);
        //return effectInstance;

        var instance = GameObject.Instantiate(effectTemplate, transform.position, transform.rotation) as GameObject;
        instance.transform.SetParent(transform);

        foreach (var effect in instance.GetComponents<EffectBehaviour>())
        {
			if (m_ResistsSet.Contains(effect.m_SpellID))
			{
				Debug.Log ("resist!");
				Destroy(effect.gameObject);
				continue;
			}



            EffectBehaviour existingEffect = null;
            if (m_registeredEffects.TryGetValue(effect.m_SpellID, out existingEffect) && existingEffect)
            {//усилим старый эффект, а новый можно и удалить
                Destroy(effect.gameObject);
            }
            else
            {
                effect.m_Target = gameObject;
                m_registeredEffects[effect.m_SpellID] = effect;
            }
        }

        return instance;
    }

	// Use this for initialization
	void Start ()
    {
		m_ResistsSet = new HashSet<string> (m_ResistsList);
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
