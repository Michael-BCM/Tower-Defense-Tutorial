using UnityEngine;

/// <summary>
/// The shop. Add new turrets to your game with code, and add new public void functions to link up to the 'Purchase' buttons. 
/// </summary>
public class Shop : MonoBehaviour
{
    [SerializeField]
    private TurretBlueprint[] turrets;

    [SerializeField]
    private int selectionNum;

    [SerializeField]
    private UnityEngine.UI.Image selectionBox;

    [SerializeField]
    [Range(50, 150)]
    private int selectionBoxSpeed;

    private void Start()
    {
        selectionNum = 0;
        SelectTurret();
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0F)
        {
            if (selectionNum == turrets.Length - 1)
            {
                selectionNum = 0;
            }
            else
            {
                selectionNum++;
            }

            SelectTurret();
        }
        else if(Input.GetAxis("Mouse ScrollWheel") > 0F)
        {
            if (selectionNum <= 0)
            {
                selectionNum = turrets.Length - 1;
            }
            else
            {
                selectionNum--;
            }
            SelectTurret();
        }

        selectionBox.transform.position = Vector3.MoveTowards
            (
            selectionBox.transform.position, 
            turrets[selectionNum].shopImage.transform.position, 
            10 * selectionBoxSpeed * Time.deltaTime
            );
    }

    public void SelectTurret ()
    {
        BuildManager.singletonInstance.SelectTurretToBuild(turrets[selectionNum]);
    }    
}
