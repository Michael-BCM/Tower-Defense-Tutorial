using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float speed = 70F;
    [SerializeField]
    private float explosionRadius = 0;
    [SerializeField]
    private GameObject impactEffect;
    [SerializeField]
    private int damage = 50;

    public void Seek(Transform _target) { target = _target; }
    
    private void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;

        float distanceThisFrame = speed * Time.deltaTime;

        if(direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    private void HitTarget ()
    {
        GameObject EffectInstance = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(EffectInstance, 2F);

        if (explosionRadius > 0F)
            Explode();

        else
            Damage(target);
        
        Destroy(gameObject);        
    }

    private void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if (e == null)
            return;

        e.TakeDamage(damage);
    }

    void Explode ()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider col in colliders)
        {
            if(col.tag == "Enemy")
                Damage(col.transform);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}