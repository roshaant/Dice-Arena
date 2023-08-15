using System;
using UnityEngine;
using TMPro;

namespace UI
{
    /// <summary>
    /// Manages UI elements and updates for the game interface.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text CoinsText;          // Text element displaying player's coins
        [SerializeField] private TMP_Text StoreCoinsText;     // Text element displaying store's coins
        [SerializeField] private TMP_Text PlayerHealthText;   // Text element displaying player's health
        [SerializeField] private TMP_Text NotificationText;   // Text element displaying notifications
        [SerializeField] private GameObject NotificationPanel; // Panel for displaying notifications
        [SerializeField] private GameObject GameWonPanel; // Panel for displaying Game Win

        private void Start()
        {
            CoinsText.text = "0";         // Initialize coins text to 0
            StoreCoinsText.text = "0";    // Initialize store coins text to 0
        }

        public void UpdateCoins(int balance)
        {
            CoinsText.text = balance.ToString();      // Update player's coins text
            StoreCoinsText.text = balance.ToString(); // Update store's coins text
        }

        public void UpdatePlayerHealth(int health)
        {
            PlayerHealthText.text = health.ToString(); // Update player's health text
        }

        public void SetNotification(string message)
        {
            NotificationText.text = message;             // Set the notification message
            NotificationPanel.SetActive(true);           // Show the notification panel
        }

        public void DisplayGameWon()
        {
            GameWonPanel.SetActive(true);
        }
    }
}