using Unity.VisualScripting;
using UnityEngine;

public class CustomerNPCMachine : MonoBehaviour
{
    [SerializeField] NPCStateMachine stateMachine;
    [SerializeField] float buyingDelay;
    [SerializeField] GameObject itemToBuy;
    [SerializeField] NPCDialogue dialogue;
    ShopItem shopItem;
    bool shopping;
    Tile targetTile;
    Tile tileWithItem;

    public void GoToBuyItem(GameObject item, Tile tile)
    {
        tileWithItem = tile;
        
        shopItem = item.GetComponent<ShopItem>();
        bool breakLoop = false;
        for(int x = -1; x < 2; x++)
        {
            for(int y = -1; y < 2; y++)
            {
                targetTile = GridManager.grid.GetTile(tile.x + x, tile.y + y);

                if(targetTile == null || targetTile.IsOccupied())
                    continue;

                if (Mathf.Abs(x) + Mathf.Abs(y) != 1)
                    continue;

                stateMachine.tileToGoTo = targetTile;
                stateMachine.allowRoaming = false;
                stateMachine.pathing.OnFinishedMovement += FinishedMovement;
                stateMachine.ChangeState(NPCStateMachine.NPCState.GoTo);

                breakLoop = true;
                break;
            }

            if(breakLoop)
                break;
        }
    }

    async void FinishedMovement()
    {
        stateMachine.pathing.OnFinishedMovement -= FinishedMovement;

        if(targetTile.centerPosition.x - gameObject.transform.position.x > .5 || targetTile.centerPosition.y - gameObject.transform.position.y > .5)
        {
            Debug.Log("Item blocked?");
            stateMachine.ChangeState(NPCStateMachine.NPCState.Idle);
            stateMachine.allowRoaming = true;
            shopping = false;
            return;
        }

        if(!tileWithItem.contents.Contains(itemToBuy))
        {
            Debug.Log("Item moved?");
            stateMachine.ChangeState(NPCStateMachine.NPCState.Idle);
            stateMachine.allowRoaming = true;
            shopping = false;
            return;
        }

        await Awaitable.WaitForSecondsAsync(buyingDelay);

        stateMachine.allowRoaming = true;
        shopping = false;
        shopItem.Buy();
        itemToBuy.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + .5f);
        itemToBuy.transform.parent = this.gameObject.transform;
        dialogue.fufilled = true;
    }

    void OnMouseDown()
    {
        if(shopping)
            return;

        GoToBuyItem(itemToBuy, GridManager.grid.GetTileWithWorldPosition(itemToBuy.transform.position));
        shopping = true;
    }
}
