using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class GameManager : MonoBehaviour {

    Task[] tasks;

    IDictionary<Task, SlidingBar> bars;

    public GameObject timedPrefab;
    public GameObject activePrefab;
    public Transform canvas;

    public static GameManager Instance { get; private set; }
    
    #region Private Methods and Unity Messages

    void Awake()
    {

        Instance = this;

        bars = new Dictionary<Task, SlidingBar>();

        // all children must contain a task component
        tasks = transform.Cast<Transform>().Select(t => t.GetComponent<Task>()).ToArray();
        foreach (var task in tasks)
        {
            task.Complete += OnTaskComplete;
        }
    }

    void OnTaskComplete(Task task)
    {
        var bar =
        bars.Remove(task);
    }

    void _CreateSlidingBar(Task task)
    {
        if (task is TimedTask)
        {
            var bar = GameObject.Instantiate(timedPrefab) as GameObject;
            bar.transform.SetParent(canvas);

            var barbar = bar.GetComponent<SlidingBar>();
            barbar.task = task;

            var rt = bar.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector3(50, -50);
            rt.pivot = new Vector2(0, 0);

            bars.Add(task, barbar);

        }
        else if (task is ActiveTask)
        {
            var bar = GameObject.Instantiate(timedPrefab) as GameObject;
            bar.transform.SetParent(canvas);

            var barbar = bar.transform.GetComponent<SlidingBar>();
            barbar.task = task;

            var activeTask = task as ActiveTask;
            activeTask.inputAction = activeTask.ListenButtonPress;
            activeTask.inputIdentifiers = (new[] { "Fire1", "Fire1", "Fire1" }).ToList();

            var rt = bar.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector3(300, -50);
            rt.pivot = new Vector2(0, 0);

            bars.Add(task, barbar);

        }
        else
        {
            return;
        }
    }

    #endregion

    #region Static Interface

    public static void CreateSlidingBar(Task task)
    {
        Instance._CreateSlidingBar(task);
    }

    #endregion
}
