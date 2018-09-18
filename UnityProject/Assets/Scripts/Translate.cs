// <copyright file="Translate.cs" company="AAllard">Copyright AAllard. All rights reserved.</copyright>

using UnityEngine;

public class Translate : MonoBehaviour
{
    [SerializeField]
    private Vector3 speed;

	private void Update()
	{
	    this.transform.position += this.speed * Time.deltaTime;
	}
}
