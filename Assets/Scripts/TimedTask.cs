using UnityEngine;
using System.Collections;

public class TimedTask : Task {

    public float duration;
    public float progress;

    void Update()
    {
        if (progress == 1)
        {
            OnComplete();
        }
    }

}
