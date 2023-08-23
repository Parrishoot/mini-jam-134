using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridManager: MonoBehaviour
{

    public Vector2Int gridSize = new Vector2Int(10, 10);

    [SerializeField]
    private Grid worldGrid;

    private List<GameObject>[,] occupantGrid;

    private SpaceOccupant[] blockingTypes = {
        SpaceOccupant.ROCK,
        SpaceOccupant.HOLE
    };

    [SerializeField]
    private GridColorManager gridColorManager;

    public void ResetGrid() {
        occupantGrid = new List<GameObject>[gridSize.x, gridSize.y];
        for(int x = 0; x < gridSize.x; x++) {
            for(int y = 0; y < gridSize.y; y++) {
                occupantGrid[x,y] = new List<GameObject>();
            }
        }
    }

    public Vector3 GetLocationOfCell(int x, int y) {
        return worldGrid.GetCellCenterWorld(new Vector3Int(x, y, 0));
    }

    public bool IsInRange(int x, int y) {
        return x >= 0 &&
               y >= 0 &&
               x <= gridSize.x - 1 &&
               y <= gridSize.y - 1;
    }

    public bool IsWalkable(int x, int y) {

        if(!IsInRange(x, y)) {
            return false;
        }

        return !blockingTypes.Contains(GetOccupant(x,y));
    }

    public Vector2Int GetGridBounds() {
        return gridSize;
    }

    public bool IsOccupied(int x, int y) {
        return !GetOccupant(x, y).Equals(SpaceOccupant.NONE);
    }

    public SpaceOccupant GetOccupant(int x, int y) {

        GridOccupant occupant = GetObjectAtLocation<GridOccupant>(x, y);

        if(occupant == null) {
            return SpaceOccupant.NONE;
        }

        return occupant.OccupantType;
    }


    public void AddOccupant(int x, int y, GridOccupant gridOccupant, bool setParent = true) {
        occupantGrid[x, y].Add(gridOccupant.gameObject);

        if(setParent) {
            gridOccupant.gameObject.transform.SetParent(transform, true);
        }
    }

    public void RemoveOccupant(int x, int y, GridOccupant gridOccupant) {
        occupantGrid[x, y].Remove(gridOccupant.gameObject);
        gridOccupant.gameObject.transform.SetParent(null, true);
    }

    public T GetObjectAtLocation<T>(int x, int y) {

        foreach(GameObject objectAtLocation in occupantGrid[x, y]) {

            // This shouldn't be happening but it is and it's a jam so... we're just gonna move on
            if(objectAtLocation == null) {
                continue;
            }

            T component = objectAtLocation.GetComponent<T>();

            if(component != null) {
                return component;
            }
        }

        return default(T);
    }

    public float GetGridSpaceSize() {
        return worldGrid.cellSize.x;
    }

    public void SetColor(GridColorManager.MapColor mapColor) {
        gridColorManager.SetColor(mapColor);
    }
}
