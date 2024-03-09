using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePoint : MonoBehaviour
{
	public int score = 200;
	private GameObject scoreDisplay;

	void Start()
	{
		scoreDisplay = GameObject.Find("Score");
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (!col.gameObject.CompareTag("Player")) return;
		scoreDisplay.SendMessage("UpdateText", score);
		Destroy(gameObject);
	}
}
