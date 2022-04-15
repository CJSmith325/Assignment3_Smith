using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // bool variable to track if random spawn has proced
    bool hasSpawned = false;
    // bool variable to restrict to one add/subtract per move, default false, true when merged then reset
    bool hasMerged = false;
    // spawn count variable to force 2 tile spawn on start, then 1 tile from then on out
    int spawnCount = 0;
    // prefab slots for editor
    public GameObject gridPrefab;
    public GameObject cubePrefab;
    // game object array for physical game pieces
    GameObject[,] blocks = new GameObject[4, 4];
    //gripieces declare
    GameObject[,] gridPieces = new GameObject[4, 4];
    // int array for text values to be stored/hopefully .parse or .tryparse still works
    int [,] values = new int[4, 4];
    private void Start()
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                gridPieces[x, y] = null;
            }
        }
        // reset spawn count
        spawnCount = 0;
        gridPieces = new GameObject[4, 4] { { gridPrefab, gridPrefab, gridPrefab, gridPrefab }, { gridPrefab, gridPrefab, gridPrefab, gridPrefab }, { gridPrefab, gridPrefab, gridPrefab, gridPrefab }, { gridPrefab, gridPrefab, gridPrefab, gridPrefab } };
        // initialize int values array to 0
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                values[x, y] = 0;
            }
        }
        // reset gameobject array for actual game blocks
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                blocks[x, y] = null;
            }
        }
        //for (int x = 0; x < 4; x++)
        //{
        //    for (int y = 0; y < 4; y++)
        //    {
        //        Debug.Log(values[x, y]);
        //    }
        //}
        // holder variables
        int holdX = 0;
        float holdY = 0;
        // grid declaration/initialization of gird prefabs
        
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                // line 23-29 is grid deployment to scene
                Instantiate(gridPieces[x, y], new Vector3(holdX, holdY, 2.5f), Quaternion.identity);
                holdX++;
                holdX++;
            }
            holdY++;
            holdY++;
            holdX = 0;
        }
        RandomBlockSpawn();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {

            RandomBlockSpawn();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {

            RandomBlockSpawn();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {

            RandomBlockSpawn();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {

            RandomBlockSpawn();
        }
    }

    // full logic for spawning random tile based on two rand int generations between 0-3, then check if cell if empty and repeat if not until eligible tile. if no eligible tile, end game
    void RandomBlockSpawn()
    {
        int rndRow = Random.Range(0, 4);
        int rndCol = Random.Range(0, 4);
        if (spawnCount == 0)
        {
            while (hasSpawned == false)
            {

                //spawn 1 blocks if cell is not occupied, if not then new rand
                if (blocks[rndRow, rndCol] == null)
                {
                    blocks[rndRow, rndCol] = cubePrefab;
                    Instantiate(blocks[rndRow, rndCol], new Vector3(rndRow * 2, rndCol * 2, 0), Quaternion.identity);
                    hasSpawned = true;
                }
                rndRow = Random.Range(0, 4);
                rndCol = Random.Range(0, 4);
            }
            hasSpawned = false;
            rndRow = Random.Range(0, 4);
            rndCol = Random.Range(0, 4);
            spawnCount++;
        }
        if (spawnCount >= 0)
        {
            //spawn 1 block   
            while (hasSpawned == false)
            {

                //spawn 1 blocks if cell is not occupied, if not then new rand
                if (blocks[rndRow, rndCol] == null)
                {
                    blocks[rndRow, rndCol] = cubePrefab;
                    Instantiate(blocks[rndRow, rndCol], new Vector3(rndRow * 2, rndCol * 2, 0), Quaternion.identity);
                    hasSpawned = true;
                }
                rndRow = Random.Range(0, 4);
                rndCol = Random.Range(0, 4);
            }
            hasSpawned = false;
            spawnCount++;
            // could display as move count in game if have time
        }
    }

    // function for pushing tiles in directions requiring subtracting from index
    void PushBlockDownIndex()
    {
        // look for empty cell, if not, check for merge before swapping cells

    }

    // function for pushing tiles in directions requiring adding to index
    void PushBlockUpIndex()
    {
        // look for empty cell, if not, check for merge before swapping cells

    }

    // visual aid in scene editor
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
