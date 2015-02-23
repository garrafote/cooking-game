using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class SlidingBar : MonoBehaviour {

    // Components
    Scrollbar ScrlBarComp;
    RectTransform RctTransComp;
	Image IconComp;
	Text TaskNameComp;
    Text InstructionsComp;

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

    public string InstructionTxt
    {
        get
        {
            return InstructionsComp.text;
        }
        set
        {
            InstructionsComp.text = value;
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
        InstructionsComp = transform.Find("Instruction").GetComponent<Text>();

		// Initialization
		Progress = 0.0f;
		TaskNameTxt = "DefaultName";
        InstructionTxt = "DefaultInstruction";
        
	}

    //TEST STUFF
    void Update()
    {
        if (_task is TimedTask)
        { 
            //Passive Task
			var timedTask = _task as TimedTask;
			Progress = currentTime / timedTask.duration;
			currentTime += Time.deltaTime;

		}
        else 
        { 
            // Active Task

			var activeTask = _task as ActiveTask;
            if (activeTask.actionType == ActionType.Smash)
            {
                Progress = ((float)activeTask.completedInputCount / (float)activeTask.totalInputCount);
                InstructionTxt = string.Concat("Press ", activeTask.inputIdentifiers.First());
            }
            else
            {
                Progress = ((float)activeTask.currentAngularDisplacement / (float)activeTask.totalAngularDisplacment);
                InstructionTxt = "ROTATE LEFT JOYSTICK!!!!";
            }
            
            
			Debug.Log(Progress);
		}
    }
}
