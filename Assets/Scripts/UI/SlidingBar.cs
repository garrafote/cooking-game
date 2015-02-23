using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SlidingBar : MonoBehaviour {

    // Components
    Scrollbar ScrlBarComp;
    RectTransform RctTransComp;
	Image IconComp;
	Text TaskNameComp;

	Task _task;
	// Variables
	float currentTime;
	public Task task
	{
		get
		{
			return _task; 
		}
		set 
		{
			_task = value;
            TaskNameComp.text = _task.name;
			if (_task is TimedTask) {
				IconComp.sprite = Resources.Load<Sprite>("Sprites/UI/Clock");
			}
			else {
                IconComp.sprite = Resources.Load<Sprite>("Sprites/UI/Hand");
			}
		}
	}
	
    // Progress for the Sliding bar. Goes from 0 to 1
    public float Progress
    {
        get
        {
            return ScrlBarComp.size; 
        }
        set 
        {
            ScrlBarComp.size = value;
        }
    }

    public Vector3 Position
    {
        get
        {
            return RctTransComp.position;
        }
        set
        {
            RctTransComp.position = value;
        }
    }

    public string TaskNameTxt
    {
        get
        {
            return TaskNameComp.text;
        }
        set
        {
            TaskNameComp.text = value;
        }
    }
    
    // Use this for initialization
	void Awake ()
    {
        // Get used components
        ScrlBarComp = GetComponentInChildren<Scrollbar>();
        RctTransComp = GetComponent<RectTransform>();
        IconComp = transform.Find("Icon").GetComponent<Image>();
        TaskNameComp = transform.Find("TaskName").GetComponent<Text>();

		// Initialization
		Progress = 0.0f;
		TaskNameTxt = "DefaultName";
        
	}

    //TEST STUFF
    void Update()
    {
        if (_task is TimedTask) {
			var timedTask = _task as TimedTask;
			Progress = currentTime / timedTask.duration;
			currentTime += Time.deltaTime;

		} else { // Active
			var activeTask = _task as ActiveTask;
			Progress = ((float)activeTask.completedInputCount / (float)activeTask.totalInputCount);
			Debug.Log(Progress);

		}
    }
}
