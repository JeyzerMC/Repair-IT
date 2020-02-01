using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // key, j1, j2, j3, j4
    [SerializeField]
    string player;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null || player == "")
        {
            Debug.LogError("Please set the player field of this PlayerInput component");
        }
        else if (player != "key" && player != "j1" && player != "j2" && player != "j3" && player != "j4")
        {
            Debug.LogError("A PlayerInput component has a player '" + player + "'. It must be one of key, j1, j2, j3 or j4");
        }
    }

    public float GetPlayerAxis(string name)
    {
        return Input.GetAxis(player + "_" + name);
    }

    public bool GetPlayerButton(string name)
    {
        return Input.GetButton(player + "_" + name);
    }
}
