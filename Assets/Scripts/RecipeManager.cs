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
		if(selectedChannel < 0)
		{
			selectedChannel = channels.Count;
		}
		else
		{
			selectedChannel--;
		}

		EventSystem.current.SetSelectedGameObject(channels[selectedChannel].FrontTask.gameObject);
	}

	void Start () 
	{
	
	}
	
	void Update () 
	{
		//If they press up
		//PressUp();

		//PressDown();
	}
}
