using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameObjects
{
    public class ForceTailScript : MonoBehaviour
    {
		SpriteRenderer mySpriteRender;
		Color colorMin;
		Color colorMax;

		[SerializeField]
		float forceMin;
		[SerializeField]
		float forceMax;
        // Use this for initialization
        void Awake()
        {
			colorMin = new Color(0.45f, 0.80f, 0.30f, 1.0f);
			colorMax = new Color(0.91f, 0.45f, 0.09f, 1.0f);
			mySpriteRender = gameObject.GetComponent<SpriteRenderer>();
        }

        public void OnEnable()
		{
			transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
			transform.localScale = new Vector3(4.0f, 2.0f, 1.0f);
			mySpriteRender.color = colorMin;
		}

		public void OnDisable()
		{
			transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
			transform.localScale = new Vector3(4.0f, 2.0f, 1.0f);
			mySpriteRender.color = colorMin;
		}

		public void SetForceAngle(float force, float angle)
		{
			float currForce = Mathf.Min(force, forceMax);
			currForce = Mathf.Max(currForce, forceMin);

			transform.eulerAngles = new Vector3(0.0f, 0.0f, angle-90.0f);
			transform.localScale = new Vector3(4.0f, 2.0f+currForce, 1.0f);
			mySpriteRender.color = Color.Lerp(colorMin, colorMax, (currForce - forceMin)/(forceMax - forceMin));
		}
    }
}