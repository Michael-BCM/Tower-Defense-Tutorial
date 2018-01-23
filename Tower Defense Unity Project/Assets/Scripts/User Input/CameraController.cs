using UnityEngine;

public class CameraController : MonoBehaviour
{
    /// <summary>
    /// Can the camera controller move?
    /// </summary>
    [SerializeField]
    private bool canMove = true;

    /// <summary>
    /// The panning speed of the camera. Determines how quickly it will move across the screen on keyboard or mouse input. 
    /// </summary>
    [Range(10, 120)]
    [SerializeField]
    private float panSpeed = 30F;

    /// <summary>
    /// Determines how close the mouse must be to the edge of the screen before the camera begins moving. 
    /// </summary>
    [Range(0, 100)]
    [SerializeField]
    private float mouseThreshold = 20F;

    /// <summary>
    /// Determines how quickly the camera zooms in and out on input.
    /// </summary>
    [Range(500, 5000)]
    [SerializeField]
    private float zoomSpeed;

    /// <summary>
    /// The furthest that the camera can zoom in. 
    /// </summary>
    [Range(10, 30)]
    [SerializeField]
    private float zoomRangeMax = 20F;

    /// <summary>
    /// The furthest that the camera can zoom out. 
    /// </summary>
    [Range(90, 120)]
    [SerializeField]
    private float zoomRangeMin;
    
    [Header("Camera Panning Keyboard Keys.")]
    [SerializeField]
    private KeyCode panForward = KeyCode.UpArrow;
    [SerializeField]
    private KeyCode panBackward = KeyCode.DownArrow;
    [SerializeField]
    private KeyCode panLeft = KeyCode.LeftArrow;
    [SerializeField]
    private KeyCode panRight = KeyCode.RightArrow;    

    private void Update()
    {
        #region Initial checks to see if we can run through the Update loop. 
        if(GameManager.hasGameEnded)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.M))
        {
            canMove = !canMove;
        }            

        if(!canMove)
        {
            return;
        }
        #endregion

        MoveCamera(direction(), panSpeed);

        /**/
            ZoomCamera();
        //}

        
    }

    /// <summary>
    /// Moves the camera controller in a direction 'direction', at speed 'moveSpeed'. 
    /// </summary>
    private void MoveCamera(Vector3 direction, float moveSpeed)
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    /// <summary>
    /// Zooms the camera in and out, dependent on the state of the scroll wheel. 
    /// </summary>
    private void ZoomCamera()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            pos.y -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime;
        }

        pos.y = Mathf.Clamp(pos.y, zoomRangeMax, zoomRangeMin);

        float xLowerLimit = (pos.y - 141) / 1.21F;
        float xUpperLimit = (49.75F - pos.y) / 1.19F;
        float zLowerLimit = ((pos.y - 20) / 15) - 5;
        float zUpperLimit = 1.067F * (95 - pos.y);

        pos.x = Mathf.Clamp(pos.x, xLowerLimit, xUpperLimit);
        pos.z = Mathf.Clamp(pos.z, zLowerLimit, zUpperLimit);

        

        transform.position = pos;

        
    }

    /// <summary>
    /// Returns a direction as a Vector3, dependent on user input. 
    /// </summary>
    private Vector3 direction ()
    {
        if (Input.GetKey(panForward) || Input.mousePosition.y > Screen.height - mouseThreshold)
            return Vector3.forward;
        
        else if (Input.GetKey(panBackward) || Input.mousePosition.y < mouseThreshold)
            return Vector3.back;
        
        else if (Input.GetKey(panLeft) || Input.mousePosition.x < mouseThreshold)
            return Vector3.left;
        
        else if (Input.GetKey(panRight) || Input.mousePosition.x > Screen.width - mouseThreshold)
            return Vector3.right;

        return Vector3.zero;
    }    
}