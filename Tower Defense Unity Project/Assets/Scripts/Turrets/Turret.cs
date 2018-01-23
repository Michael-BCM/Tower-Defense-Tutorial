using UnityEngine;
using System.Collections;

public abstract class Turret : MonoBehaviour
{
    /// <summary>
    /// The maximum range from which this turret can lock onto a target. 
    /// </summary>
    [SerializeField]
    protected float range = 15f;
        
    /// <summary>
    /// The speed at which this turret rotates to lock onto a target. 
    /// </summary>
    [SerializeField]
    protected float rotationSpeed = 10F;
    
    /// <summary>
    /// The pivot on which this turret rotates. 
    /// </summary>
    [SerializeField]
    protected Transform gunPivot;

    /// <summary>
    /// The point at which a projectile is fired, a line is rendered, or a hitscan weapon's fire effect is spawned. 
    /// </summary>
    [SerializeField]
    protected Transform firingPoint;

    protected EnemyInfo target;
    
    protected virtual void Start ()
    {
        StartCoroutine(RefreshTarget(0.5F));
    }

    protected IEnumerator RefreshTarget (float delay)
    {
        while(true)
        {
            target = Target();
            yield return new WaitForSeconds(delay);
        }
    }

    protected EnemyInfo Target ()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float distanceToNearestEnemy = Mathf.Infinity;

        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if(distanceToEnemy < distanceToNearestEnemy)
            {
                distanceToNearestEnemy = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && distanceToNearestEnemy <= range)
        {
            return new EnemyInfo(nearestEnemy, nearestEnemy.GetComponent<Enemy>());
        }

        return null;
    }

    protected virtual void Update()
    {
        LockOnTarget();
    }

    protected void LockOnTarget ()
    {
        Vector3 direction = target.enemyObject.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(gunPivot.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        gunPivot.rotation = Quaternion.Euler(0F, rotation.y, 0F);
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

public class EnemyInfo
{
    public GameObject enemyObject { get; private set; }
    public Enemy enemyComponent { get; private set; }

    public EnemyInfo(GameObject _object, Enemy _component)
    {
        enemyObject = _object;
        enemyComponent = _component;
    }
}