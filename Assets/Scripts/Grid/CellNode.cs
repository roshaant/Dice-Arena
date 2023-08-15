using UnityEngine;
using System.Collections.Generic;

namespace Grid
{
    /// <summary>
    /// Represents a node in a grid-based system that contains information about its position, connections, and indices.
    /// </summary>
    public struct CellNode
    {
        // Row index of the cell in the grid.
        public int Row;

        // Column index of the cell in the grid.
        public int Col;

        // Position of the cell node in the world.
        public Vector3 Position;

        // List of connected cell nodes.
        public List<CellNode> ConnectedCellNodes;
       
        /// <summary>
        /// Constructor for creating a CellNode.
        /// </summary>
        /// <param name="row">Row index of the cell.</param>
        /// <param name="col">Column index of the cell.</param>
        /// <param name="position">Position of the cell node in the world.</param>
        public CellNode(int row, int col, Vector3 position)
        {
            Row = row;
            Col = col;
            Position = position;
            ConnectedCellNodes = new List<CellNode>();
        }
    }

    public enum Effect
    {
        Default = 0,
        Win = 1,
        Damage = 2,
        MAX
    }
}