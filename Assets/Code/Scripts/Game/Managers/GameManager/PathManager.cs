namespace ProjectPBR.Managers
{
    using UnityEngine;
    using ProjectPBR.Level.Grid;
    using VUDK.Generic.Managers.Main.Interfaces;
    using ProjectPBR.Managers;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Player.Objective;
    using ProjectPBR.Config.Constants;
    using ProjectPBR.Level.Blocks;
    using VUDK.Extensions.Vectors;

    public class PathManager : MonoBehaviour, ICastGameManager<GameManager>
    {
        [SerializeField, Header("Level Path")]
        private Vector2Int _fromTile;
        [SerializeField]
        private Vector2Int _toTile;
        [SerializeField, Header("Fail Trigger")]
        private ObjectiveFailTrigger _failTrigger;

        public GameManager GameManager => MainManager.Ins.GameManager as GameManager;
        private LevelGrid _grid => GameManager.GameGridManager.Grid;

        private void Awake()
        {
            _failTrigger.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnBeginObjectivePhase, StartPathing);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener(Constants.Events.OnBeginObjectivePhase, StartPathing);
        }

        private void StartPathing()
        {
            if (!IsPathValid(out Vector3 interruptPosition))
            {
                _failTrigger.gameObject.SetActive(true);
                _failTrigger.transform.position = interruptPosition;
            }
        }

        private bool IsPathValid(out Vector3 interruptPosition)
        {
            LevelTile[,] tiles = _grid.GridTiles;

            LevelTile fromTile = tiles[_fromTile.x, _fromTile.y];
            LevelTile toTile = tiles[_toTile.x, _toTile.y];

            Vector2Int currentTilePosition = fromTile.GridPosition;
            interruptPosition = Vector3.zero;

            while (currentTilePosition.x <= toTile.GridPosition.x)
            {
                currentTilePosition.x++;

                if (currentTilePosition == toTile.GridPosition)
                    return true;

                if (currentTilePosition.x >= _grid.Size.x || currentTilePosition.y >= _grid.Size.y)
                {
                    Debug.LogError("Path out of grid");
                    return false;
                }

                LevelTile currentTile = tiles[currentTilePosition.x, currentTilePosition.y];

                if (currentTile.IsOccupied)
                {
                    if (currentTile.InsertedBlock.IsClimbable && !CheckSurface(currentTile, Vector2.up, out bool isBlock, out bool isFlat))
                    {
                        currentTilePosition.y++;
                    }
                    else
                    {
                        interruptPosition = currentTile.transform.position;
                        return false;
                    }
                }
                else
                {
                    if (CheckSurface(currentTile, Vector2.down, out bool isBlock, out bool isFlat))
                    {
                        if (!isFlat)
                        {
                            if (isBlock)
                            {
                                LevelTile underTile = tiles[currentTilePosition.x, currentTilePosition.y - 1];

                                if (underTile.InsertedBlock.IsSlideable)
                                {
                                    currentTilePosition.y--;
                                }
                                else
                                {
                                    interruptPosition = currentTile.transform.position;
                                    return false;
                                }
                            }
                        }
                    }
                    else
                    {
                        interruptPosition = currentTile.transform.position;
                        return false;
                    }
                }
            }

            return false;
        }

        private bool CheckSurface(LevelTile fromTile, Vector2 direction, out bool isBlock, out bool isFlatSurface)
        {
            Vector3 position = fromTile.transform.position + (Vector3.left * .15f); // To avoid raycast hitting the same tile when checking up, TO DO: Optimize this
            RaycastHit2D hit = Physics2D.Raycast(position, direction, 1.25f, MainManager.Ins.GameConfig.GroundLayerMask);
            isBlock = false;

            if (hit)
            {
                Vector2 hitNormal = hit.normal;
                Vector2 downDirection = Vector2.down;
                float angle = Vector2.Angle(hitNormal, downDirection);
                float flatAngleThreshold = 10f;
                //Debug.Log("-Hitted: " + hit.transform.name);
                //Debug.Log("-Calculating angle: " + Mathf.Abs(angle - 180f));
                //Debug.Log("-Treshold: " + flatAngleThreshold);

                isFlatSurface = Mathf.Abs(angle - 180f) < flatAngleThreshold;

                if(hit.transform.TryGetComponent(out BlockBase block))
                    isBlock = true;

                return true;
            }

            isFlatSurface = false;
            return false;
        }
    }
}
