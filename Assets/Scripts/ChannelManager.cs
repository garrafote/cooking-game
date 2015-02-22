using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ChannelManager : MonoBehaviour 
{
	List<RectTransform> Tasks;

	public GameObject thingPrefab;

	public RectTransform FrontTask
	{
		get { return null; }
	}

	void PositionTasks()
	{
		//Go through each task button.

		//Position each one based.
		
	}

	void CreateNewTask(RectTransform newTask)
	{
		Tasks.Add(newTask);
		newTask.transform.SetParent(transform);
		newTask.transform.SetAsFirstSibling();
	}

	void Start () 
	{
		Tasks = new List<RectTransform>();
	}

	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.G))
		{
			Debug.Log("G Pressed\n");
			CreateNewTask(((GameObject)GameObject.Instantiate(thingPrefab, transform.position, Quaternion.identity)).GetComponent<RectTransform>());
		}
	}
}
