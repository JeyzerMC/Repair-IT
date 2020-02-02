using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public int completionReward = 30;

    private int _moneyCount;

    private bool playing;

    TextMeshProUGUI textComp;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent<TextMeshProUGUI>(out textComp);
        _moneyCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (textComp != null)
        {
            textComp.text = $"{_moneyCount}";
        }
    }

    public int GetMoneyCount()
    {
        return _moneyCount;
    }

    public void ResetMoney()
    {
        _moneyCount = 0;
    }

    public void CompleteOrder()
    {
        _moneyCount += completionReward;
    }
}
