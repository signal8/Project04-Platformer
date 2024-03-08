using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fades : MonoBehaviour
{
	public float fadeOutPos = -20.0f;
	public float fadeInPos = 20.0f;
	public bool isDone = false;
	private bool fadeIn = true;
	private float timer = 0.0f;

	void Update()
	{
		if (isDone == true) return;

		if (fadeIn == true)
		{
			transform.localPosition = new Vector3(Mathf.Lerp(
						transform.localPosition.x, 
						fadeOutPos, timer), 0, 1);
			timer += Time.deltaTime / 2;
			if (timer >= 1.0f)
			{
				fadeIn = false;
				isDone = true;
			}
		}
		else
		{
			transform.localPosition = new Vector3(Mathf.Lerp(
						transform.localPosition.x, 
						0.0f, timer), 0, 1);
			timer += Time.deltaTime / 2;
			if (timer >= 0.25f)
			{
				SceneManager.LoadScene(0);
			}
		}
	}

	public void ResetPosition()
	{
		transform.localPosition = new Vector3(fadeInPos, 0, 1);
		timer = 0.0f;
		return;
	}
}
