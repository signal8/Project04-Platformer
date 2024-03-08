using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalEnemy : MonoBehaviour
{
	public float moveSpeed = 3.0f;
	private ParticleSystem ps;
	private BoxCollider2D b2d;
	private SpriteRenderer sr;
	private Enemy nme;
	private bool explotano = false;
	private float timer = 0.0f;
	private Color startColor;

	void Awake()
	{
		ps = GetComponent<ParticleSystem>();
		b2d = GetComponent<BoxCollider2D>();
		sr = GetComponent<SpriteRenderer>();

		startColor = sr.color;
	}

	void Update()
	{
		if (!explotano)
		{
			transform.position += Vector3.up * moveSpeed 
				* Time.deltaTime;
			transform.rotation = Quaternion.identity;
		} 
		else 
		{
			sr.color = Color.Lerp(startColor, Color.clear, 
					timer * 2);
                	timer += Time.deltaTime;
               	 	if (timer >= 0.5f) Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		moveSpeed *= -1;
		transform.position += Vector3.up * moveSpeed * 0.01f;
	}

	public void Explotano()
	{
		ps.Play();
		b2d.enabled = false;
		explotano = true;
	}
}
