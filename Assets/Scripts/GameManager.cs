using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

// LINES125-126 for text manipulation
// keep blocks[,] and values[,] parallel, always manipulate together

public class GameManager : MonoBehaviour
{
    //float variable for lerp argument
    float lerpTime = 4f;
    // bool variable to track if random spawn has proced
    bool hasSpawned = false;
    // bool variable to restrict to one add/subtract per move, default false, true when merged then reset
    
    // spawn count variable to force 2 tile spawn on start, then 1 tile from then on out
    int spawnCount = 0;
    // count variable to track # elements in array
    int countLength = 0;
    // prefab slots for editor
    public GameObject gridPrefab;
    public GameObject cubePrefab;
    // game object array for physical game pieces
    GameObject[,] blocks = new GameObject[4, 4];
    //gridpieces declare
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
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            PushBlockW();
            Check2048();
            RandomBlockSpawn();
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PushBlockA();
            Check2048();
            RandomBlockSpawn();
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            PushBlockS();
            Check2048();
            RandomBlockSpawn();
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            PushBlockD();
            Check2048();
            RandomBlockSpawn();
        }
    }

    void Check2048()
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (values[x,y] == 2048)
                {
                    SceneManager.LoadScene("VictoryScreen");
                }
            }
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
                        GameObject tempObj = new GameObject();
                        tempObj = Instantiate(cubePrefab, new Vector3(rndRow * 2, rndCol * 2, 0), Quaternion.identity);
                        var textValue = tempObj.GetComponentInChildren<TextMeshProUGUI>();
                        textValue.text = "2";
                        // || arrays
                        blocks[rndRow, rndCol] = tempObj;
                        values[rndRow, rndCol] = 2;
                        //
                        hasSpawned = true;
                    }
                    rndRow = Random.Range(0, 4);
                    rndCol = Random.Range(0, 4);
                }
                hasSpawned = false;
                rndRow = Random.Range(0, 4);
                rndCol = Random.Range(0, 4);
                spawnCount++;
                countLength++;
            }
            
            if (spawnCount >= 0)
            {
                // THE BLOWUP IS RIGHT HERE AT THIS WHILE LOOP
                //spawn 1 block  
                while (hasSpawned == false)
                {
                    //spawn 1 blocks if cell is not occupied, if not then new rand
                    if (blocks[rndRow, rndCol] == null)
                    {
                        GameObject tempObj = new GameObject();
                        tempObj = Instantiate(cubePrefab, new Vector3(rndRow * 2, rndCol * 2, 0), Quaternion.identity);
                        var textValue = tempObj.GetComponentInChildren<TextMeshProUGUI>();
                        textValue.text = "2";
                        // || arrays
                        try
                        {
                            blocks[rndRow, rndCol] = tempObj;
                        }
                        catch(UnityException ex)
                        {
                            SceneManager.LoadScene("GameOver");
                        }
                        values[rndRow, rndCol] = 2;
                        //
                        Debug.Log(values[rndRow, rndCol]);
                        hasSpawned = true;
                        Debug.Log(rndRow);
                        Debug.Log(rndCol);
                        TextMeshProUGUI cubeValue = cubePrefab.GetComponentInChildren<TextMeshProUGUI>();
                        Debug.Log(cubeValue.text.ToString());
                        
                    }
                    rndRow = Random.Range(0, 4);
                    rndCol = Random.Range(0, 4);
                    
                    if (countLength == 16)
                    {
                    // primitive bug fix
                    SceneManager.LoadScene("GameOver");
                    break;
                    }
                    
                }
                hasSpawned = false;
                // could display as move count in game if have time (-2 for initial spawns)
                spawnCount++;
                countLength++;
            }
        
        
    }

    // function for pushing tiles in directions requiring subtracting from index
    void PushBlockW()
    {
        GameObject swapObj = new GameObject();
        int swapInt = 0;
        float holderX = 0f;
        float holderY = 0f;
        // look for empty cell, if not, check for merge before swapping cells
        bool hasMerged = false;
        for (int x = 0; x < 4; x++)
        {
            hasMerged = false;
            for (int y = 0; y < 3; y++)
            {
                if (blocks[x, y] != null)
                {
                    if (blocks[x, y + 1] == null)
                    {
                        //while (blocks[x, y + 1] == null)
                        //{
                            //empty cell, simple move
                            swapObj = blocks[x, y + 1];
                            blocks[x, y + 1] = blocks[x, y];
                            blocks[x, y] = swapObj;
                            swapInt = values[x, y + 1];
                            values[x, y + 1] = values[x, y];
                            values[x, y] = swapInt;
                            holderX = x;
                            holderY = y;
                            Vector3 vecMove = Vector3.Lerp(new Vector3(holderX * 2, holderY * 2, 0), new Vector3(holderX * 2, (holderY * 2) + 2, 0), lerpTime);
                            blocks[x, y + 1].transform.position = vecMove;
                        //}
                    }
                    if (blocks[x, y + 1] != null)
                    {
                        //merge and move
                        if (hasMerged == false)
                        {
                            if (values[x, y] == values[x, y + 1])
                            {
                                // change scene by updating gameobject array, update and utilize backend values array
                                int holdAdd;
                                string strHold;
                                holdAdd = values[x, y] + values[x, y + 1];
                                values[x, y + 1] = holdAdd;
                                strHold = holdAdd.ToString();
                                TextMeshProUGUI txtHold = blocks[x, y + 1].GetComponentInChildren<TextMeshProUGUI>();
                                txtHold.text = strHold;
                                values[x, y] = 0;
                                
                                Destroy(blocks[x, y].gameObject);
                                countLength--;
                            }                           
                            hasMerged = true;
                        }
                        if (hasMerged == true) 
                        {
                            //push +1 block until next index not null

                        }
                        
                        
                    }
                }
            }
        }
    }

    void PushBlockS()
    {
        GameObject swapObj = new GameObject();
        int swapInt = 0;
        float holderX = 0f;
        float holderY = 0f;
        // look for empty cell, if not, check for merge before swapping cells
        bool hasMerged = false;
        for (int x = 0; x < 4; x++)
        {
            hasMerged = false;
            for (int y = 3; y > 0; y--)
            {
                if (blocks[x, y] != null)
                {
                    if (blocks[x, y - 1] == null)
                    {
                        //while (blocks[x, y + 1] == null)
                        //{
                        //empty cell, simple move
                        swapObj = blocks[x, y - 1];
                        blocks[x, y - 1] = blocks[x, y];
                        blocks[x, y] = swapObj;
                        swapInt = values[x, y - 1];
                        values[x, y - 1] = values[x, y];
                        values[x, y] = swapInt;
                        holderX = x;
                        holderY = y;
                        Vector3 vecMove = Vector3.Lerp(new Vector3(holderX * 2, holderY * 2, 0), new Vector3(holderX * 2, (holderY * 2) - 2, 0), lerpTime);
                        blocks[x, y - 1].transform.position = vecMove;
                        //}
                    }
                    if (blocks[x, y - 1] != null)
                    {
                        //merge and move
                        if (hasMerged == false)
                        {
                            if (values[x, y] == values[x, y - 1])
                            {
                                // change scene by updating gameobject array, update and utilize backend values array
                                int holdAdd;
                                string strHold;
                                holdAdd = values[x, y] + values[x, y - 1];
                                values[x, y - 1] = holdAdd;
                                strHold = holdAdd.ToString();
                                TextMeshProUGUI txtHold = blocks[x, y - 1].GetComponentInChildren<TextMeshProUGUI>();
                                txtHold.text = strHold;
                                values[x, y] = 0;

                                Destroy(blocks[x, y].gameObject);
                                countLength--;
                            }
                            hasMerged = true;
                        }
                        if (hasMerged == true)
                        {
                            //push +1 block until next index not null

                        }


                    }
                }
            }
        }
    }

    void PushBlockD()
    {
        GameObject swapObj = new GameObject();
        int swapInt = 0;
        float holderX = 0f;
        float holderY = 0f;
        // look for empty cell, if not, check for merge before swapping cells
        bool hasMerged = false;
        for (int y = 0; y < 4; y++)
        {
            hasMerged = false;
            for (int x = 0; x < 3; x++)
            {
                if (blocks[x, y] != null)
                {
                    if (blocks[x+1, y] == null)
                    {
                        //while (blocks[x, y + 1] == null)
                        //{
                        //empty cell, simple move
                        swapObj = blocks[x+1, y];
                        blocks[x+1, y] = blocks[x, y];
                        blocks[x, y] = swapObj;
                        swapInt = values[x+1, y];
                        values[x+1, y] = values[x, y];
                        values[x, y] = swapInt;
                        holderX = x;
                        holderY = y;
                        Vector3 vecMove = Vector3.Lerp(new Vector3(holderX * 2, holderY * 2, 0), new Vector3((holderX * 2) + 2, (holderY * 2), 0), lerpTime);
                        blocks[x+1, y].transform.position = vecMove;
                        //}
                    }
                    if (blocks[x+1, y] != null)
                    {
                        //merge and move
                        if (hasMerged == false)
                        {
                            if (values[x, y] == values[x+1, y])
                            {
                                // change scene by updating gameobject array, update and utilize backend values array
                                int holdAdd;
                                string strHold;
                                holdAdd = values[x, y] + values[x+1, y];
                                values[x+1, y] = holdAdd;
                                strHold = holdAdd.ToString();
                                TextMeshProUGUI txtHold = blocks[x+1, y].GetComponentInChildren<TextMeshProUGUI>();
                                txtHold.text = strHold;
                                values[x, y] = 0;

                                Destroy(blocks[x, y].gameObject);
                                countLength--;
                            }
                            hasMerged = true;
                        }
                        if (hasMerged == true)
                        {
                            //push +1 block until next index not null

                        }


                    }
                }
            }
        }
    }

    void PushBlockA()
    {
        // look for empty cell, if not, check for merge before swapping cells
        GameObject swapObj = new GameObject();
        int swapInt = 0;
        float holderX = 0f;
        float holderY = 0f;
        // look for empty cell, if not, check for merge before swapping cells
        bool hasMerged = false;
        for (int y = 0; y < 4; y++)
        {
            hasMerged = false;
            for (int x = 3; x > 0; x--)
            {
                if (blocks[x, y] != null)
                {
                    if (blocks[x - 1, y] == null)
                    {
                        //while (blocks[x, y + 1] == null)
                        //{
                        //empty cell, simple move
                        swapObj = blocks[x - 1, y];
                        blocks[x - 1, y] = blocks[x, y];
                        blocks[x, y] = swapObj;
                        swapInt = values[x - 1, y];
                        values[x - 1, y] = values[x, y];
                        values[x, y] = swapInt;
                        holderX = x;
                        holderY = y;
                        Vector3 vecMove = Vector3.Lerp(new Vector3(holderX * 2, holderY * 2, 0), new Vector3((holderX * 2) - 2, (holderY * 2), 0), lerpTime);
                        blocks[x - 1, y].transform.position = vecMove;
                        //}
                    }
                    if (blocks[x - 1, y] != null)
                    {
                        //merge and move
                        if (hasMerged == false)
                        {
                            if (values[x, y] == values[x - 1, y])
                            {
                                // change scene by updating gameobject array, update and utilize backend values array
                                int holdAdd;
                                string strHold;
                                holdAdd = values[x, y] + values[x - 1, y];
                                values[x - 1, y] = holdAdd;
                                strHold = holdAdd.ToString();
                                TextMeshProUGUI txtHold = blocks[x - 1, y].GetComponentInChildren<TextMeshProUGUI>();
                                txtHold.text = strHold;
                                values[x, y] = 0;

                                Destroy(blocks[x, y].gameObject);
                                countLength--;
                            }
                            hasMerged = true;
                        }
                        if (hasMerged == true)
                        {
                            //push +1 block until next index not null

                        }


                    }
                }
            }
        }
    }

    // visual aid in scene editor
    //void OnDrawGizmos()
    //{
    //    for (int x = 0; x < 4; x++)
    //    {
    //        for (int y = 0; y < 4; y++)
    //        {
                
    //            Gizmos.DrawWireCube(new Vector2(2 * x, 2 * y), new Vector2(1.25f, 1.25f));
                
    //        }
    //    }
    //}
}
