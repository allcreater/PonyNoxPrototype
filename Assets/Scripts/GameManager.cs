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
        {
            Debug.LogWarning(string.Format("GameManager have redudand instance in {0} and will be deleted", gameObject.name));
            GameObject.Destroy(gameObject);
        }
        GameObject.DontDestroyOnLoad(this);
	}

    public void LoadLevel(string levelName)
    {
        Application.LoadLevel(levelName);
    }
	
}
