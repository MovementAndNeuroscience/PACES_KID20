using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TNVirtualKeyboard : MonoBehaviour
{
	
	public static TNVirtualKeyboard instance;
	
	public string words = "";
	public bool enter = false;
	private string key = "";
	
	public GameObject vkCanvas;
	
	public GameObject inputField;

	public TMP_InputField targetText;

	public GameObject a; 
	public GameObject b;
	public GameObject c;
	public GameObject d;
	public GameObject e;
	public GameObject f;
	public GameObject g;
	public GameObject h;
	public GameObject i;
	public GameObject j;
	public GameObject k;
	public GameObject l;
	public GameObject m;
	public GameObject n;
	public GameObject o;
	public GameObject p;
	public GameObject q;
	public GameObject r;
	public GameObject s;
	public GameObject t;
	public GameObject u;
	public GameObject v;
	public GameObject w;
	public GameObject x;
	public GameObject y;
	public GameObject z;
	public GameObject ae;
	public GameObject oe;
	public GameObject aa;
	public GameObject space;
	public GameObject back;
	public GameObject hide;
	public GameObject Enter;
	public GameObject _1;
	public GameObject _2;
	public GameObject _3;
	public GameObject _4;
	public GameObject _5;
	public GameObject _6;
	public GameObject _7;
	public GameObject _8;
	public GameObject _9;
	public GameObject _0;
	public GameObject keyPanel;
	public GameObject shadow_L;
	public GameObject shadow_D;




	// Start is called before the first frame update
	void Start()
    {
		targetText = inputField.GetComponent<TMP_InputField>();

		instance = this;
		HideVirtualKeyboard();
		
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void KeyPress(string k){
		words += k;
		targetText.text = words;
		SetKey(k);
	}
	
	public void Del(){
		words = words.Remove(words.Length - 1, 1);
		targetText.text = words;	
	}
    public void EnableEnterMode()
    {
		vkCanvas.SetActive(true);
		keyPanel.SetActive(true);
		_1.SetActive(false);
		_2.SetActive(false);
		_3.SetActive(false);
		_4.SetActive(false);
		_5.SetActive(false);
		_6.SetActive(false);
		_7.SetActive(false);
		_8.SetActive(false);
		_9.SetActive(false);
		_0.SetActive(false);
		a.SetActive(false);
		b.SetActive(false);
		c.SetActive(false);
		d.SetActive(false);
		e.SetActive(false);
		f.SetActive(false);
		g.SetActive(false);
		h.SetActive(false);
		i.SetActive(false);
		j.SetActive(false);
		k.SetActive(false);
		l.SetActive(false);
		m.SetActive(false);
		n.SetActive(false);
		o.SetActive(false);
		p.SetActive(false);
		q.SetActive(false);
		r.SetActive(false);
		s.SetActive(false);
		t.SetActive(false);
		u.SetActive(false);
		v.SetActive(false);
		w.SetActive(false);
		x.SetActive(false);
		y.SetActive(false);
		z.SetActive(false);
		ae.SetActive(false);
		oe.SetActive(false);
		aa.SetActive(false);
		space.SetActive(false);
		back.SetActive(false);
		hide.SetActive(false);
		Enter.SetActive(true);
		Enter.GetComponent<RectTransform>().localPosition = new Vector3(655f, -5f, -1.02f);
		Enter.GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1.1f);
		keyPanel.GetComponent<Image>().color = new Color(255, 255, 255, 0);
		shadow_D.SetActive(false);
		shadow_L.SetActive(false);
	}
    public void ActivateGameMode()
    {
		vkCanvas.SetActive(true);
		keyPanel.SetActive(true);
		_1.SetActive(false);
		_2.SetActive(false);
		_3.SetActive(false);
		_4.SetActive(false);
		_5.SetActive(false);
		_6.SetActive(false);
		_7.SetActive(false);
		_8.SetActive(false);
		_9.SetActive(false);
		_0.SetActive(false);
		a.SetActive(false);
		b.SetActive(false);
		c.SetActive(false);
		d.SetActive(false);
		e.SetActive(false);
		f.SetActive(false);
		g.SetActive(true);
		h.SetActive(false);
		i.SetActive(false);
		j.SetActive(false);
		k.SetActive(false);
		l.SetActive(false);
		m.SetActive(false);
		n.SetActive(false);
		o.SetActive(false);
		p.SetActive(false);
		q.SetActive(false);
		r.SetActive(false);
		s.SetActive(false);
		t.SetActive(false);
		u.SetActive(false);
		v.SetActive(false);
		w.SetActive(false);
		x.SetActive(false);
		y.SetActive(false);
		z.SetActive(false);
		ae.SetActive(false);
		oe.SetActive(false);
		aa.SetActive(false);
		space.SetActive(false);
		back.SetActive(false);
		hide.SetActive(false);
		Enter.SetActive(false);
		//shadow_L.SetActive(true);
		//shadow_D.SetActive(true);
		g.GetComponent<RectTransform>().localPosition = new Vector3(530, 250, -0f);
		g.GetComponent<RectTransform>().localScale = new Vector3(2.4f, 2.4f, 2);
		//shadow_L.GetComponent<RectTransform>().localPosition = new Vector3(530, 250, -0f);
		//shadow_L.GetComponent<RectTransform>().localScale = new Vector3(2.4f, 2.4f, 2);
		//d.GetComponent<RectTransform>().localPosition = new Vector3(-530, 250, -0f);
		//d.GetComponent<RectTransform>().localScale = new Vector3(2.4f, 2.4f, 2);
		//shadow_D.GetComponent<RectTransform>().localPosition = new Vector3(-530, 250, -0f);
		//shadow_D.GetComponent<RectTransform>().localScale = new Vector3(2.4f, 2.4f, 2);
	}
	public void ContinueFromTrainingMode()
	{
		vkCanvas.SetActive(true);
		keyPanel.SetActive(true);
		_1.SetActive(false);
		_2.SetActive(false);
		_3.SetActive(false);
		_4.SetActive(false);
		_5.SetActive(false);
		_6.SetActive(false);
		_7.SetActive(false);
		_8.SetActive(false);
		_9.SetActive(false);
		_0.SetActive(false);
		a.SetActive(false);
		b.SetActive(false);
		c.SetActive(false);
		d.SetActive(false);
		e.SetActive(false);
		f.SetActive(false);
		g.SetActive(true);
		h.SetActive(false);
		i.SetActive(false);
		j.SetActive(false);
		k.SetActive(false);
		l.SetActive(false);
		m.SetActive(false);
		n.SetActive(true);
		o.SetActive(false);
		p.SetActive(false);
		q.SetActive(false);
		r.SetActive(false);
		s.SetActive(false);
		t.SetActive(false);
		u.SetActive(false);
		v.SetActive(false);
		w.SetActive(false);
		x.SetActive(false);
		y.SetActive(false);
		z.SetActive(false);
		ae.SetActive(false);
		oe.SetActive(false);
		aa.SetActive(false);
		space.SetActive(false);
		back.SetActive(false);
		hide.SetActive(false);
		Enter.SetActive(false);
		//shadow_L.SetActive(true);
		//shadow_D.SetActive(true);
		g.GetComponent<RectTransform>().localPosition = new Vector3(530, 250, -0f);
		g.GetComponent<RectTransform>().localScale = new Vector3(2.4f, 2.4f, 2);
		//shadow_L.GetComponent<RectTransform>().localPosition = new Vector3(530, 250, -0f);
		//shadow_L.GetComponent<RectTransform>().localScale = new Vector3(2.4f, 2.4f, 2);
		n.GetComponent<RectTransform>().localPosition = new Vector3(-530, 250, -0f);
		n.GetComponent<RectTransform>().localScale = new Vector3(2.4f, 2.4f, 2);
		//shadow_D.GetComponent<RectTransform>().localPosition = new Vector3(-530, 250, -0f);
		//shadow_D.GetComponent<RectTransform>().localScale = new Vector3(2.4f, 2.4f, 2);
	}
	public void ShowVirtualKeyboard(){
		vkCanvas.SetActive(true);
	}
	
	public void HideVirtualKeyboard(){
		vkCanvas.SetActive(false);
	}
	public void HideVirtualKeyboardPanel()
    {
		keyPanel.SetActive(false);

	}
	public void SubmitAnswer()
    {
		EnterPressed();
		HideVirtualKeyboard();
	}
	public void EnterPressed()
    {
		enter = true;
    }
    public void EnterReleased()
	{
		enter = false;
    }
	public bool GetEnter()
    {
		return enter;
    }
	public string GetKey()
    {
		return key;
    }
	public void SetKey(string k )
    {
		key = k; 
    }

}
