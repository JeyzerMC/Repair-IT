using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionSystem : MonoBehaviour
{

    private float _progression = 0.0f;

    private RectTransform _bar;

    // Start is called before the first frame update
    void Start()
    {
        _bar = GetComponent<RectTransform>();
        _bar.localScale = new Vector3(Time.time, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time < 3.0f) 
        {
            _bar.localScale = new Vector3(Time.time / 3.0f, 1, 1);
        }
    }
}
