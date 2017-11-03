using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class StartSceneController : MonoBehaviour
{
    private void Start()
    {
        Ssg.Social.Social.GetInstance().Authenticate(null);
        InitLanguage();
    }

    private void Update()
    {
        if (SplashScreen.isFinished)
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
        return PlayerPrefs.GetInt("skip_intro", 0) == 1 || GameSettings.Censore;
    }

	public static void InitLanguage()
    {
        if (GameSettings.Censore)
            Localization.language = "EnglishIOS";
        else
            Localization.language = "English";
    }
}
