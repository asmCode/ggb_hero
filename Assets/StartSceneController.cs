using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartSceneController : MonoBehaviour
{
    private void Start()
    {
        Ssg.Social.Social.GetInstance().Authenticate(null);
    }

    private void Update()
    {
        if (!Application.isShowingSplashScreen)
        {
            SceneManager.LoadScene("IntroScene");
        }
    }
}
