using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    public int timeLimit;

    private float startTime;

    private bool playing;

    TextMeshProUGUI textComp;

    private GameObject _endGame;
    
    private MoneyManager _moneyManager;

    private AudioSource mainMusic;
    public AudioClip fastClip;
    public AudioClip slowClip;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent<TextMeshProUGUI>(out textComp);
        _endGame = GameObject.Find("EndGameScreen");
        _moneyManager = FindObjectOfType<MoneyManager>();

        mainMusic = Camera.main.GetComponent<AudioSource>();

        ResetGame();
        UpdateTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
            UpdateTimer();
        else if (Input.GetButtonDown("j1_Fire1") || Input.GetButtonDown("j2_Fire1") || Input.GetButtonDown("key_Fire1"))
            SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
        else if (Input.GetButtonDown("j1_Fire2") || Input.GetButtonDown("j2_Fire2") || Input.GetButtonDown("key_Fire2"))
            QuitGame();
    }

    void UpdateTimer()
    {
        var remainingTime = (int) Mathf.Round(timeLimit - Time.time + startTime);

        if (remainingTime < 15 && mainMusic.clip != fastClip)
        {
            mainMusic.clip = fastClip;
            mainMusic.Play();
        }

        if (remainingTime == 0 && _endGame != null)
        {
            _endGame.SetActive(true);
            mainMusic.Stop();
            playing = false;
            Debug.Log("Endgame");
        }

        if (textComp != null)
            textComp.text = $"{remainingTime}";
    }

    public void ResetGame()
    {
        startTime = Time.time;
        playing = true;
        if (_endGame != null)
            _endGame.SetActive(false);

        if (_moneyManager != null)
            _moneyManager.ResetMoney();
        mainMusic.clip = slowClip;
        mainMusic.Play();
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
