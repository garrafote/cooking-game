using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class ChannelManager : MonoBehaviour 
{
	public List<RectTransform> Tasks;

	public GameObject taskButtonPrefab;

	public RectTransform FrontTask
	{
		get { return Tasks[0]; }
	}

	public void PositionTasks()
	{
        for (int i = Tasks.Count - 1; i >= 0; i--)
        {
            var task = Tasks[i];
            if (task.parent != transform)
            {
                Tasks.Remove(task);
            }
        }

		//Go through each task button.
		for(int i = 0; i < Tasks.Count; i++)
		{
			if(i == 0)
			{
				Tasks[i].position = new Vector3(0, Tasks[i].position.y, Tasks[i].position.z);
				//Tasks[i].position = new Vector3(Tasks[i].position.x + 150, Tasks[i].position.y, Tasks[i].position.z);
			}
			else
			{
				Tasks[i].position = new Vector3(Tasks[i - 1].position.x - 20 + Tasks[i - 1].rect.width, Tasks[i].position.y, Tasks[i].position.z);
			}
		}
	}

	public void GenerateTask(Task task = null)
	{
		//Make some tasks. Give them random widths
		RectTransform rt = ((GameObject)GameObject.Instantiate(taskButtonPrefab, transform.position, Quaternion.identity)).GetComponent<RectTransform>();
		if(task != null)
		{
			rt.transform.Find("Text").GetComponent<Text>().text = task.name;
			rt.GetComponent<TaskButton>().task = task;
			
			if(task is ActiveTask)
			{
				//Do activeTask stuff
				rt.sizeDelta = new Vector2(60 + 20 * ((ActiveTask)task).totalInputCount, 30);
				rt.GetComponent<Image>().color = Color.yellow;
			}
			else
			{
				rt.sizeDelta = new Vector2(75 + 5 * ((TimedTask)task).duration, 30);
				rt.GetComponent<Image>().color = Color.green;

			}

		}
		else
		{
			rt.pivot = new Vector2(0,1);
			rt.sizeDelta = new Vector2(Random.Range(80, 180), 30);
		}
		RegisterTaskButton(rt);
	}

	void RegisterTaskButton(RectTransform newTask)
	{
		Tasks.Add(newTask);
		newTask.transform.SetParent(transform);
		newTask.transform.SetAsFirstSibling();
	}

	public void Awake()
	{
		Tasks = new List<RectTransform>();
	}

	void Update () 
	{
	}
}
