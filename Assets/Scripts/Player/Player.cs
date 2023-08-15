using System;
using UnityEngine;
using UnityEngine.Events;

namespace DA.Player
{
    /// <summary>
    /// Represents the player character, handling movement and health.
    /// </summary>
    public class Player : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 5f; // Movement speed of the player
        [SerializeField] private int Health; // Player's health
        private Vector3 targetPosition; // Target position for movement
        public bool isMoving = false; // Flag indicating if the player is currently moving
        public event UnityAction onHealthChange;
        
        private void Update()
        {
            if (isMoving)
            {
                // Move the player towards the target position at a specific speed
                Vector3 newPosition = Vector3.MoveTowards(transform.position, new Vector3(targetPosition.x, 1, targetPosition.z), movementSpeed * Time.deltaTime);
                transform.position = newPosition;
            }

            // Check if the player is close enough to the target position to stop moving
            if (Vector3.Distance(transform.position, new Vector3(targetPosition.x, 1, targetPosition.z)) < 0.01f)
            {
                isMoving = false;
            }
        }

        private void Start()
        {
            Health = 100; // Initialize player's health
        }

        public void MoveToCell(Vector3 targetNode)
        {
            targetPosition = targetNode;
            isMoving = true; // Start moving towards the target position
        }

        public void Heal(int amount)
        {
            Health += amount; // Increase player's health by the specified amount
            onHealthChange?.Invoke();
        }

        public void Damage(int amount)
        {
            Health -= amount;
            if (Health < 0)
                Health = 0;
            onHealthChange?.Invoke();
        }
        public int GetPlayerHealth()
        {
            return Health; // Return the player's current health
        }
    }
}