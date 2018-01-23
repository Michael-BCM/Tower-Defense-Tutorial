using UnityEngine;

///<summary>
///Contains functionality for an enemy's movement. Requires that the object have an 'Enemy' component on it. 
/// </summary>
[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    /// <summary>
    /// The target waypoint. 
    /// </summary>
    [SerializeField]
    private Waypoint target;
    
    /// <summary>
    /// The 'Enemy' component on this GameObject.
    /// </summary>
    [SerializeField]
    private Enemy enemy;
    
    /// <summary>
    /// The spawn point of this enemy.
    /// </summary>
    [Header("The spawn point of the enemy.")]
    [SerializeField]
    private Waypoint origin;

    /// <summary>
    /// Sets the spawn point (the wave spawner) of this enemy. Called by the Wave Spawner object that spawns this enemy.
    /// </summary>
    public void SetOrigin(Waypoint _origin) { origin = _origin; }

    private void Start()
    {
        GetNextWaypoint();
    }
    
    private void Update()
    {
        ///Get the enemy to look in the direction it is travelling,
        ///and to smoothly look at the next waypoint when changing direction.
        ///See your checklist and the Turret LookAt method for information on how to implement this.
        //transform.LookAt(target);
        
        transform.Translate(MoveDirection() * enemy.speed * Time.deltaTime, Space.World);

        ///If the enemy is within proximity of the waypoint, 
        if (Vector3.Distance(transform.position, target.transform.position) <= (enemy.speed / 100))
        {
            GetNextWaypoint();
        }
        enemy.ResetSpeed();
    }
    
    /// <summary>
    /// The direction in which the enemy is to move next. 
    /// </summary>
    private Vector3 MoveDirection()
    {
        return (target.transform.position - transform.position).normalized;
        ///If the direction is not normalized, the enemy will move at high speed, 
        ///relative to the distance between each waypoint, 
        ///because the difference of the two positions will likely be much greater than 1.
    }
    
    /// <summary>
    /// Sets the target to the next waypoint, determined by the waypoint object on the target.  
    /// If the target is already the final waypoint, 
    /// the enemy has reached the end of its path and is terminated.
    /// </summary>
    private void GetNextWaypoint()
    {
        if(target == null)
        {
            target = origin;
            return;
        }

        if(target.nextWaypoint != null)
        {
            target = target.nextWaypoint;
            return;
        }
        EndPath();
    }

    /// <summary>
    /// The enemy reaches the end of its path and is terminated. The player loses a life, 
    /// the number of enemies alive is reduced, and the enemy object is destroyed.
    /// </summary>
    private void EndPath()
    {
        PlayerStats.LoseLife();
        enemy.DeductAndDestroy();
    }
}