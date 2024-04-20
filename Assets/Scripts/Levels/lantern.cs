using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lantern : MonoBehaviour
{
    private GameObject player;
    public bool isLanternEquipped = false;

    public GameObject lanternprefab;

    // Start is called before the first frame update
    void Start()
    {
       player = GameObject.FindWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        
            transform.position = player.transform.position;
         //   Instantiate(lanternprefab, player.transform);

    }
}
