using System.Collections.Generic;
using UnityEngine;

public class PlayerBoatController : MonoBehaviour
{

    [SerializeField]
    private int moveQueueLength;

    [SerializeField]
    private GridMover gridMover;

    [SerializeField]
    private HitController hitController;

    // Update is called once per frame
    void Update()
{
        if(Input.GetKey(KeyCode.W)) {
            gridMover.MoveUp();
        }
        else if(Input.GetKey(KeyCode.S)) {
            gridMover.MoveDown();
        }
        else if(Input.GetKey(KeyCode.A)) {
            gridMover.MoveLeft();
        }
        else if(Input.GetKey(KeyCode.D)) {
            gridMover.MoveRight();
        }
    }

    public GridMover GetGridMover() {
        return gridMover;
    }

    private void OnTriggerEnter2D(Collider2D other) {

        EnemyBoatController enemyBoatController = other.gameObject.GetComponent<EnemyBoatController>();

        if(enemyBoatController != null && !hitController.IsInvincible() && !GameManager.GetInstance().IsPaused()) {
            hitController.ProcessHit();
            HealthCounter.GetInstance().LoseLife();
            enemyBoatController.Die();
        }
    }
}
