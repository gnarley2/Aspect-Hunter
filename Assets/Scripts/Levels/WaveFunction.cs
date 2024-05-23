using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
// using UnityEditor;

// [ExecuteInEditMode]
public class WaveFunction : MonoBehaviour
{
    public int width;
    public int height;
    public Tile[] tiles;
    public List<Cell> grid;
    public Cell cell;

    int iterations = 0;

    void Awake()
    {
        grid = new List<Cell>();
        InitializeGrid();
    }
    public void InitializeGrid()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Cell newCell = Instantiate(cell, new Vector2(
                    (3.8f * x + this.transform.position.x) - width - 3.8f,
                    (3.8f * y + this.transform.position.y) - height), Quaternion.identity);
                newCell.CreateCell(false, tiles);
                grid.Add(newCell);
            }
        }

        // move the grid to the center of the map generator object

        StartCoroutine(CheckEntropy());
    }

    IEnumerator CheckEntropy()
    {
        List<Cell> tempGrid = new List<Cell>(grid);

        tempGrid.RemoveAll(c => c.collapsed);

        tempGrid.Sort((a, b) => { return a.tileOptions.Length - b.tileOptions.Length; });

        int arrLength = tempGrid[0].tileOptions.Length;
        int stopIndex = default;

        for (int i = 1; i < tempGrid.Count; i++)
        {
            if (tempGrid[i].tileOptions.Length > arrLength)
            {
                stopIndex = 1;
                break;
            }
        }

        if (stopIndex > 0)
        {
            tempGrid.RemoveRange(stopIndex, tempGrid.Count - stopIndex);
        }

        yield return new WaitForSeconds(0.1f);

        CollapseCell(tempGrid);
    }

    void CollapseCell(List<Cell> tempGrid)
    {
        int randIndex = UnityEngine.Random.Range(0, tempGrid.Count);

        Cell cellToCollapse = tempGrid[randIndex];

        cellToCollapse.collapsed = true;
        Tile selectedTile = cellToCollapse.tileOptions[UnityEngine.Random.Range(0, cellToCollapse.tileOptions.Length)];
        cellToCollapse.tileOptions = new Tile[] { selectedTile };

        Tile foundTile = cellToCollapse.tileOptions[0];
        Instantiate(foundTile, cellToCollapse.transform.position, Quaternion.identity);

        UpdateGeneration();
    }

    void UpdateGeneration()
    {
        List<Cell> newGenerationCell = new List<Cell>(grid);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var index = x + y * height;
                if (grid[index].collapsed)
                {
                    Debug.Log("called");
                    newGenerationCell[index] = grid[index];
                }
                else
                {
                    List<Tile> options = new List<Tile>();
                    foreach (Tile t in tiles)
                    {
                        options.Add(t);
                    }

                    // Update up
                    if (y > 0)
                    {
                        Cell up = grid[x + (y - 1) * height];
                        List<Tile> validOptions = new List<Tile>();

                        foreach (Tile possibleOptions in up.tileOptions)
                        {
                            var valOption = Array.FindIndex(tiles, obj => obj == possibleOptions);
                            var valid = tiles[valOption].upNeighbours;

                            validOptions = validOptions.Concat(valid).ToList();
                        }

                        CheckValidity(options, validOptions);
                    }

                    //Update right
                    if (x < width - 1)
                    {
                        // Cell right = grid[x + 1 + y * dimensions].GetComponent<Cell>();
                        Cell right = grid[x + 1 + y * width];
                        List<Tile> validOptions = new List<Tile>();

                        foreach (Tile possibleOptions in right.tileOptions)
                        {
                            var valOption = Array.FindIndex(tiles, obj => obj == possibleOptions);
                            var valid = tiles[valOption].leftNeighbours;

                            validOptions = validOptions.Concat(valid).ToList();
                        }

                        CheckValidity(options, validOptions);

                    }

                    // Update down
                    if (y < height - 1)
                    {
                        // Cell right = grid[x + (y + 1) * dimensions].GetComponent<Cell>();
                        Cell down = grid[x + (y + 1) * height];
                        List<Tile> validOptions = new List<Tile>();

                        foreach (Tile possibleOptions in down.tileOptions)
                        {
                            var valOption = Array.FindIndex(tiles, obj => obj == possibleOptions);
                            var valid = tiles[valOption].downNeighbours;

                            validOptions = validOptions.Concat(valid).ToList();
                        }

                        CheckValidity(options, validOptions);

                    }

                    // Update left
                    if (x > 0)
                    {
                        Cell left = grid[x - 1 + y * width];
                        List<Tile> validOptions = new List<Tile>();

                        foreach (Tile possibleOptions in left.tileOptions)
                        {
                            var valOption = Array.FindIndex(tiles, obj => obj == possibleOptions);
                            var valid = tiles[valOption].rightNeighbours;

                            validOptions = validOptions.Concat(valid).ToList();
                        }

                        CheckValidity(options, validOptions);
                    }

                    Tile[] newTileList = new Tile[options.Count];

                    for (int i = 0; i < options.Count; i++)
                    {
                        newTileList[i] = options[i];
                    }

                    newGenerationCell[index].RecreateCell(newTileList);
                }
            }
        }

        grid = newGenerationCell;
        iterations++;

        if (iterations < width * height)
        {
            StartCoroutine(CheckEntropy());
        }
    }

    void CheckValidity(List<Tile> optionList, List<Tile> validOption)
    {
        for (int x = optionList.Count - 1; x >= 0; x--)
        {
            var element = optionList[x];
            if (!validOption.Contains(element))
            {
                optionList.RemoveAt(x);
            }
        }
    }
}

// [CustomEditor(typeof(WaveFunction))]
// public class CustomEditorButton : Editor
// {
//     public override void OnInspectorGUI()
//     {
//         DrawDefaultInspector();

//         WaveFunction wf = (WaveFunction)target;

//         if (GUILayout.Button("Generate Map"))
//         {
//             wf.grid = new List<Cell>();
//             wf.InitializeGrid();
//         }
//     }
// }
