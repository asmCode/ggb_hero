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
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        string sceneName = ShouldSkipIntro() ? "GameplayTest" : "IntroScene";
        SceneManager.LoadScene(sceneName);
    }

    private bool ShouldSkipIntro()
    {
        return PlayerPrefs.GetInt("skip_intro", 0) == 1;
    }
}
