using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class GameManager : MonoBehaviour {

    List<Task> tasks;

    IDictionary<Task, SlidingBar> bars;

    public GameObject timedPrefab;
    public GameObject activePrefab;
    public Transform canvas;
    private RectTransform lastRectTransform;
    public RecipeManager recipe;

    public static GameManager Instance { get; private set; }
    
    #region Private Methods and Unity Messages

    void Awake()
    {

        Instance = this;

        bars = new Dictionary<Task, SlidingBar>();

        tasks = new List<Task>();
    }

    void Start()
    {  // all children must contain a task component
        foreach (var task in tasks)
        {
            task.Complete += OnTaskComplete;
        }

        for (var i = 0; i < transform.childCount; i++)
        {
            foreach (Transform child in transform.GetChild(i))
            {
                var task = child.GetComponent<Task>();
                task.Complete += OnTaskComplete;

                tasks.Add(task);

                recipe.channels[i].GenerateTask(task);
            }

            recipe.channels[i].PositionTasks();
        }

    }

    void OnTaskComplete(Task task)
    {
        var bar = bars[task];

        bars.Remove(task);
        bar.transform.SetParent(null);
        Destroy(bar.gameObject);


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

            rt.anchorMin = new Vector2(0.0f, 1.0f);
            rt.anchorMax = new Vector2(0.0f, 1.0f);
            
            rt.anchoredPosition = new Vector3(10, -150);
            
            rt.pivot = new Vector2(0, 0);

            if (lastRectTransform)
            {
                rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y - lastRectTransform.rect.height);
            }
            else
            {
                lastRectTransform = rt;
            }

            bars.Add(task, barbar);
        }
        else if (task is ActiveTask)
        {
            var bar = GameObject.Instantiate(timedPrefab) as GameObject;
            bar.transform.SetParent(canvas);

            var barbar = bar.transform.GetComponent<SlidingBar>();
            barbar.task = task;

            var activeTask = task as ActiveTask;
            activeTask.inputAction = (activeTask.actionType == ActionType.Smash) ? (System.Predicate<ActiveTask>)activeTask.ListenButtonPress : activeTask.ListenJoyStickRotation; 

            var rt = bar.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(1.0f, 1.0f);
            rt.anchorMax = new Vector2(1.0f, 1.0f);

            rt.anchoredPosition = new Vector3(-310, -150);
            
            rt.pivot = new Vector2(0, 0);

            bars.Add(task, barbar);
        }
        else
        {
            return;
        }

        foreach (var channel in recipe.channels)
        {
            channel.PositionTasks();
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
