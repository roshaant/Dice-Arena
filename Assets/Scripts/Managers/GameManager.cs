using Grid;
using UnityEngine;
using DA.Player;
using Unity.Mathematics;
using DA.Wallet;
using UI;

namespace Managers
{
    /// <summary>
    /// Manages the overall game state, player interactions, and UI updates.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance; // Singleton instance of the GameManager

        [SerializeField] private GridManager _gridManager; // Reference to the GridManager
        [SerializeField] private UIManager _uiManager;     // Reference to the UIManager
        [SerializeField] private Player _player;           // Reference to the player object
        [SerializeField] private Vector2 CurrentCell;       // Current cell position of the player
        private Wallet _wallet;                             // Player's wallet for balance management

        private void Awake()
        {
            // Singleton pattern for the GameManager
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            CurrentCell = new Vector2(0, 0); // Initialize the current cell position
        }

        private void Start()
        {
            // Subscribe to the GridAvailable event from GridManager
            _gridManager.GridAvailable += () =>
            {
                // Instantiate the player object and initialize the wallet
                var obj = Instantiate(_player, new Vector3(0, 1, 0), quaternion.identity);
                _player = obj.GetComponent<Player>();
                _wallet = new Wallet(0);
                _player.onHealthChange += () =>
                {
                    _uiManager.UpdatePlayerHealth(_player.GetPlayerHealth());
                };
            };
        }

        public int GetAvailableBalance()
        {
            return _wallet.GetAvailableBalance();
        }

        public void SubtractAvailableBalance(int balance)
        {
            _wallet.SubtractBalance(balance);
            _uiManager.UpdateCoins(_wallet.GetAvailableBalance());
        }

        public void MovePlayerToCell(Cell cell)
        {
            CellNode node = _gridManager.GetNodeFromCellData(cell).Value;

            // Calculate the cost based on the distance between current and target cell
            int cost = (int)(CurrentCell - new Vector2(node.Row, node.Col)).magnitude;
            
            if(cost <= 0) return;
            
            _wallet.SubtractBalance(cost);
            _uiManager.UpdateCoins(_wallet.GetAvailableBalance());

            CurrentCell = new Vector2(node.Row, node.Col); // Update the current cell position
            _player.MoveToCell(node.Position);             // Move the player to the target cell
            _gridManager.ClearSelections();                 // Clear highlighted cells after movement
            ApplyCellEffect(cell);
           
        }

        void ApplyCellEffect(Cell cell)
        {
            switch (cell.CellEffect)
            {
                case Effect.Default:
                    return;
                case Effect.Win:
                    TriggerGameWin();
                    break;
                case Effect.Damage:
                    _player.Damage(5);
                    break;
                default:
                    return;
            }
        }
        public void HighlightPossibleMoves()
        {
            // Highlight reachable cells based on the current cell and available balance
            _gridManager.FindPosibleMoves(CurrentCell, _wallet.GetAvailableBalance());
        }

        void TriggerGameWin()
        {
            _uiManager.DisplayGameWon();
        }
        public void ResetSelection()
        {
            _gridManager.ClearSelections(); // Clear highlighted cells
        }

        public void AddBalance(int balance)
        {
            _wallet.AddBalance(balance);
            _uiManager.UpdateCoins(_wallet.GetAvailableBalance());
        }

        public Vector2 GetGridSize()
        {
            return _gridManager.GetGridSize();
        }

        public Transform GetPlayer()
        {
            return _player.transform;
        }

        public void HealPlayer(int amount)
        {
            _player.Heal(amount);
        }

        public void EnableMoveBoast()
        {
            _gridManager.EnableBoast();
        }

        public void SetNotification(string message)
        {
            _uiManager.SetNotification(message);
        }
    }
}
