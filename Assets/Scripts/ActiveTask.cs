using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ActiveTask : Task 
{
	public System.Predicate<ActiveTask> inputAction;
	private float previousInputAngle;
    public float totalAngularDisplacment;
    private float currentAngularDisplacement;
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

        var fuckYou = false;
        if (xAxis == 0 && yAxis == 0)
            return fuckYou;

		var currentInputAngle = Mathf.Atan2 (yAxis, xAxis) * Mathf.Rad2Deg;
        if (currentInputAngle < 0)
        {
            currentInputAngle += 360.0f;
        }

        var delta = currentInputAngle - previousInputAngle; // Assuming positive rotation
        
        
        currentAngularDisplacement += delta; 
        previousInputAngle = currentInputAngle;
        Debug.Log("Delta" + delta);
        Debug.Log("CAD" + currentAngularDisplacement);

        if (currentAngularDisplacement >= totalAngularDisplacment) {
			
            return false;
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
