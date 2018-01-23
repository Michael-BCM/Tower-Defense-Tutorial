using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    [SerializeField]
    private GameObject ui;

    [SerializeField]
    private Text upgradeCost;

    public Button upgradeButton;

    [SerializeField]
    private Text sellAmount;

    private Node target;

    ///<summary>
    ///The selected node is set as the build target,
    ///and the currently hidden NodeUI moves to the node's position.
    ///Then, the NodeUI is shown to the user.
    /// </summary>
    public void SetTarget (Node _target)
    {
        target = _target;
        transform.position = target.BuildPosition();

        if(!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "MAX";
            upgradeButton.interactable = false;
        }

        sellAmount.text = "$" + target.turretBlueprint.SellAmount;

        ui.SetActive(true);
    }    

    public void Hide ()
    {
        ui.SetActive(false);
    }

    public void Upgrade ()
    {
        target.UpgradeTurret();
        BuildManager.singletonInstance.DeselectNode();
    }

    public void Sell ()
    {
        target.SellTurret();
        BuildManager.singletonInstance.DeselectNode();
    }
}