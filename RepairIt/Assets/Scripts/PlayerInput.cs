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
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GetPlayerAxis("Horizontal"));
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
