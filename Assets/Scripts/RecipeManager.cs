using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class RecipeManager : MonoBehaviour 
{
	//RectTransform of Recipe Region
	//A copy of the 'SERVE' button.

	//We have a list of channels.
		//Each channel has a list of button tasks.
	//Position the channels on top of each other.
	public List<ChannelManager> channels;

	//Listen for up/down movement. Change their selection accordingly.

	//ChannelManager
	//List of buttons
	//Get frontButton - returns first button.

	public int selectedChannel = 0;
	void PressUp()
	{
		int next = FindPrevChannel();
		if(next != -1)
		{
			selectedChannel = next;
			EventSystem.current.SetSelectedGameObject(channels[selectedChannel].FrontTask.gameObject);
		}
		/*
		if(selectedChannel <= 0)
		{
			selectedChannel = channels.Count - 1;
		}
		else
		{
			selectedChannel--;
		}*/

	}
	void PressDown()
	{
		int next = FindNextChannel();
		if (next != -1)
		{
			selectedChannel = next;
			EventSystem.current.SetSelectedGameObject(channels[selectedChannel].FrontTask.gameObject);
		}

		/*if (selectedChannel >= channels.Count - 1)
		{
			selectedChannel = 0;
		}
		else
		{
			selectedChannel++;
		}

		EventSystem.current.SetSelectedGameObject(channels[selectedChannel].FrontTask.gameObject);*/
	}

	int FindNextChannel()
	{
		int nextChannel = -1;

		for (int i = selectedChannel + 1; i < channels.Count + 3; i++)
		{
			if (i >= channels.Count)
			{
				i = 0;
			}

			nextChannel = i;
			
			if (channels[nextChannel].Tasks.Count > 0)
			{
				return nextChannel;
			}

			if(i == selectedChannel)
			{
				return -1;
			}
		}

		return nextChannel;
	}

	int FindPrevChannel()
	{
		int nextChannel = -1;

		for (int i = selectedChannel - 1; i > -200; i--)
		{
			if (i < 0)
			{
				i = channels.Count - 1;
			}

			nextChannel = i;

			if (channels[nextChannel].Tasks.Count > 0)
			{
				return nextChannel;
			}

			if (i == selectedChannel)
			{
				return -1;
			}
		}
		return nextChannel;
	}


	void Start () 
	{
	
	}

	bool upPressed = false;
	bool downPressed = false;
	void Update () 
	{

		if (Input.GetAxis("VertNav") > .4f && !upPressed)
		{
			upPressed = true;
			PressUp();
		}
		if (Input.GetAxis("VertNav") < -.4f && !downPressed)
		{
			downPressed = true;
			PressDown();
		}
		if (Input.GetAxis("VertNav") < .4f && Input.GetAxis("VertNav") > -.4f)
		{
			upPressed = false;
			downPressed = false;
		}
		//If they press up
		//PressUp();

		//PressDown();
	}
}
