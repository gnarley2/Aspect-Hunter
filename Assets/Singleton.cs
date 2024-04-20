using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    // Static reference to the instance
    private static Singleton _instance;

    // Public property to access the instance
    public static Singleton Instance
    {
        get { return _instance; }
    }

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // If there is no instance yet, set this as the instance
 
            _instance = this;
            DontDestroyOnLoad(gameObject);
        
        // If an instance already exists and it's not this, destroy this

    }

    // Your other methods and variables here
}
