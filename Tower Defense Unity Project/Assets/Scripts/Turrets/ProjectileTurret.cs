using UnityEngine;

public class ProjectileTurret : SingleShotTurret
{
    [SerializeField]
    private GameObject bulletPrefab;

    protected override void Shoot()
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("There is no bullet prefab. Ensure that you have added a prefab.");
            return;
        }

        GameObject bulletObject = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);

        Bullet bullet = bulletObject.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target.enemyObject.transform);
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
    }
}
