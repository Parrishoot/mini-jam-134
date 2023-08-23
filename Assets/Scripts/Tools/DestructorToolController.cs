using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestructorToolController : IToolController
{
    [SerializeField]
    private SpaceOccupant occupantType;


    public override void Use(Direction direction)
    {
        Vector2Int space = FindSpace(direction);

        GridManager gridManager = LevelCreator.GetInstance().GetCurrentGridManager();

        if(!gridManager.IsInRange(space.x, space.y) || toolAnimationController.InProgress()) {
            return;
        }

        toolAnimationController.Strike();

        if(gridManager.GetOccupant(space.x, space.y).Equals(occupantType)) {
            DestructableOccupant occupant = gridManager.GetObjectAtLocation<DestructableOccupant>(space.x, space.y);
            occupant.Hit();

            if(occupant.IsDestroyed()) {
                occupant.Remove(space.x, space.y);
            }
        }
    }
}
