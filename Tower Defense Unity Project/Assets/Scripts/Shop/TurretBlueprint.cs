using UnityEngine;

/// <summary>
/// To add a turret to your game, a field of this type and of the same name must first be added to the shop.
/// </summary>
[System.Serializable]
public class TurretBlueprint
{
    [SerializeField]
    private string _name;

    /// <summary>
    /// The name of the turret.
    /// </summary>
    public string name { get { return _name; } }

    [SerializeField]
    private GameObject _prefab;
    /// <summary>
    /// The turret or utility at Level 1. 
    /// </summary>
    public GameObject prefab { get { return _prefab; } }

    [SerializeField]
    private int _cost;
    /// <summary>
    /// The cost of the turret.
    /// </summary>
    public int cost { get { return _cost; } }

    [SerializeField]
    private GameObject _upgradedPrefab;
    /// <summary>
    /// The turret or utility at Level 2. 
    /// </summary>
    public GameObject upgradedPrefab { get { return _upgradedPrefab; } }

    [SerializeField]
    private int _upgradeCost;
    /// <summary>
    /// The cost to upgrade from Level 1 to Level 2. 
    /// </summary>
    public int upgradeCost { get { return _upgradeCost; } }

    /// <summary>
    /// The amount refunded when the turret is sold. 
    /// </summary>
    public int SellAmount { get { return cost / 2; } }

    [SerializeField]
    private UnityEngine.UI.Image _shopImage;

    /// <summary>
    /// The image of this turret as displayed in the shop. 
    /// </summary>
    public UnityEngine.UI.Image shopImage { get { return _shopImage; } }
}