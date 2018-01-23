using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField]
    private Waypoint _nextWaypoint;

    public Waypoint nextWaypoint { get { return _nextWaypoint; } }
}
