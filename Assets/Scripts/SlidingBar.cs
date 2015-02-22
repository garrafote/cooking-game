using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SlidingBar : MonoBehaviour {

    // Components
    Scrollbar ScrlBarComp;
    RectTransform RctTransComp;
	
    // Progress for the Sliding bar. Goes from 0 to 1
    public float Progress
    {
        get
        {
            return ScrlBarComp.size; 
        }
        set 
        {
            ScrlBarComp.size = value;
        }
    }


    public Vector3 Position
    {
        get
        {
            return RctTransComp.position;
        }
        set
        {
            RctTransComp.position = value;
        }
    }
    
    // Use this for initialization
	void Awake ()
    {
        // Get used components
        ScrlBarComp = GetComponent<Scrollbar>();
        RctTransComp = GetComponent<RectTransform>();
	}

    //TEST STUFF
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Progress -= 0.01f;
        if (Input.GetKeyDown(KeyCode.W)) Position = new Vector3(Position.x,Position.y+1.0f);
        if (Input.GetKeyDown(KeyCode.S)) Position = new Vector3(Position.x, Position.y - 1.0f);
        if (Input.GetKeyDown(KeyCode.A)) Position = new Vector3(Position.x - 1.0f, Position.y);
        if (Input.GetKeyDown(KeyCode.D)) Position = new Vector3(Position.x + 1.0f, Position.y);
    }
	


}
