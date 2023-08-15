using Managers;
using UnityEngine;

namespace Store
{
    /// <summary>
    /// Manages purchases and interactions within the in-game store.
    /// </summary>
    public class StoreController : MonoBehaviour
    {
        /// <summary>
        /// Purchase health using in-game currency.
        /// </summary>
        public void HealthPurchase()
        {
            if (GameManager.Instance.GetAvailableBalance() >= 30)
            {
                // Heal the player and deduct the cost from the available balance
                GameManager.Instance.HealPlayer(10);
                GameManager.Instance.SubtractAvailableBalance(30);
                GameManager.Instance.SetNotification("Health Purchased!");
            }
            else
            {
                GameManager.Instance.SetNotification("Insufficient Funds!");
            }
        }

        /// <summary>
        /// Purchase move boast using in-game currency.
        /// </summary>
        public void MoveBoastPurchase()
        {
            if (GameManager.Instance.GetAvailableBalance() >= 60)
            {
                // Enable the move boast ability and deduct the cost from the available balance
                GameManager.Instance.EnableMoveBoast();
                GameManager.Instance.SubtractAvailableBalance(60);
                GameManager.Instance.SetNotification("Move Boast Purchased!");
            }
            else
            {
                GameManager.Instance.SetNotification("Insufficient Funds!");
            }
        }

        /// <summary>
        /// Purchase attack boast using in-game currency.
        /// </summary>
        public void AttackBoastPurchase()
        {
            if (GameManager.Instance.GetAvailableBalance() >= 30)
            {
                // Enable the attack boast ability and deduct the cost from the available balance
                GameManager.Instance.SubtractAvailableBalance(30);
                GameManager.Instance.SetNotification("Attack Boast Purchased!");
            }
            else
            {
                GameManager.Instance.SetNotification("Insufficient Funds!");
            }
        }
    }
}
