using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Dice
{
    /// <summary>
    /// Controls the behavior of rolling a dice and displaying the result.
    /// </summary>
    public class DiceRoller : MonoBehaviour
    {
        // Public variables accessible in the Inspector
        public Sprite[] DiceFaces;      // Array of dice face sprites
        public Image DiceImage;         // UI Image component to display dice face
        public float RollDuration = 0.5f; // Duration of dice rolling animation

        // Private variables for dice rolling logic
        private bool IsRolling = false;  // Flag indicating if the dice is currently rolling
        private int TargetFace;          // The target face index after rolling
        private float StartTime;         // Time when the rolling started

        private void Update()
        {
            if (IsRolling)
            {
                // Calculate the progress of the rolling animation
                float progress = (Time.time - StartTime) / RollDuration;

                if (progress >= 1.0f)
                {
                    // Rolling animation is complete
                    IsRolling = false;
                    DiceImage.sprite = DiceFaces[TargetFace];
                    // Add the rolled face value to the player's balance
                    GameManager.Instance.AddBalance(TargetFace + 1);
                }
                else
                {
                    // Animation still in progress
                    float randomRotation = Mathf.Lerp(0.0f, 360.0f, progress);
                    // Randomly change the displayed face during animation
                    DiceImage.sprite = DiceFaces[Random.Range(0, DiceFaces.Length)];
                    // Apply a random rotation to simulate rolling
                    DiceImage.transform.rotation = Quaternion.Euler(0.0f, 0.0f, randomRotation);
                }
            }
        }

        /// <summary>
        /// Initiates the dice rolling process.
        /// </summary>
        public void RollDice()
        {
            // Reset any previous player selection
            GameManager.Instance.ResetSelection();

            if (!IsRolling)
            {
                // Choose a random target face for the roll
                TargetFace = Random.Range(0, DiceFaces.Length);
                // Record the start time of the rolling animation
                StartTime = Time.time;
                // Set the rolling flag to start the animation
                IsRolling = true;
            }
        }
    }
}
