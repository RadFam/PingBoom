using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SystemObjects;

namespace GameObjects
{
    public class ScoreObjects : MonoBehaviour
    {
		[SerializeField]
		protected int scoreCount;
		[SerializeField]
		Collider2D myCollider;
		public LevelManager levelManager;
		bool isTouched;
        // Use this for initialization
        void Start()
        {
			isTouched = false;
        }

        // Update is called once per frame
        void OnTriggerEnter2D(Collider2D col)
		{
			if (!isTouched && col.gameObject.CompareTag("Player"))
			{
				//Debug.Log(gameObject.name + " is exploded");
				isTouched = true;
				levelManager.AddNewScore(scoreCount, 0);
				myCollider.enabled = false;
				StartCoroutine(DisposeStage());
			}
		}

		IEnumerator DisposeStage()
		{
			float delta = 0.05f;
			float size = 1.0f;
			while (size > 0.0f)
			{
				size -= delta;
				gameObject.transform.localScale = new Vector3(size, size, 1.0f);
				yield return new WaitForEndOfFrame();
			}

			Destroy(this.gameObject);
			yield return null;
		}
    }
}