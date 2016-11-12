using UnityEngine;
using System.Collections;

public class CameraChange : MonoBehaviour {

    private double width = Screen.width;
    private double height = Screen.height;
    private bool topDown = false;

    private float t_z = 0;
    private float t_y = 10;
    private float t_rx = 90;
	public Vector3 vec;

    // Use this for initialization
    void Start ()
    {
        double w = width * 0.08;
        double x = width * 0.95;
        double y = height * 0.913;

        GetComponent<RectTransform>().position = new Vector3((float)x, (float)y, transform.position.z);
        GetComponent<RectTransform>().sizeDelta = new Vector2((float)w, (float)w);
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void OnMouseDown()
    {
        if (topDown) {
            topDown = !topDown;
            Camera.main.gameObject.transform.position = new Vector3(Camera.main.gameObject.transform.position.x, 6, 0);
            Camera.main.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else {
            topDown = !topDown;
			Camera.main.gameObject.transform.position = vec;
			Camera.main.gameObject.transform.rotation = new Quaternion(1.0f, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        }
    }
}
