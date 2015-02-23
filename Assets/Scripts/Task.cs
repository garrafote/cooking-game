using UnityEngine;
using System.Collections;

public class Task : MonoBehaviour {

    public string description;
    public bool isActive;


    public event System.Action<Task> Complete;


    protected void OnComplete() {
        if (Complete != null) Complete(this);
    }
}
