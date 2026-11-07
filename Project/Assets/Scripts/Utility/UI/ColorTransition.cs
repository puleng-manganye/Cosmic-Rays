using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorTransition : MonoBehaviour
{
	public Text text;
	public Gradient colorTransition;
	public float speed = 3.5f;

	private void Update()
	{
		//text effect
		if (text != null && text.gameObject.activeSelf)
		{
			float t = Mathf.PingPong(Time.time * speed, 1f);
			text.color = colorTransition.Evaluate(t);
		}
	}
}
