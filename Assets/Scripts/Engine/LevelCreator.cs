using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class LevelCreator : Singleton<LevelCreator>
{

    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private GameObject gridPrefab;

    [SerializeField]
    private GameObject rockPrefab;

    [SerializeField]
    private GameObject hiddenTreasurePrefab;

    [SerializeField]
    private GameObject enemySpawnerPrefab;

    [SerializeField]
    private List<GameObject> itemPrefabs;

    [SerializeField]
    private float randomRockSpawnChance = .25f;

    [SerializeField]
    private int numCrabs = 3;

    [SerializeField]
    private Transform gridSpawnLocation;

    [SerializeField]
    private Transform gridGameLocation;

    [SerializeField]
    private Transform gridDespawnLocation;

    private GridManager currentGridManager;

    private GameObject currentGrid;

    private PlayerBoatController playerBoatController;

    public void CreateNewLevel()
    {
        SpawnGrid(); 
        SpawnPlayer(); 
        SpawnTreasures();
        SpawnCrabs();
        SpawnRocks();
        SetGridColor();
        ShowGrids();

    }

    private void SetGridColor()
    {
        GridColorManager.MapColor[] colorList = (GridColorManager.MapColor[]) System.Enum.GetValues(typeof(GridColorManager.MapColor));
        GridColorManager.MapColor mapColor = colorList[Random.Range(0, colorList.Count())];
        currentGridManager.SetColor(mapColor);
        BackgroundController.GetInstance().SetColor(mapColor);
    }

    private void SpawnPlayer()
    {
        if(playerBoatController == null) {
            playerBoatController = Instantiate(playerPrefab, 
                                               GetPlayerPosition(gridSpawnLocation),
                                               Quaternion.identity).GetComponent<PlayerBoatController>();
        }

        GridMover playerGridMover = playerBoatController.GetGridMover();
        currentGridManager.AddOccupant(0, 0, playerGridMover, false);
        playerBoatController.transform.SetParent(null);
    }

    private void ShowGrids()
    {
        Sequence s = DOTween.Sequence();

        GameObject gridToDestroy = currentGrid;
        currentGrid = currentGridManager.gameObject;

        if(gridToDestroy != null) {
            s.Append(gridToDestroy.transform.DOMove(gridDespawnLocation.position, 1f).SetEase(Ease.InOutCubic).OnComplete(()=> {
                Destroy(gridToDestroy);
            }));
        }

        s.Join(currentGrid.transform.DOMove(gridGameLocation.position, 1f).SetEase(Ease.InOutCubic));

        s.Join(playerBoatController.transform.DOMove(GetPlayerPosition(gridGameLocation), 1f).SetEase(Ease.InOutCubic));

        s.Play().OnComplete(()=> {
            GameManager.GetInstance().BeginRound();
            playerBoatController.GetGridMover().HardSetLocation(0, 0);
        });
    }   

    private void SpawnGrid()
    {
        currentGridManager = Instantiate(gridPrefab, gridSpawnLocation.position, Quaternion.identity).GetComponent<GridManager>();
        currentGridManager.ResetGrid();
    }

    private void SpawnTreasures()
    {

        Queue<GameObject> treasuresToSpawn = new Queue<GameObject>(itemPrefabs);

        while (!treasuresToSpawn.Count.Equals(0))
        {

            Vector2Int gridBounds = currentGridManager.GetGridBounds();
            int randomX = Random.Range(0, gridBounds.x);
            int randomY = Random.Range(0, gridBounds.y);

            if (!currentGridManager.IsOccupied(randomX, randomY))
            {
                GameObject hiddenTreasureObject = SpawnPrefab(hiddenTreasurePrefab, randomX, randomY);
                hiddenTreasureObject.GetComponent<HiddenTreasureOccupant>().SetItemPrefab(treasuresToSpawn.Dequeue());
            }
        }
    }

    private void SpawnCrabs() {
        
        int currentCrabs = 0;

        while (currentCrabs < numCrabs)
        {
            Vector2Int gridBounds = currentGridManager.GetGridBounds();
            int randomX = Random.Range(0, gridBounds.x);
            int randomY = Random.Range(0, gridBounds.y);

            if (!currentGridManager.IsOccupied(randomX, randomY))
            {
                GameObject hiddenTreasureObject = SpawnPrefab(enemySpawnerPrefab, randomX, randomY);
                hiddenTreasureObject.GetComponent<EnemySpawner>().BeginSpawn(randomX, randomY);
                
                currentCrabs++;
            }
        }
    }

    private void SpawnRocks()
    {
        Vector2Int gridBounds = currentGridManager.GetGridBounds();
        int spawnChance = (int)(1 / randomRockSpawnChance);

        for (int x = 0; x < gridBounds.x; x++)
        {
            for (int y = 0; y < gridBounds.y; y++)
            {

                int spawnRoll = Random.Range(0, spawnChance);

                if (spawnRoll.Equals(0) && !currentGridManager.IsOccupied(x, y))
                {
                    SpawnPrefab(rockPrefab, x, y);
                }
            }
        }
    }

    private GameObject SpawnPrefab(GameObject prefabToSpawn, int x, int y)
    {
        GameObject spawned = Instantiate(prefabToSpawn, currentGridManager.GetLocationOfCell(x, y), Quaternion.identity);
        currentGridManager.AddOccupant(x, y, spawned.GetComponent<GridOccupant>());

        return spawned;
    }

    public GridManager GetCurrentGridManager() {
        return currentGridManager;
    }

    private Vector3 GetPlayerPosition(Transform positionTransform) {
        return positionTransform.position + new Vector3(currentGridManager.GetGridSpaceSize() / 2, currentGridManager.GetGridSpaceSize() / 2, 0);
    }
}
