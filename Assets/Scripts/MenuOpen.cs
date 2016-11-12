using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class MenuOpen : MonoBehaviour
{

    private double width = Screen.width;
    private double height = Screen.height;
    private bool isMenuOpen = false;

    private Button button;
    public Sprite menuSprite;
    public Sprite pressedSprite;
    public Sprite pressedBackSprite;

    // Use this for initialization
    void Start()
    {
        button = GetComponent<Button>();

        double w = width * 0.08;
        double x = width * 0.05;
        double y = height * 0.913;

        GetComponent<RectTransform>().position = new Vector3((float)x, (float)y, transform.position.z);
        GetComponent<RectTransform>().sizeDelta = new Vector2((float)w, (float)w);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (isMenuOpen)
        {
           button.image.overrideSprite = menuSprite;
        }
        else
        {
            button.image.overrideSprite = null;
        }
        if (Input.GetMouseButton(0) && !isMenuOpen)
        {
            button.image.overrideSprite = pressedSprite;
        }
        else if (Input.GetMouseButton(0) && isMenuOpen)
        {
            button.image.overrideSprite = pressedBackSprite;
        }*/
    }

    public void OnMouseDown()
    {
        //sDebug.Log(width + " " + height);
        if (isMenuOpen)
        {
            isMenuOpen = !isMenuOpen;
            button.image.overrideSprite = menuSprite;
        }
        else
        {
            isMenuOpen = !isMenuOpen;
            button.image.overrideSprite = null;
        }
    }
}
