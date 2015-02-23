using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class ChannelManager : MonoBehaviour 
{
	List<RectTransform> Tasks;

	public GameObject taskButtonPrefab;

	public RectTransform FrontTask
	{
		get { return Tasks[0]; }
	}

	public void PositionTasks()
	{
		//Go through each task button.
		for(int i = 0; i < Tasks.Count; i++)
		{
			if(i == 0)
			{
				Tasks[i].position = new Vector3(0, Tasks[i].position.y, Tasks[i].position.z);
				Tasks[i].position = new Vector3(Tasks[i].position.x + 150, Tasks[i].position.y, Tasks[i].position.z);
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
		}
		rt.pivot = new Vector2(0,1);
		rt.sizeDelta = new Vector2(Random.Range(80, 180), 30);

		RegisterTaskButton(rt);
	}

	void RegisterTaskButton(RectTransform newTask)
	{
		Tasks.Add(newTask);
		newTask.transform.SetParent(transform);
		newTask.transform.SetAsFirstSibling();
	}

	public void Start()
	{
		Tasks = new List<RectTransform>();
        //GenerateTask();
        //GenerateTask();
        //GenerateTask();
        //GenerateTask();

		//Then position the tasks.
		PositionTasks();
	}

	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.G))
		{
			Debug.Log("G Pressed\n");
			RegisterTaskButton(((GameObject)GameObject.Instantiate(taskButtonPrefab, transform.position, Quaternion.identity)).GetComponent<RectTransform>());
		}
	}
}
