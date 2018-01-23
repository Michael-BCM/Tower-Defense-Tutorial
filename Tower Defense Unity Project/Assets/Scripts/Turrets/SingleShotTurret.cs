using UnityEngine;

public abstract class SingleShotTurret : Turret
{
    [SerializeField]
    protected float fireRate = 1F;
    [SerializeField]
    protected float timeToNextFire = 0F;

    protected abstract void Shoot();

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (timeToNextFire <= 0F)
        {
            Shoot();
            timeToNextFire = 1F / fireRate;
        }

        timeToNextFire -= Time.deltaTime;
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
    }
}