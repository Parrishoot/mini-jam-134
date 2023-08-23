using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class EnemyBoatController : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer spriteRenderer;


    private GridMover gridMover;

    private GridManager gridManager;

    void Start()
    {
        gridMover = GetComponent<GridMover>();
        gridManager = LevelCreator.GetInstance().GetCurrentGridManager();
        
        JumpIn();
    }

    void JumpIn() {
        transform.DOJump(transform.position, .3f, 1, .3f).SetEase(Ease.InOutSine).OnComplete(() => {
            TickController.GetInstance().AddFastMoverAction(Move);  
        });
    }

    private void Move() {

        Direction movementDirection = GetRandomValidDirection();

        switch(movementDirection) {

            case Direction.NORTH:
                gridMover.MoveUp();
                break;

            case Direction.SOUTH:
                gridMover.MoveDown();
                break;

            case Direction.EAST:
                gridMover.MoveLeft();
                break;

            case Direction.WEST:
                gridMover.MoveRight();
                break;

        }
    }

    private Direction GetRandomValidDirection() {

        Vector2Int currentLocation = gridMover.GetCurrentSpace();

        List<Direction> directions = new List<Direction>();

        if(gridManager.IsWalkable(currentLocation.x, currentLocation.y + 1)) {
            directions.Add(Direction.NORTH);
        }

        if(gridManager.IsWalkable(currentLocation.x, currentLocation.y - 1)) {
            directions.Add(Direction.SOUTH);
        }

        if(gridManager.IsWalkable(currentLocation.x - 1, currentLocation.y)) {
            directions.Add(Direction.EAST);
        }

        if(gridManager.IsWalkable(currentLocation.x + 1, currentLocation.y)) {
            directions.Add(Direction.WEST);
        }

        if(directions.Count.Equals(0)) {
            return Direction.NORTH;
        }

        return directions[Random.Range(0, directions.Count)];

    }

    public void Die() {

        TickController.GetInstance().RemoveFastMoverAction(Move);
        gridMover.Remove();

        transform.DOKill();
        Sequence s = DOTween.Sequence();

        s.Append(transform.DOShakePosition(.25f, strength: .15f, vibrato: 100));
        s.Join(spriteRenderer.DOFade(0f, .25f));
        s.Play().OnComplete(()=> {
            Destroy(gameObject);
        });
    }
}
