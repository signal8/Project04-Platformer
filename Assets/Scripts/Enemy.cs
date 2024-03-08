using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public void UpdateExplosionAnim(SpriteRenderer sr, float timer, 
			GameObject go, Color sc)
	{
		sr.color = Color.Lerp(sc, Color.clear, timer * 2);
                timer += Time.deltaTime;
                if (timer >= 0.5f) Destroy(go);
	}
}
