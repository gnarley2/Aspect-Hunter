using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP_Orb : MonoBehaviour
{
    public int xpValue = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddXP(xpValue);// Call the AddXP method in the GameManager
            Destroy(gameObject);
        }
    }
}
