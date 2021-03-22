﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effects;
using SystemObjects;

namespace GameObjects
{
    public class ExplodeTargetMeta : MonoBehaviour
    {
		[SerializeField]
		protected int scoreCount;
		protected ExplosionEffectScript explodePart;

		public LevelManager levelManager;
        // Use this for initialization
        protected virtual void Start()
		{
			explodePart = transform.GetChild(0).GetComponent<ExplosionEffectScript>();
		}

		protected virtual void SetItselfInvisible()
		{

		}
		protected void SetExplosion()
		{
			explodePart.SetVisible();
		}
    }
}