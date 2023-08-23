using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IToolController: MonoBehaviour
{
    [SerializeField]
    protected GridMover playerGridMover;

    [SerializeField]
    protected ToolAnimationController toolAnimationController;

    public abstract void Use(Direction direction);

    public virtual void SetActive() {
        gameObject.SetActive(true);
    }

    public virtual void SetInactive() {
        gameObject.SetActive(false);
    }

    protected Vector2Int FindSpace(Direction direction) {
        
        Vector2Int space = playerGridMover.GetCurrentSpace();;

        switch(direction) {

            case Direction.NORTH:
                space += Vector2Int.up;
                break;

            case Direction.SOUTH:
                space += Vector2Int.down;
                break;

            case Direction.EAST:
                space += Vector2Int.left;
                break;

            case Direction.WEST:
                space += Vector2Int.right;
                break;

        }

        return space;
    }
    
}
