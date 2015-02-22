using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ActiveTask : Task 
{
	public System.Predicate<ActiveTask> inputAction;
	public float previousInputAngle;
	public List<string> inputIdentifiers;
	public int totalInputCount;
	public int completedInputCount;


    void Update()
    {

        if (inputAction == null)
            return;

        var done = inputAction(this);

        if (done)
        {
            OnComplete();
            inputAction = null;
        }

    }



	public bool ListenJoyStickRotation(ActiveTask task)
	{
		var xAxis = Input.GetAxis ("Horizontal");
		var yAxis = Input.GetAxis ("Vertical");
		previousInputAngle += Mathf.Atan2 (yAxis, xAxis) * Mathf.Rad2Deg;
		if (previousInputAngle >= 360.0f) {
			// Signal Done
            return true;
		}

        return false;
	}

	public bool ListenButtonPress(ActiveTask task) 
	{

		if (Input.GetButtonUp (inputIdentifiers.First())) {
            
            inputIdentifiers.RemoveAt(0);
			completedInputCount++;

            if (inputIdentifiers.Count == 0)
            {
                return true;
            }
        }
        

        return false;
	}
}
