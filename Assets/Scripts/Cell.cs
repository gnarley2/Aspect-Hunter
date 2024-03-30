using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool collapsed;
    public Tile[] tileOptions;

    public void CreateCell(bool collapseState, Tile[] tiles)
    {
        collapsed = collapseState;
        tileOptions = tiles;
    }

    public void RecreateCell(Tile[] tiles)
    {
        tileOptions = tiles;
    }
}
