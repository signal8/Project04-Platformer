using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	private SpriteRenderer sr;

	void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		if (!sr.isVisible) Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Wall"))
		{
			Destroy(gameObject);
		}
		else if (col.CompareTag("Enemy"))
		{
			col.gameObject.SendMessage("Explotano");
			Destroy(gameObject);
		}
	}
}
