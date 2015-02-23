using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

[RequireComponent(typeof(Button))]
public class TaskButton : MonoBehaviour {
     
    public Task task;

    private Button button;


    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        transform.SetParent(null);
        GameManager.CreateSlidingBar(task);
        Destroy(gameObject);
    }

}
