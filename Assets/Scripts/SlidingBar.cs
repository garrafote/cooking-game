using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SlidingBar : MonoBehaviour {

    // Components
    Scrollbar ScrlBarComp;
    RectTransform RctTransComp;
    Image IconComp;
    Text TaskNameComp;
	
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

    public string TaskNameTxt
    {
        get
        {
            return TaskNameComp.text;
        }
        set
        {
            TaskNameComp.text = value;
        }
    }
    
    // Use this for initialization
	void Awake ()
    {
        // Get used components
        ScrlBarComp = GetComponentInChildren<Scrollbar>();
        RctTransComp = GetComponent<RectTransform>();
        IconComp = transform.Find("Icon").GetComponent<Image>();
        TaskNameComp = transform.Find("TaskName").GetComponent<Text>();

        // Default Task is active type
        SetTaskType(true);

        TaskNameTxt = "Bla bla bla";
        
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

    void SetTaskType(bool isActiveTask)
    {
        if (isActiveTask) IconComp.sprite = Resources.Load<Sprite>("Sprites/aRound");
        else IconComp.sprite = Resources.Load<Sprite>("Sprites/bRound");
    }
}
