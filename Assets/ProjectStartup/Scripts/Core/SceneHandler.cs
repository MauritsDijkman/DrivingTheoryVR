using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] private AudioClip clickSoundClip;
    private AudioSource clickSoundSource;

    int maxSceneIndex = 3;

    private void Awake()
    {
        clickSoundSource = GetComponent<AudioSource>();
    }

    public void LoadScene(int newSceneIndex)
    {
        SceneManager.LoadScene(newSceneIndex);
    }

    /// <summary>
    /// Switch to the next scene. If there isn't any, go back to the main menu.
    /// </summary>
    public void NextScene()
    {
        int newIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (newIndex >= maxSceneIndex)
        {
            SceneManager.LoadScene(0);
        }
        else SceneManager.LoadScene(newIndex);
    }

    /// <summary>
    /// Switch to the previous scene. If there this is the first scene, then don't switch.
    /// </summary>
    public void PreviousScene()
    {
        int newIndex = SceneManager.GetActiveScene().buildIndex - 1;
        if (newIndex == 0)
            return;

        else SceneManager.LoadScene(newIndex);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlaySound()
    {
        clickSoundSource.PlayOneShot(clickSoundClip);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            NextScene();
    }
}
