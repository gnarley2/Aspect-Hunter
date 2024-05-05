using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public int currentXP;
    public int currentLevel;
    public int maxLevels = 10; // Set the maximum number of levels
    public LevelData[] levelData;


    public int totalCoinsCollected;

    [System.Serializable]
    public class LevelData
    {
        public int xpRequired; //This will be set in the Start method for each lvl.
        // AddItem any additional data you want to associate with each level
    }


    /// ////////////////////////////////////////////////////////////////////////
    /// ///////////////////////////////////////////////////////////////////////
    /// //Singleton Pattern to ensure only 1 instance can exist at once
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    instance = go.AddComponent<GameManager>();
                }
            }

            return instance;
        }
    }
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    /// ////////////////////////////////////////////////////////////////////////
    /// ///////////////////////////////////////////////////////////////////////


    void Start()
    {
        totalCoinsCollected = 0;
        currentXP = 0;
        currentLevel = 1;
        levelData = new LevelData[maxLevels];

        for (int i = 0; i < maxLevels; i++)
        {
            levelData[i] = new LevelData();
            levelData[i].xpRequired = 10 * (i + 1); // This is where we set the increasing xp requirements for each lvl. 
        }
    }



    /// ////////////////////////////////////////////////////////////
    /// ///////////////////////////////////////////////////////////
    /// Xp and Leveling up System
    
    //This is called from the XPOrb script when it is picked up.
    //Debug messages for now, but can be UI later.

    public void AddXP(int xpToAdd)
    {
        currentXP += xpToAdd;
        CheckLevelUp();

        // Calculate the XP required for the next level
        int nextLevelXpRequired;
        if (currentLevel < maxLevels)
        {
            nextLevelXpRequired = levelData[currentLevel].xpRequired;
        }
        else
        {
            nextLevelXpRequired = 0; // No more levels to progress
        }

        int remainingXP = nextLevelXpRequired - currentXP;

        // Condense the debug message into a single line
        string debugMessage = $"Level {currentLevel}: Current Lvl XP = {currentXP}";
        if (remainingXP > 0)
        {
            debugMessage += $", XP remaining for next lvl: {remainingXP}";
        }
        Debug.Log(debugMessage);
    }

    void CheckLevelUp()
    {
        if (currentLevel < maxLevels)
        {
            if (currentXP >= levelData[currentLevel].xpRequired)
            {
                currentXP -= levelData[currentLevel].xpRequired;
                currentLevel++;
                Debug.Log("Player reached lvl " + currentLevel + "!");

                // AddItem any additional level up logic here, such as increasing player stats, etc.
            }
        }
    }
    ///////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////




    ///////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////
    /// Coin Collecting
    public void AddCoin()
    {
        totalCoinsCollected++;
        Debug.Log($"Total coins collected: {totalCoinsCollected}");
    }

    ///////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////
}

