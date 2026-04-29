using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    private UIManager _uiManager;

    public override void OnStartLocalPlayer()
    {
        _uiManager = FindFirstObjectByType<UIManager>();
        _uiManager.ShowUI();  // Ä«µå ½½·Ô¸¸ ÄÔ
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (other.TryGetComponent(out Card card))
        {
            CmdCollectCard(other.gameObject);
        }
    }

    [Command]
    private void CmdCollectCard(GameObject cardObj)
    {
        Card card = cardObj.GetComponent<Card>();

        if (card != null)
        {
            Grade grade = card.GetGrade();

            RpcOnCardCollected(grade);

            NetworkServer.Destroy(cardObj);
        }
    }

    [ClientRpc]
    private void RpcOnCardCollected(Grade grade)
    {
        if (!isLocalPlayer)
        {
            return;
        }

        _uiManager.OnCardCollected(grade);
    }
}