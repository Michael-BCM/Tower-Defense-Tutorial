using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private static int _money;
    public static int money
    {
        get { return _money; }
        private set
        {
            if (_money < 0)
                _money = 0;
            else
                _money = value;
        }
    }

    private static int _lives;
    public static int lives
    {
        get
        {
            if(_lives < 0)
            {
                return 0;
            }
            return _lives;
        }        
        private set
        {
            if(_lives < 0)
                _lives = 0;
            else
                _lives = value;
        }
    }
    
    private static int _rounds;
    public static int rounds { get { return _rounds; } private set { _rounds = value; } }
    
    [SerializeField]
    private int startMoney = 400;
    [SerializeField]
    private int startLives = 20;

    private void Start()
    {
        money = startMoney;
        lives = startLives;

        rounds = 0;
    }

    public static void PayForObject (int amount)
    {
        if(amount < 0)
        {
            Debug.LogError("Use 'PayForObject' to pay for items, " +
                "rather than increasing the money counter for some other use. Returning...");
            return;
        }
        else if (amount == 0)
        {
            Debug.LogError("Nothing in life is free. Returning...");
        }
        money -= amount;
    }

    public static void LoseLife ()
    {
        lives--;
    }

    public static void EarnMoney (int amount)
    {
        if (amount < 0)
        {
            Debug.LogError("Use 'EarnMoney' to earn money from actions, " +
                "rather than dropping the money counter for some other use. Returning...");
            return;
        }
        else if (amount == 0)
        {
            Debug.LogError("Nothing in life is free. Returning...");
        }
        money += amount;
    }

    public static void GoToNextRound ()
    {
        rounds++;
    }
}