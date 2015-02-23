using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public enum ActionType
{
    Rotate,
    Smash
}

public class ActiveTask : Task 
{
	public System.Predicate<ActiveTask> inputAction;
	private float previousInputAngle;
    public float totalAngularDisplacment;
    public float currentAngularDisplacement;
	public List<string> inputIdentifiers;
	public int totalInputCount;
	public int completedInputCount;
    public ActionType actionType;

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

        var fuckYou = false;
        if (xAxis == 0 && yAxis == 0)
            return fuckYou;

		var currentInputAngle = Mathf.Atan2 (yAxis, xAxis) * Mathf.Rad2Deg;
        if (currentInputAngle < 0)
        {
            currentInputAngle += 360.0f;
        }

        if (currentInputAngle > 90.0f && currentInputAngle < 180.0f && previousInputAngle < 90.0f)
        {
            currentAngularDisplacement += currentInputAngle;
        }
        else if (currentInputAngle > 180.0f && currentInputAngle < 270.0f && previousInputAngle > 90.0f && previousInputAngle < 180.0f)
        {
            currentAngularDisplacement += currentInputAngle;

        }
        else if (currentInputAngle > 270.0f && currentInputAngle < 360.0f && previousInputAngle > 180.0f && previousInputAngle < 270.0f)
        {
            currentAngularDisplacement += currentInputAngle;

        }
        else if (currentInputAngle > 0.0f && currentInputAngle < 90.0f && previousInputAngle > 270.0f && previousInputAngle < 360.0f)
        {
            currentAngularDisplacement += currentInputAngle;

        }

        
        previousInputAngle = currentInputAngle;
       // Debug.Log("Delta" + delta);
        Debug.Log("CAD" + currentInputAngle);

        if (currentAngularDisplacement >= totalAngularDisplacment) {
			
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
