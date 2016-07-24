using UnityEngine;
using System.Collections;

public class MonoBehaviourSingleton<T> : MonoBehaviour
    where T : Component
{
    private static T m_instance;

    public static T GetInstance()
    {
        if (m_instance == null)
        {
            GameObject gameObject = new GameObject(typeof(T).Name);
            m_instance = gameObject.AddComponent<T>();
            DontDestroyOnLoad(gameObject);
        }

        return m_instance;
    }

    protected virtual void Awake()
    {
        if (m_instance != null)
        {
            Destroy(gameObject);
        }
    }
}
