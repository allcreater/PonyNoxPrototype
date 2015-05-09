using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

	void Awake ()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            GameObject.Destroy(gameObject);

        GameObject.DontDestroyOnLoad(this);
	}
	
}
