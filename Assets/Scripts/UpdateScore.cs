using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateScore : MonoBehaviour
{
	private TMP_Text text;
	private int score = 0;

	void Awake()
	{
		text = GetComponent<TMP_Text>();
		UpdateText(0);
	}

	void UpdateText(int x)
	{
		score += x;
		text.text = score.ToString();
	}	
}
