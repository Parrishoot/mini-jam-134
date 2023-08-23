using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GridMover : GridOccupant
{
    [SerializeField]
    private float movementSpeed = .5f;

    [SerializeField]
    private Vector2Int startingSpace = new Vector2Int(0, 0);

    private Vector2Int currentSpace;

    private Vector2Int targetSpace;

    private bool moving = false;

    private float rotationAmount = 10f;

    private Sequence sequence;

    private bool active = true;

    protected void Start() {

        if(OccupantType != SpaceOccupant.NONE) {
            LevelCreator.GetInstance().GetCurrentGridManager().AddOccupant(startingSpace.x, startingSpace.y, this);
        }
    }

    public void SetTarget(int x, int y) {

        if(!active) {
            return;
        }

        Vector2Int newTarget = new Vector2Int(x, y);

        GridManager gridManager = LevelCreator.GetInstance().GetCurrentGridManager();

        // Set the character to face the way they're walking
        int direction = (newTarget - currentSpace).x;
        if(Mathf.Sign(direction) != Mathf.Sign(transform.localScale.x)) {
            transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
        }

        // Check if we can move to the open space
        if(!gridManager.IsWalkable(x, y) || moving) {
            return;
        }

        targetSpace = newTarget;
        moving = true;

        if(OccupantType != SpaceOccupant.NONE) {
             // Set the current space to empty and the target space to be filled by the mover
            gridManager.RemoveOccupant(currentSpace.x, currentSpace.y, this);
            gridManager.AddOccupant(targetSpace.x, targetSpace.y, this);
        }

        // Tween movement to that space
        sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(gridManager.GetLocationOfCell(x, y), movementSpeed).SetEase(Ease.InOutCubic).OnComplete(() => {
            currentSpace = targetSpace;
            moving = false;
            rotationAmount *= -1;
        }));
        sequence.Join(transform.DORotate(new (0, 0, rotationAmount), movementSpeed).SetEase(Ease.InOutCubic));
        sequence.Play();
    }

    public Vector2Int GetCurrentSpace() {

        if(sequence == null) {
            return currentSpace;
        }

        return sequence.ElapsedPercentage() >= .05f ? targetSpace : currentSpace;
    }

    public void HardSetLocation(int x, int y) {
        transform.position = LevelCreator.GetInstance().GetCurrentGridManager().GetLocationOfCell(x, y);
        currentSpace = new Vector2Int(x, y);
    }

    public void MoveLeft() {
        SetTarget(currentSpace.x - 1, currentSpace.y);
    }

    public void MoveRight() {
        SetTarget(currentSpace.x + 1, currentSpace.y);
    }

    public void MoveUp() {
        SetTarget(currentSpace.x, currentSpace.y + 1);
    }

    public void MoveDown() {
        SetTarget(currentSpace.x, currentSpace.y - 1);
    }

    public void Remove() {
        Remove(currentSpace.x, currentSpace.y);
        Remove(targetSpace.x, targetSpace.y);
    }

    public void SetActive(bool active) {
        this.active = active;
    }
}
