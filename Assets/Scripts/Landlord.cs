using UnityEngine;

public class Landlord : MonoBehaviour
{
    [SerializeField] NPCDialogue dialogue;

    public void Interact()
    {
        if(MoneyManger.instance.money >= 12)
        {
            MoneyManger.instance.AddMoney(-12);
            dialogue.fufilled = true;
        }
    }
}
