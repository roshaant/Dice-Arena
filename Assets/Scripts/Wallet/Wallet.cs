using UnityEngine.Events;

namespace DA.Wallet
{
    /// <summary>
    /// Represents a wallet that stores an in-game balance.
    /// </summary>
    public struct Wallet
    {
        private int Balance; // Current balance in the wallet

        public Wallet(int balance)
        {
            Balance = balance; // Initialize the wallet with the specified balance
        }

        public int GetAvailableBalance()
        {
            return Balance; // Return the current available balance in the wallet
        }

        public void SetBalance(int balance)
        {
            Balance = balance; // Set the balance of the wallet to the specified value
        }

        public void AddBalance(int balance)
        {
            Balance += balance; // Add the specified amount to the wallet balance
        }

        public void SubtractBalance(int balance)
        {
            Balance -= balance; // Subtract the specified amount from the wallet balance

            if (Balance <= 0)
            {
                Balance = 0; // Ensure that the balance doesn't go below zero
            }
        }
    }
}