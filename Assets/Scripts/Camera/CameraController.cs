using Managers;
using UnityEngine;

namespace DA.Controller
{
    /// <summary>
    /// Controls the camera's behavior, following the player and managing zoom.
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        // Public variables accessible in the Inspector
        public Transform player;                // The player's transform to follow
        public float smoothSpeed = 0.125f;      // Speed of camera movement smoothing
        public float zoomSpeed = 5f;            // Speed of zooming in/out
        public float minZoom = 3f;              // Minimum allowed zoom level
        public float maxZoom = 10f;             // Maximum allowed zoom level
        public Vector3 offset;                  // Offset between camera and player

        private Vector3 desiredPosition;        // The desired position of the camera based on player's position

        private void Start()
        {
            // Get the player's transform from the GameManager
            player = GameManager.Instance.GetPlayer();

            // Calculate the initial offset between camera and player
            offset = transform.position - player.position;
        }

        private void LateUpdate()
        {
            // Calculate the desired position of the camera
            desiredPosition = player.position + offset;

            // Smoothly move the camera towards the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;

            // Get input for zooming and adjust the camera's orthographic size accordingly
            float zoomInput = Input.GetAxis("Mouse ScrollWheel");
            float newZoom = Camera.main.orthographicSize - zoomInput * zoomSpeed;
            newZoom = Mathf.Clamp(newZoom, minZoom, maxZoom);
            Camera.main.orthographicSize = newZoom;
        }
    }
}