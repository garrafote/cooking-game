using UnityEngine;
using System.Collections.Generic;

public class ActiveTask : Task 
{
	public System.Action<ActiveTask> inputAction;
	public float previousInputAngle;
	public Stack<string> inputIdentifiers;

	public void ListenJoyStickRotation(ActiveTask task)
	{
		var xAxis = Input.GetAxis ("Horizontal");
		var yAxis = Input.GetAxis ("Vertical");
		previousInputAngle += Mathf.Atan2 (yAxis, xAxis) * Mathf.Rad2Deg;
		if (previousInputAngle >= 360.0f) {
			// Signal Done
		}
	}

	public void ListenButtonPress(ActiveTask task) 
	{

		if (Input.GetButtonUp (inputIdentifiers.Peek())) {
			if (inputIdentifiers.Count == 0) {
				// Signal Done
			}
			else {
				inputIdentifiers.Pop();
			}	
		}
	}
}
