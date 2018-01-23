using UnityEngine;

/// <summary>
/// This class is attached to the Game Manager, and controls the building process of turrets. 
/// </summary>
public class BuildManager : MonoBehaviour
{
    /// <summary>
    /// Allows access to the Build Manager without the need for a reference to it.
    /// </summary>
    public static BuildManager singletonInstance { get; private set; }

    /// <summary>
    /// Stores the prefab for the particle effect triggered when a turret is built.
    /// </summary>
    [SerializeField]
    private GameObject _buildEffectPrefab;
    public GameObject buildEffectPrefab { get { return _buildEffectPrefab; } }

    ///<summary>
    ///Stores the prefab for the particle effect triggered when a turret is sold. 
    /// </summary>
    [SerializeField]
    private GameObject _sellEffectPrefab;
    public GameObject sellEffectPrefab { get { return _sellEffectPrefab; } }

    /// <summary>
    /// The current selected turret. 
    /// </summary>
    public TurretBlueprint turretToBuild { get; private set; }

    /// <summary>
    /// The current selected node. 
    /// </summary>
    public Node selectedNode { get; private set; }

    /// <summary>
    /// The user interface for the selected node, with 'Upgrade' and 'Sell' buttons. 
    /// </summary>
    [SerializeField]
    private NodeUI nodeUI;
    
    private static UnityEngine.UI.Image moneyPanel;

    /// <summary>
    /// Without this 'Awake' pattern, the 'singletonInstance' object will
    /// have a new BuildManager attached to it every time the scene is reloaded.
    /// </summary>
    private void Awake()
    {
        ///If the 'singletonInstance' already has a BuildManager object in it,
        if (singletonInstance != null)
        {
            Debug.LogError(TD_Utils.ErrorNotif + "More than one BuildManager in scene!");
            return; ///Do nothing.  
        }
        ///Otherwise, set the instance to this script. 
        singletonInstance = this;

        moneyPanel = GameObject.FindGameObjectWithTag("Money").GetComponent<UnityEngine.UI.Image>();
    }

    /// <summary>
    /// This method is called by each of the methods in the Shop script. 
    /// These methods are triggered by clicking a button in the UI in order to select a turret. 
    /// </summary>
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        ///The current selected turret is changed to that of the parameter, controlled by the user interaction with the Shop. 
        turretToBuild = turret;
        ///When a new turret is selected using this function, 
        ///the currently selected node is deselected and the Node UI menu is hidden.
        DeselectNode();
    }
    
    /// <summary>
    /// Selects the node defined by the parameter 'node'.
    /// </summary>
    public void SelectNode(Node node)
    {
        ///If the selected node matches the parameter,  
        if (selectedNode == node)
        {
            ///The selection is toggled back off, 
            DeselectNode();
            ///and execution of this method ends.
            return;
        }

        ///Otherwise, the node is selected, 
        selectedNode = node;
        ///and any selected turret is deselected. 
        turretToBuild = null;
        ///Mouse over for details. 
        nodeUI.SetTarget(node);
    }

    /// <summary>
    /// The currently selected node is deselected, and the NodeUI menu is hidden.
    /// </summary>
    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    /// Boolean properties, as opposed to boolean functions. They return the output of statements. 

    ///<summary>
    ///If there is a turret selected (i.e. the selection isn't 'null'), the user can build a turret.
    /// </summary>
    public bool canBuildTurret { get { return turretToBuild != null; } }
    /// <summary>
    /// If the user's money exceeds the cost of the selected turret, the user has enough money for a turret.
    /// </summary>
    public bool hasMoneyForTurret { get { return PlayerStats.money >= turretToBuild.cost; } }

    public static void PlayInsufficientMoneyAnim ()
    {
        moneyPanel.GetComponent<Animation>().Play();
    }
}