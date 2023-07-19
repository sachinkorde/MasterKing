using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoPlayer : MonoBehaviour
{
    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        PlayMovie();
    }

    void PlayMovie()
    {
        Handheld.PlayFullScreenMovie("SplashVideo.mp4", Color.black, FullScreenMovieControlMode.Hidden, FullScreenMovieScalingMode.AspectFit);
        Invoke(nameof(LogInScene), 0.25f);
    }

    void LogInScene()
    {
        SceneManager.LoadScene("Login");
    }
}
