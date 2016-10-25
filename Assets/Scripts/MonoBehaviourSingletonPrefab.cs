//using UnityEngine;
//using System.Collections;

//public class MonoBehaviourSingletonPrefab<T, TMeta> : MonoBehaviour
//    where T : MonoBehaviour
//{
//    private static T m_instance;

//    public static T GetInstance()
//    {
//        if (m_instance == null)
//        {
//            var gameObject = new GameObject(typeof(T).Name);
//            m_instance = gameObject.AddComponent<T>();

//            var singletonInterface = m_instance as MonoBehaviourSingleton<T>;
//            //singletonInterface.m_createdManually = true;

//            DontDestroyOnLoad(gameObject);
//        }

//        return m_instance;
//    }

//    protected virtual void Awake()
//    {
//        if (m_instance != null)
//        {
//            Destroy(gameObject);
//        }
//    }
//}
