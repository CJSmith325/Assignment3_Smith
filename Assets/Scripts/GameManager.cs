using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //array for values
    GameObject[,] tileValues = new GameObject[4, 4];
    public GameObject gridPiece;
    private void Start()
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {

                Instantiate(gridPiece);

            }
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
