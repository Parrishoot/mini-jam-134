using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    [SerializeField]
    private GameObject holePrefab;

    private DestructableOccupant currentHole;

    private Vector2Int gridLocation;

    private int digDepth = 0;

    private enum GroundState {
        CLEAN,
        DIGGING,
        HOLE
    }

    private GridManager gridManager;

    private GroundState groundState;

    public void Start() {
        gridManager = LevelCreator.GetInstance().GetCurrentGridManager();
    }

    public void Dig() {
        
        switch(groundState) {

            case GroundState.CLEAN:

                digDepth++;
                groundState = GroundState.DIGGING;

                break;

            case GroundState.DIGGING:
                digDepth++;

                if(digDepth.Equals(3)) {
                    SpawnHole();
                    groundState = GroundState.HOLE;            
                }

                break;

            case GroundState.HOLE:
                currentHole.Hit();

                if(currentHole.IsDestroyed()) {
                    RemoveHole();
                }

                break;
        }
    }

    public void SetLocation(int x, int y) {
        gridLocation = new Vector2Int(x, y);
    }

    private void RemoveHole() {
        currentHole.Remove(gridLocation.x, gridLocation.y);
        groundState = GroundState.CLEAN;
        digDepth = 0;
    }

    private void SpawnHole() {
        currentHole = Instantiate(holePrefab, transform.position, Quaternion.identity).GetComponent<DestructableOccupant>();
        gridManager.AddOccupant(gridLocation.x, gridLocation.y, currentHole);
    }
}
