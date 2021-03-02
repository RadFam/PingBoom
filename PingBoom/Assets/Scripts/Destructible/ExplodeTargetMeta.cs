using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effects;

namespace GameObjects
{
    public class ExplodeTargetMeta : MonoBehaviour
    {
		protected ExplosionEffectScript explodePart;
        // Use this for initialization
        protected virtual void Start()
		{
			Debug.Log("Parent Start() has performed");
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