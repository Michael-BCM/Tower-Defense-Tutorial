using UnityEngine;

public class HitScanTurret : SingleShotTurret
{
    [SerializeField]
    private ParticleSystem firingPointEffect;

    [SerializeField]
    private ParticleSystem impactEffect;

    [SerializeField]
    private float damage;

    protected override void Shoot()
    {
        firingPointEffect.Play();

        
        target.enemyComponent.TakeDamage(damage);
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
