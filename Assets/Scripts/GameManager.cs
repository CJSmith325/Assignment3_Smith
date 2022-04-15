using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //arrays for grid deployment/ tile values
    public GameObject gridPrefab;
    public GameObject cubePrefab;
    //GameObject[,] gridPieces = new GameObject[4, 4];
    int[,] tileValues = new int[4, 4];
    private void Start()
    {
        int holdX = 0;
        float holdY = 0;
        GameObject[,] gridPieces = new GameObject[4, 4] { { gridPrefab, gridPrefab, gridPrefab, gridPrefab }, { gridPrefab, gridPrefab, gridPrefab, gridPrefab }, { gridPrefab, gridPrefab, gridPrefab, gridPrefab }, { gridPrefab, gridPrefab, gridPrefab, gridPrefab } };
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                Instantiate(gridPieces[x, y], new Vector3(holdX, holdY, 2.5f), Quaternion.identity);
                holdX++;
                holdX++;
            }
            holdY ++;
            holdY++;
            holdX = 0;
        }
    }

    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.W))
        {

        }

        if (Input.GetKeyDown(KeyCode.A))
        {

        }

        if (Input.GetKeyDown(KeyCode.S))
        {

        }

        if (Input.GetKeyDown(KeyCode.W))
        {

        }
    }

    void RandomSpawnTile()
    {

    }


    void OnDrawGizmos()
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                
                    Gizmos.DrawWireCube(new Vector2(2 * x, 2 * y), new Vector2(1.25f, 1.25f));
                
            }
        }
    }
}
