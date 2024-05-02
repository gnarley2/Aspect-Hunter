using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AspectType
{
    Fire,
    Frost,
    Shock,
    Poison,
    Water,
    None,
    // Combination
    Steam,
    Blast,
    Radiation,
    Gas,
    Paralysis,
    Pollution,
    Superconductor,
    Necrotic,
    Corrosion,
    IceSpike, 
    
}

public class AspectDatabase : MonoBehaviour
{
    public static AspectDatabase Instance;
    
    [System.Serializable]
    public class AspectCombination
    {
        public AspectType aspect1;
        public AspectType aspect2;
        public AspectType aspect3;

        public bool IsEqual(AspectType aspectType1, AspectType aspectType2)
        {
            return (aspect1 == aspectType1 && aspect2 == aspectType2) ||
                   (aspect2 == aspectType1 && aspect1 == aspectType2);
        }
    }

    void Awake()
    {
        Instance = this;
    }

    [SerializeField] private List<AspectCombination> AspectCombinations = new List<AspectCombination>();

    public AspectType GetCombination(AspectType aspectType1, AspectType aspectType2)
    {
        foreach (AspectCombination combination in AspectCombinations)
        {
            if (combination.IsEqual(aspectType1, aspectType2))
            {
                return combination.aspect3;
            }
        }

        Debug.LogError("Cant find combination");
        return AspectType.None;
    }
    
}
