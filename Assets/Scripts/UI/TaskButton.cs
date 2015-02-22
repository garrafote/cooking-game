using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

[RequireComponent(typeof(Button))]
public class TaskButton : MonoBehaviour {
     
    public Task task;

    private Button button;

    private GameObject timedPrefab;
    private GameObject activePrefab;

    private Transform canvas;

    void Awake()
    {
        canvas = GameObject.Find("Canvas").transform;

        timedPrefab = Resources.Load<GameObject>("Prefabs/TaskBar");


        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
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
            

        }
        else if (task is ActiveTask)
        {
            var bar = GameObject.Instantiate(timedPrefab) as GameObject;
            bar.transform.SetParent(canvas);

            var barbar = bar.transform.GetChild(0).GetComponent<SlidingBar>();
            barbar.task = task;


            var activeTask = task as ActiveTask;
            activeTask.inputAction = activeTask.ListenButtonPress;
            activeTask.inputIdentifiers = (new[] { "Fire1", "Fire1", "Fire1" }).ToList();
            activeTask.Complete += () => Destroy(bar);

            var rt = bar.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector3(300, -50);
            rt.pivot = new Vector2(0, 0);


        }
        else
        {
            return;
        }

        Destroy(gameObject);
    }

}
