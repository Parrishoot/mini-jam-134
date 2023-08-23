using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemySpawner : GridOccupant
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private Transform spriteTransform;

    [SerializeField]
    private float waitToSpawnTime;

    [SerializeField]
    ParticleSystem particles;

    GridManager beginSpawnGridManager;


    public void BeginSpawn(int x, int y) {
        particles.Play();

        beginSpawnGridManager = LevelCreator.GetInstance().GetCurrentGridManager();
        
        spriteTransform.DOShakePosition(waitToSpawnTime, .05f, 50, fadeOut: false).OnComplete(() => {
            particles.Stop();
            SpawnEnemy(x, y);
            Destroy(gameObject);
        });
    }

    private void SpawnEnemy(int x, int y) {

        GridManager gridManager = LevelCreator.GetInstance().GetCurrentGridManager();

        if(gridManager != beginSpawnGridManager) {
            return;
        }

        gridManager.RemoveOccupant(x, y, this);

        GameObject spawned = Instantiate(enemyPrefab, gridManager.GetLocationOfCell(x, y), Quaternion.identity);
        gridManager.AddOccupant(x, y, spawned.GetComponent<GridOccupant>());
        spawned.GetComponent<GridMover>().HardSetLocation(x, y);
    }
}
