using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;



public class ColorPicker : MonoBehaviour {
    bool one_click = false;
    bool timer_running;
    float timer_for_double_click;
    float delay = 0.5f;
    public Texture2D colorSpace;
	public Texture2D alphaGradient;
	public string Title = "Color Picker";
	public Vector2 startPos = new Vector2(30, 30);
	public GameObject receiver;
	public string colorSetFunctionName = "OnSetNewColor";
	public string colorGetFunctionName = "OnGetColor";
	public bool useExternalDrawer = false;
	public int drawOrder = 0;
    int clc = 0;
	private Color TempColor; 
	private Color SelectedColor;
   
    static ColorPicker activeColorPicker = null;
    public LayerMask lmask;
	int sizeFull = 200;
    bool MouseCondition=true;
	float dt = 0;

	float sizeCurr = 0;
	float alphaGradientHeight = 16;

	GUIStyle LabelStyle = null;
	int fontSize = 12;
	Color textColor = Color.black;
	Texture2D txColorDisplay;
	bool mWaitForMouseOut = false;

	string txtR, txtG, txtB, txtA;
	float valR, valG, valB, valA;
	
	public void NotifyColor(Color color)
	{
		SetColor(color);
		SelectedColor = color;
		UpdateColorEditFields(false);
		UpdateColorSliders(false);
	}

	void Start()
	{
        sizeCurr = 200;
        
		txColorDisplay = new Texture2D(1, 1, TextureFormat.ARGB32, false);
		if(receiver)
		{
			receiver.SendMessage(colorGetFunctionName, this, SendMessageOptions.DontRequireReceiver);
		}
	}


	void OnGUI()
	{
		if(!useExternalDrawer)
		{
			_DrawGUI();
		}
	}

	void UpdateColorSliders(bool isFocused)
	{
		if(!isFocused)
		{
			valR = TempColor.r;
			valG = TempColor.g;
			valB = TempColor.b;
			valA = TempColor.a;
		}
		else
		{
			SetColor(new Color(valR, valG, valB, valA));
		}
	}
    void Update()
    {
     
        
            RaycastHit vHit = new RaycastHit();
            Ray vRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        
            if (Physics.Raycast(vRay, out vHit, 1000,lmask) )
            {
            if (Input.GetMouseButtonDown(0))
            {
                
                if (!one_click) // first click no previous clicks
                {
                    one_click = true;

                    timer_for_double_click = Time.time; // save the current time
                                                        // do one click things;
                }
                else
                {
                    one_click = false; // found a double click, now reset
                    receiver = vHit.transform.gameObject;
                    clc = 1;
                    //do double click things;
                }
            }
            if (one_click)
            {
                // if the time now is delay seconds more than when the first click started. 
                if (Time.time - timer_for_double_click > delay)
                {

                    //basically if thats true its been too long and we want to reset so the next click is simply a single click and not a double click.
                    one_click = false;
                }
            }
        }
        
    }
      

void UpdateColorEditFields(bool isFocused)
	{
		if(!isFocused)
		{
			txtR = (255 * TempColor.r).ToString();
			txtG = (255 * TempColor.g).ToString();
			txtB = (255 * TempColor.b).ToString();
			txtA = (255 * TempColor.a).ToString();
		}
		else
		{
			byte r = 0;
			byte g = 0;
			byte b = 0;
			byte a = 0;
			if(!string.IsNullOrEmpty(txtR)) {
				r = byte.Parse(txtR, System.Globalization.NumberStyles.Any);
			}
			if(!string.IsNullOrEmpty(txtG)) {
				g = byte.Parse(txtG, System.Globalization.NumberStyles.Any);
			}
			if(!string.IsNullOrEmpty(txtB)) {
				b = byte.Parse(txtB, System.Globalization.NumberStyles.Any);
			}
			if(!string.IsNullOrEmpty(txtA)) {
				a = byte.Parse(txtA, System.Globalization.NumberStyles.Any);
			}
			SetColor(new Color32(r, g, b, a));
		}
	}

	// Update is called once per frame
	public void _DrawGUI () 
	{
        if (clc == 1)
        {

            GUIStyle titleStyle = new GUIStyle(GUI.skin.label);
            titleStyle.normal.textColor = textColor;

            Rect rectColorEdit = new Rect(startPos.x + sizeCurr + 10, startPos.y + 30, 40, 140);
            Rect rectColorSlider = new Rect(startPos.x + sizeCurr + 50, startPos.y + 30, 60, 140);

            GUI.Label(new Rect(startPos.x + sizeCurr + 60, startPos.y, 200, 30), Title, titleStyle);

            GUI.DrawTexture(new Rect(startPos.x + sizeCurr + 10, startPos.y, 40, 20), txColorDisplay);

            
                txtR = GUI.TextField(new Rect(startPos.x + sizeCurr + 10, startPos.y + 30, 40, 20), txtR, 3);
                txtG = GUI.TextField(new Rect(startPos.x + sizeCurr + 10, startPos.y + 60, 40, 20), txtG, 3);
                txtB = GUI.TextField(new Rect(startPos.x + sizeCurr + 10, startPos.y + 90, 40, 20), txtB, 3);
                txtA = GUI.TextField(new Rect(startPos.x + sizeCurr + 10, startPos.y + 120, 40, 20), txtA, 3);
                valR = GUI.HorizontalSlider(new Rect(startPos.x + sizeCurr + 50, startPos.y + 35, 60, 20), valR, 0.0f, 1.0f);
                valG = GUI.HorizontalSlider(new Rect(startPos.x + sizeCurr + 50, startPos.y + 65, 60, 20), valG, 0.0f, 1.0f);
                valB = GUI.HorizontalSlider(new Rect(startPos.x + sizeCurr + 50, startPos.y + 95, 60, 20), valB, 0.0f, 1.0f);
                valA = GUI.HorizontalSlider(new Rect(startPos.x + sizeCurr + 50, startPos.y + 125, 60, 20), valA, 0.0f, 1.0f);
               

            GUIStyle labelStyleRGBA = new GUIStyle(GUI.skin.label);
            labelStyleRGBA.normal.textColor = Color.white;
            GUI.Label(new Rect(startPos.x + sizeCurr + 110, startPos.y + 30, 20, 20), "R", labelStyleRGBA);
            GUI.Label(new Rect(startPos.x + sizeCurr + 110, startPos.y + 60, 20, 20), "G", labelStyleRGBA);
            GUI.Label(new Rect(startPos.x + sizeCurr + 110, startPos.y + 90, 20, 20), "B", labelStyleRGBA);
            GUI.Label(new Rect(startPos.x + sizeCurr + 110, startPos.y + 120, 20, 20), "A", labelStyleRGBA);

            //draw color picker
            Rect rect = new Rect(startPos.x, startPos.y, sizeCurr, sizeCurr);
            GUI.DrawTexture(rect, colorSpace);

            float alphaGradHeight = alphaGradientHeight * (sizeCurr / sizeFull);
            Vector2 startPosAlpha = startPos + new Vector2(0, sizeCurr);
            Rect rectAlpha = new Rect(startPosAlpha.x, startPosAlpha.y, sizeCurr, alphaGradHeight);
            GUI.DrawTexture(rectAlpha, alphaGradient);

            Rect rectFullSize = new Rect(startPos.x, startPos.y, sizeCurr, sizeCurr + alphaGradHeight);
           
            Vector2 mousePos = Event.current.mousePosition;
            Event e = Event.current;
            if (rectFullSize.Contains(e.mousePosition))
                MouseCondition = false;
            else
                MouseCondition = true;
            bool isLeftMBtnClicked = e.type == EventType.mouseUp;
            bool isLeftMBtnDragging = e.type == EventType.MouseDrag;
            bool isMouseMoving = Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0;
            bool openCondition = (rectFullSize.Contains(e.mousePosition) && (((e.type == EventType.MouseUp || e.type == EventType.mouseDrag || e.type == EventType.MouseMove) && e.isMouse)));
            bool closeCondition = (!rectFullSize.Contains(e.mousePosition)) && (e.isMouse && (e.type == EventType.MouseMove || e.type == EventType.MouseDown));
            if (openCondition && (activeColorPicker == null))
            {
                
                    activeColorPicker = this;
                    dt = 0;
                
            }
            if (closeCondition)
            {
                
                    if (isLeftMBtnClicked)
                    {
                        ApplyColor();
                    }
                    else
                    {
                        SetColor(SelectedColor);
                    }

                clc = 0;
                
            }
            
                if (rect.Contains(e.mousePosition))
                {
                    float coeffX = colorSpace.width / sizeCurr;
                    float coeffY = colorSpace.height / sizeCurr;
                    Vector2 localImagePos = (mousePos - startPos);
                    Color res = colorSpace.GetPixel((int)(coeffX * localImagePos.x), colorSpace.height - (int)(coeffY * localImagePos.y) - 1);
                    SetColor(res);
                    if (isLeftMBtnDragging)
                    {
                        ApplyColor();
                    }
                    UpdateColorEditFields(false);
                    UpdateColorSliders(false);
                }
                else if (rectAlpha.Contains(e.mousePosition))
                {
                    float coeffX = alphaGradient.width / sizeCurr;
                    float coeffY = alphaGradient.height / sizeCurr;
                    Vector2 localImagePos = (mousePos - startPosAlpha);
                    Color res = alphaGradient.GetPixel((int)(coeffX * localImagePos.x), colorSpace.height - (int)(coeffY * localImagePos.y) - 1);
                    Color curr = GetColor();
                    curr.a = res.r;
                    SetColor(curr);
                    if (isLeftMBtnDragging)
                    {
                        ApplyColor();
                    }
                    UpdateColorEditFields(false);
                    UpdateColorSliders(false);
                }
                else if (rectColorEdit.Contains(e.mousePosition))
                {
                    UpdateColorEditFields(true);
                    UpdateColorSliders(false);
                }
                else if (rectColorSlider.Contains(e.mousePosition))
                {
                    UpdateColorEditFields(false);
                    UpdateColorSliders(true);
                }
                else
                {
                    SetColor(SelectedColor);

                }
          
        }
	}

	public void SetColor(Color color)
	{
		TempColor = color;
		if(txColorDisplay != null)
		{
			txColorDisplay.SetPixel(0, 0, color);
			txColorDisplay.Apply();
		}
	}

	public Color GetColor()
	{
		return TempColor;
	}

	public void SetTitle(string title, int fontSize, Color textColor)
	{
		this.Title = title;
		this.textColor = textColor;
		this.fontSize = fontSize;
	}

	public void ApplyColor()
	{
		SelectedColor = TempColor;
		if(receiver)
		{
			receiver.SendMessage(colorSetFunctionName, SelectedColor, SendMessageOptions.DontRequireReceiver);
		}
	}
}
