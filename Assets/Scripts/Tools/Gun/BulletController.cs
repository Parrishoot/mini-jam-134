using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 10f;

    private Direction direction;

    private GridManager gridManager;

    private void Start() {
        gridManager = LevelCreator.GetInstance().GetCurrentGridManager();
    }

    public void Fire(Direction direction) {
        this.direction = direction;
    }

    private void Update() {

        // This is messy but it's 2am please forgive me
        switch(direction) {

            case Direction.NORTH:

                transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);
                break;

            case Direction.SOUTH:

                transform.Translate(Vector3.down * bulletSpeed * Time.deltaTime);
                break;

            case Direction.EAST:

                transform.Translate(Vector3.left * bulletSpeed * Time.deltaTime);
                break;

            case Direction.WEST:

                transform.Translate(Vector3.right * bulletSpeed * Time.deltaTime);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.isTrigger) {
            return;
        }

        EnemyBoatController enemyBoatController = other.gameObject.GetComponent<EnemyBoatController>();

        if(enemyBoatController != null) {
            GameManager.GetInstance().KillEnemy();
            enemyBoatController.Die();
        }

        Destroy(gameObject);
    }
}
