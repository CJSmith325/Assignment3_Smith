using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void OnDrawGizmos()
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                for (int z = 0; z < 4; z++)
                {
                    Gizmos.DrawWireCube(new Vector2(2 * x, 2 * y), new Vector2(1.25f, 1.25f));
                }
            }
        }
    }
}
