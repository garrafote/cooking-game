using UnityEngine;
using System.Collections.Generic;

public class ActiveTask : Task 
{
	public System.Action<ActiveTask> inputAction;
	public float previousInputAngle;
	public Stack<string> InputIdentifiers { get; set; };

	public void ListenJoyStickRotation(ActiveTask task)
	{
		var xAxis = Input.GetAxis ("Horizontal");
		var yAxis = Input.GetAxis ("Vertical");
		var vector = Vector2 (xAxis, yAxis);
		previousInputAngle += Mathf.Atan2 (yAxis, xAxis) * Mathf.Rad2Deg;
		if (previousInputAngle >= 360.0f) {
			// Signal Done
		}
	}

	public void ListenButtonPress(ActiveTask task) 
	{

		if (Input.GetButtonUp (InputIdentifiers.Peek)) {
			if (InputIdentifiers.Count == 0) {
				// Signal Done
			}
			else {
				InputIdentifiers.Pop();
			}	
		}
	}
}
