using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour
{

		public int damping;

		private Boolean isMove = false;

		private int playerX;
		private int playerY;

		void Start ()
		{
				
		}

		void Update ()
		{
				//transform.position = Vector3.Lerp (position1, position2, Time.deltaTime * damping);
		}
}