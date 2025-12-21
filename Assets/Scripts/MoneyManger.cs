using UnityEngine;
using TMPro;

public class MoneyManger : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyDisplay;
    [SerializeField] int startingMoney;
    public static MoneyManger instance;
    public int money{get; private set;}
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        AddMoney(startingMoney);
    }

    public void AddMoney(int newMoney)
    {
        money += newMoney;
        UpdateDisplay();
    }
    
    void UpdateDisplay()
    {
        moneyDisplay.text = $"{money}";
    }
}
