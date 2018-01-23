using UnityEngine;

public class SustainedTurret : Turret
{
    /// <summary>
    /// Set this to true if this turret should render a visible line between the firing point and the target. 
    /// </summary>
    [SerializeField]
    private bool usesLineRenderer;
    
    /// <summary>
    /// The Line Renderer component of this object, which renders a line between the firing point and the target. 
    /// </summary>
    [SerializeField]
    private LineRenderer lineRenderer;
    
    /// <summary>
    /// A particle effect that spawns at the point of impact on the end of the line renderer and the target.
    /// </summary>
    [SerializeField]
    private ParticleSystem impactEffect;

    [SerializeField]
    private Light impactLight;

    [SerializeField]
    private int damageOverTime = 30;

    [SerializeField]
    private float slowPercentage = 0.5F;

    private void Fire()
    {
        target.enemyComponent.TakeDamage(damageOverTime * Time.deltaTime);
        target.enemyComponent.Slow(slowPercentage);

        if(usesLineRenderer)
        {
            if(lineRenderer == null)
            {
                Debug.LogError("This turret uses a line renderer. Please add a line renderer component.");
                return;
            }
            RenderLine();
        }
        
        Vector3 dir = firingPoint.position - target.enemyObject.transform.position;

        impactEffect.transform.position = target.enemyObject.transform.position + dir.normalized;

        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    private void RenderLine()
    {
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }

        lineRenderer.SetPosition(0, firingPoint.transform.position);
        lineRenderer.SetPosition(1, target.enemyObject.transform.position);
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (target == null)
        {
            if(usesLineRenderer)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                }
            }
            impactEffect.Stop();
            impactLight.enabled = false;
            return;
        }

        base.Update();
        
        Fire();
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
    }
}
