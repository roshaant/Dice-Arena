using Managers;
using UnityEngine;

namespace Grid
{
    /// <summary>
    /// Represents a cell in a grid-based system that can be highlighted and interacted with.
    /// </summary>
    public class Cell : MonoBehaviour, ICell
    {
        private bool isHighlighted; // Flag indicating if the cell is highlighted
        public Effect CellEffect;
        
        /// <summary>
        /// Called when the cell is clicked.
        /// </summary>
        public void OnMouseDown()
        {
            // If the cell is not highlighted, return early
            if (!isHighlighted)
                return;
            
            // Move the player to this cell using the GameManager
            GameManager.Instance.MovePlayerToCell(this);
        }

        /// <summary>
        /// Highlights the cell using the provided material.
        /// </summary>
        /// <param name="mat">The material to use for highlighting.</param>
        public void Highlight(Material mat)
        {
            // Set the cell's material to the provided material
            gameObject.GetComponent<MeshRenderer>().material = mat;
            isHighlighted = true; // Set the highlighted flag to true
        }

        /// <summary>
        /// Removes the highlight from the cell using the provided material.
        /// </summary>
        /// <param name="mat">The material used for highlighting.</param>
        public void UnHighlight(Material mat)
        {
            // Set the cell's material back to the provided material
            gameObject.GetComponent<MeshRenderer>().material = mat;
            isHighlighted = false; // Set the highlighted flag to false
        }

        /// <summary>
        /// Placeholder for occupying the cell (not implemented).
        /// </summary>
        public void ModifyCellEffect(Effect effect)
        {
            CellEffect = effect;
        }
    }
}