using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DIE : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D col)
	{
		if (!col.gameObject.CompareTag("Player")) return;
		col.gameObject.SendMessage("Pause");
	}
}
