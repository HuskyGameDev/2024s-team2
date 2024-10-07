using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    public bool callOnStart;
    // Start is called before the first frame update
    void Start()
    {
        if (callOnStart)
        {
            DestroyIn(2.5f);
        }
    }

    public void DestroyIn(float timeToDestroy)
    {
        StartCoroutine(CountDown(timeToDestroy));
    }

    IEnumerator CountDown(float value)
    {
        float normalizedTime = 0;
        while(normalizedTime <= 1)
        {
            normalizedTime += Time.deltaTime / value;
            yield return null;
        }
        Destroy(gameObject);
    }
}
