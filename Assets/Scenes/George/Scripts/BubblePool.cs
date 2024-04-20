using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePool : MonoBehaviour
{
    public static BubblePool bubblePoolInstance;

    [SerializeField]
    private GameObject pooledBubble;
    private bool notEnoughBubblesInPool = true;

    private List<GameObject> bubbles;

    private void Awake()
    {
        bubblePoolInstance = this;
    }

    void Start()
    {
        bubbles = new List<GameObject>();
    }

    public GameObject GetBubble()
    {
        if (bubbles.Count > 0)
        {
            for (int i = 0; i < bubbles.Count; i++)
            {
                if (!bubbles[i].activeInHierarchy)
                {
                    return bubbles[i];
                }
            }
        }

        if (notEnoughBubblesInPool)
        {
            GameObject b = Instantiate(pooledBubble);
            b.SetActive(false);
            bubbles.Add(b);
            return b;
        }

        return null;
    }
}
