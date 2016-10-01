using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartSceneController : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene("IntroScene");
    }
}
