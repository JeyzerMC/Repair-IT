using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public float Progress { get { return barProgressScale.x; } set { barProgressScale.x = Mathf.Clamp01(value); } }

    private RectTransform _bar;
    private Vector3 barProgressScale = new Vector3(0, 1, 1);

    // Start is called before the first frame update
    void Start()
    {
        _bar = GetComponent<RectTransform>();
        _bar.localScale = barProgressScale;
    }

    void Update()
    {
        _bar.localScale = barProgressScale;

        //Debug.LogError(Progress + " ==> " + barProgressScale.x +  " ==> " + _bar.localScale.x);
    }
        //    if (Progress < 1.0f)
        //    {
        //        Progress += 0.1f * Time.deltaTime;
        //        barProgressScale.x = Progress;
        //    }
        //}
    }
