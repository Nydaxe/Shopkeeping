using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [SerializeField] Placeable placeable;
    [SerializeField] int price;

    public void Buy()
    {
        MoneyManger.instance.AddMoney(price);
        placeable.Remove();
    }
}
