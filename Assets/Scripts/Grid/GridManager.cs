using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Grid
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private GameObject CellPrefab;
        [SerializeField] private GameObject IndicatorPrefab;

        [SerializeField] private int ROWS;
        [SerializeField] private int COLUMNS;
        [SerializeField] private int DAMAGE_NODES;

        [SerializeField] private Material DefaultMat;
        [SerializeField] private Material HighlightedMat;
        [SerializeField] private Material DamageIndicatorMat;
        [SerializeField] private Material WinIndicatorMat;

        private List<Cell> SelectedCells = new List<Cell>();
        private Dictionary<Vector2Int, Cell> CachedCellsData = new Dictionary<Vector2Int, Cell>();
        private Dictionary<Cell, CellNode> CachedCellNodes = new Dictionary<Cell, CellNode>();
        private bool MovementBoast;
        private CellNode[,] Grid;
        
        public event UnityAction GridAvailable;

        private void Start()
        {
            GenerateGrid();
            MovementBoast = false;
        }

        void GenerateGrid()
        {
            Grid = new CellNode[ROWS, COLUMNS];

            for (int row = 0; row < ROWS; row++)
            {
                for (int col = 0; col < COLUMNS; col++)
                {
                    Vector3 cellPosition = new Vector3(row, 0, col);
                    GameObject cellGO = Instantiate(CellPrefab, cellPosition, Quaternion.identity);
                    Cell cellData = cellGO.AddComponent<Cell>();
                    CellNode node = new CellNode(row, col, cellPosition);
                    Grid[row, col] = node;
                    CachedCellsData[new Vector2Int(row, col)] = cellData;
                    CachedCellNodes[cellData] = node;
                }
            }

            ConnectAllNeighbors();
            AssignCellEffects();
            GridAvailable?.Invoke();

        }

        void ConnectAllNeighbors()
        {
            for (int row = 0; row < ROWS; row++)
            {
                for (int col = 0; col < COLUMNS; col++)
                {
                    ConnectNodeToNeighbors(Grid[row, col], row, col);
                }
            }
        }
        
        void AssignCellEffects()
        {
            // Assigning Winning Node
            Vector2Int winNode = new Vector2Int(Random.Range(1, ROWS - 1), Random.Range(0, COLUMNS - 1));
            GetCellData(winNode.x, winNode.y).ModifyCellEffect(Effect.Win);
            var temp = Instantiate(IndicatorPrefab, new Vector3(winNode.x, 1f, winNode.y), Quaternion.identity);
            temp.GetComponent<MeshRenderer>().material = WinIndicatorMat;
           
            //Assigning Nodes that can inflict Damage
            for (int i = 0; i < DAMAGE_NODES; i++)
            {
                int row;
                int col = Random.Range(0, ROWS - 1);;

                // Ensuring that the damage node is not also winning node
                do
                {
                    row = Random.Range(0, ROWS - 1); // Verifying only the row is enough to ensure we dont get the winning cell
                } while (row == winNode.x);
                
                GetCellData(row, col).ModifyCellEffect(Effect.Damage);
                temp = Instantiate(IndicatorPrefab, new Vector3(row, 1f, col), Quaternion.identity);
                temp.GetComponent<MeshRenderer>().material = DamageIndicatorMat;
            }
        }

        void ConnectNodeToNeighbors(CellNode node, int row, int col)
        {

            if (row > 0)
            {
                node.ConnectedCellNodes
                    .Add(Grid[row - 1, col]);
            }

            if (row < ROWS - 1)
            {
                node.ConnectedCellNodes
                    .Add(Grid[row + 1, col]);
            }

            if (col > 0)
            {
                node.ConnectedCellNodes
                    .Add(Grid[row, col - 1]);
            }

            if (col < COLUMNS - 1)
            {
                node.ConnectedCellNodes
                    .Add(Grid[row, col + 1]);
            }
        }

        public CellNode GetNode(int row, int col)
        {
            return Grid[row, col];
        }

        public CellNode? GetNodeFromCellData(Cell cellData)
        {
            if (CachedCellNodes.TryGetValue(cellData, out CellNode node))
                return node;

            return null;
        }

        public Cell GetCellData(int row, int col)
        {
            Vector2Int key = new Vector2Int(row, col);

            if (CachedCellsData.TryGetValue(key, out Cell cell))
                return cell;

            return null;
        }

        void HighlightReachableCells(CellNode startNode, int availablePoints)
        {
            HighlightCellNode(startNode, availablePoints);
        }

        void HighlightCellNode(CellNode startNode, int availablePoints)
        {
            int startRow = startNode.Row;
            int startCol = startNode.Col;

            for (int col = Mathf.Max(startCol - availablePoints, 0);
                 col <= Mathf.Min(startCol + availablePoints, COLUMNS - 1);
                 col++)
            {
                var _cell = GetCellData(startRow, col);
                _cell.Highlight(HighlightedMat);
                SelectedCells.Add(_cell);

            }

            for (int row = Mathf.Max(startRow - availablePoints, 0);
                 row <= Mathf.Min(startRow + availablePoints, ROWS - 1);
                 row++)
            {
                var _cell = GetCellData(row, startCol);
                _cell.Highlight(HighlightedMat);
                SelectedCells.Add(_cell);
            }
        }

        public void FindPosibleMoves(Vector2 startingNode, int balance)
        {
            if (MovementBoast)
            {
                balance *= 2;
                MovementBoast = false;
            }

            ClearSelections();
            HighlightReachableCells(GetNode((int)startingNode.x, (int)startingNode.y), balance);
        }

        public void EnableBoast()
        {
            MovementBoast = true;
        }

        public void ClearSelections()
        {
            foreach (var cell in SelectedCells)
            {
                cell.UnHighlight(DefaultMat);
            }

            SelectedCells.Clear();
        }

        public Vector2 GetGridSize()
        {
            return new Vector2(ROWS, COLUMNS);
        }
    }
}