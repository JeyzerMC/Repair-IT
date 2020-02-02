using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public int timeLimit;

    private float startTime;

    private bool playing;

    TextMeshProUGUI textComp;

    private GameObject _endGame;
    
    private MoneyManager _moneyManager;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent<TextMeshProUGUI>(out textComp);
        _endGame = GameObject.Find("EndGameScreen");
        _moneyManager = FindObjectOfType<MoneyManager>();
        
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

        if (remainingTime == 0 && _endGame != null)
        {
            _endGame.SetActive(true);
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
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
