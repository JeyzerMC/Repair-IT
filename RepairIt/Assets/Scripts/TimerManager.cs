using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public int timeLimit;

    private float startTime;

    private bool playing;

    TextMeshProUGUI textComp;

    private GameObject endGame;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent<TextMeshProUGUI>(out textComp);
        endGame = GameObject.Find("EndGameScreen");
        
        ResetGame();
        UpdateTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
            UpdateTimer();
    }

    void UpdateTimer()
    {
        var remainingTime = (int) Mathf.Round(timeLimit - Time.time + startTime);

        if (remainingTime == 0 && endGame != null)
        {
            endGame.SetActive(true);
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
        if (endGame != null)
            endGame.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
