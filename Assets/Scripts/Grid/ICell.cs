using UnityEngine;

namespace Grid
{
    /// <summary>
    /// Interface for representing a cell in a grid-based system.
    /// </summary>
    public interface ICell
    {
        /// <summary>
        /// Highlights the cell with the specified material.
        /// </summary>
        /// <param name="mat">The material to use for highlighting.</param>
        void Highlight(Material mat);

        /// <summary>
        /// Removes the highlight from the cell using the specified material.
        /// </summary>
        /// <param name="mat">The material used for highlighting.</param>
        void UnHighlight(Material mat);

        /// <summary>
        /// Occupies the cell, indicating that it is now in use.
        /// </summary>
        void ModifyCellEffect(Effect effect);
    }
}