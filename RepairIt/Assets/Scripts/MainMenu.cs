using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("j1_Fire1") || Input.GetButtonDown("j2_Fire1") || Input.GetButtonDown("key_Fire1"))
            PlayButton();
        else if (Input.GetButtonDown("j1_Fire2") || Input.GetButtonDown("j2_Fire2") || Input.GetButtonDown("key_Fire2"))
            QuitButton();
    }

    // Start is called before the first frame update
    public void PlayButton()
    {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
