using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : IToolController
{

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private AudioSource audioSource;

    public override void Use(Direction direction)
    {
        if(AmmoController.GetInstance().OutOfAmmo()) {
            return;
        }

        toolAnimationController.Shake();
        audioSource.Play();

        AmmoController.GetInstance().Fire();

        GridManager gridManager = LevelCreator.GetInstance().GetCurrentGridManager();

        Vector2Int spawnPosition = playerGridMover.GetCurrentSpace(); 
        BulletController bullet = Instantiate(bulletPrefab, gridManager.GetLocationOfCell(spawnPosition.x, spawnPosition.y), Quaternion.identity).GetComponent<BulletController>();
        bullet.Fire(direction);
    }
}
