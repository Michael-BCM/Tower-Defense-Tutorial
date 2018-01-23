using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// A base on which a tower or utility can be placed. 
/// </summary>
public class Node : MonoBehaviour
{
    private Renderer _renderer;
    private BuildManager buildManager_Instance;

    private Vector3 lightLocation;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        buildManager_Instance = BuildManager.singletonInstance;      
        lightLocation = transform.position + new Vector3(0, 10, 0);
    }

    /// <summary>
    /// The tower on this node.
    /// </summary>
    public GameObject turretOnNode;

    /// <summary>
    /// The blueprint of the tower on this node. 
    /// </summary>
    public TurretBlueprint turretBlueprint;
    
    /// <summary>
    /// Has the tower on this node been upgraded fully? 
    /// </summary>
    public bool isUpgraded { get; private set; }

    /// <summary>
    /// The colour of this node while the mouse is hovering over it. 
    /// </summary>
    [SerializeField]
    private Color hoverColor;

    /// <summary>
    /// The colour of this node if the player hasn't enough money to build the currently selected turret, 
    /// while the mouse is hovering over it.
    /// </summary>
    [SerializeField]
    private Color cannotBuildColor;
    
    /// <summary>
    /// The distance in all 3 axis to offset the turret on this node. 
    /// </summary>
    private readonly Vector3 positionOffset = new Vector3(0, 0.5F, 0);

    /// <summary>
    /// The point in world space to build a turret. 
    /// </summary>
    /// <returns></returns>
    public Vector3 BuildPosition() { return transform.position + positionOffset; }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        ///If there is a turret on this node,
        if(turretOnNode != null)
        {
            ///this node is selected,
            buildManager_Instance.SelectNode(this);

            ///and the execution of this function stops here. 
            return;
        }

        if(!buildManager_Instance.canBuildTurret)
            return;
        
        BuildTurret(buildManager_Instance.turretToBuild);
    }
    
    private void OnMouseEnter ()
    {
        if(!buildManager_Instance.canBuildTurret)
            return;

        Light indicatorLight = GameObject.FindGameObjectWithTag("Indicator Light").GetComponent<Light>();

        indicatorLight.enabled = true;
        indicatorLight.transform.position = lightLocation;
        indicatorLight.color = HoverColor();
    }

    private Color HoverColor ()
    {
        if (buildManager_Instance.hasMoneyForTurret)
            return hoverColor;

        return cannotBuildColor;
    }

    private void OnMouseExit()
    {
        GameObject.FindGameObjectWithTag("Indicator Light").GetComponent<Light>().enabled = false;
    }

    public void BuildTurret(TurretBlueprint blueprint)
    {
        //If the player can't afford to build the turret, 
        if (PlayerStats.money < blueprint.cost)
        {
            //Play a 'nope' sound effect.

            BuildManager.PlayInsufficientMoneyAnim();

            return; //Exit the function on this line/
            //do not execute anything from this line onwards to the closing bracket of this function. 
        }
        
        PlayerStats.PayForObject(blueprint.cost);
        //Remove money from player according to the cost of the turret. 

        turretOnNode = Instantiate(blueprint.prefab, BuildPosition(), Quaternion.identity);
        //Instantiate a turret of the selected type, and create a reference to said turret.
        //You could potentially create a global reference to the turret outside of the void functions:
        //a more fitting name for this would be 'recentlyInstantiatedTurret' or something shorter.

        turretBlueprint = blueprint;
        //Set this node's turret to be the turret that was just instantiated.

        GameObject effect = Instantiate(buildManager_Instance.buildEffectPrefab, BuildPosition(), Quaternion.identity);
        Destroy(effect, 5F);        
    }

    public void UpgradeTurret ()
    {
        //If the player can't afford to upgrade the turret, 
        if (PlayerStats.money < turretBlueprint.upgradeCost)
        {
            Debug.Log(TD_Utils.GameNotif + "Not enough money to upgrade that!");
            return; //Exit the function on this line/
            //do not execute anything from this line onwards to the closing bracket of this function. 
        }

        PlayerStats.PayForObject(turretBlueprint.upgradeCost);
        //Remove money from player according to the cost of the turret. 

        Destroy(turretOnNode); //Get rid of the old turret.

        //Build a new one. 
        turretOnNode = Instantiate(turretBlueprint.upgradedPrefab, BuildPosition(), Quaternion.identity);
        //Instantiate a turret of the selected type, and create a reference to said turret.
        //You could potentially create a global reference to the turret outside of the void functions:
        //a more fitting name for this would be 'recentlyInstantiatedTurret' or something shorter.
        
        GameObject effect = Instantiate(buildManager_Instance.buildEffectPrefab, BuildPosition(), Quaternion.identity);
        Destroy(effect, 5F);

        isUpgraded = true;

        Debug.Log(TD_Utils.GameNotif + "Turret upgraded!");
    }

    public void SellTurret()
    {
        PlayerStats.EarnMoney(turretBlueprint.SellAmount);

        GameObject effect = Instantiate(buildManager_Instance.sellEffectPrefab, BuildPosition(), Quaternion.identity);
        Destroy(effect, 5F);

        Destroy(turretOnNode);

        turretBlueprint = null;

        isUpgraded = false;
    }
}