using System.Collections;
using UnityEngine;
using SystemObjects;

namespace GameObjects
{
    public class CatchingGloveScript : MonoBehaviour
    {
		[SerializeField]
		Collider2D myCollider;
		[SerializeField]
		PlayerMoveControl playerMoveControl;
		bool canCatch;
		bool isGrown;

		bool hasPlayerTouched;
        bool hasPlayerContain;
        // Use this for initialization

		void Awake()
		{
			canCatch = false;
			isGrown = false;

			hasPlayerTouched = false;
        	hasPlayerContain = false;
			Debug.Log("Awake made");
		}
        void Start()
        {
			StartCoroutine(StartAppear());
        }

        IEnumerator StartAppear()
		{
			float selfSize = 0.2f;
			while (selfSize < 1.0f)
			{
				selfSize += 0.05f;
				gameObject.transform.localScale = new Vector3(selfSize, selfSize, 1.0f);
				yield return new WaitForEndOfFrame();
			}

			gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			isGrown = true;
			yield return null;
		}

		IEnumerator Disappear()
		{
			yield return new WaitForSeconds(0.5f);
			Destroy(gameObject);
		}

		public void SetPlayerCtrl(PlayerMoveControl pmc)
		{
			Debug.Log("SetPlayerCtrl invoked");
			playerMoveControl = pmc;
			canCatch = true;
		}

		void OnTriggerEnter2D(Collider2D col)
		{
			Debug.Log("hasPlayerTouched: " + hasPlayerTouched + "  isGrown: " + isGrown + "  canCatch: " + canCatch);
			if (!hasPlayerTouched && isGrown && canCatch)
			{
				if (col.CompareTag("Player"))
				{
					hasPlayerTouched = true;
					Debug.Log("Glove was touched");
				}
			}
		}

		void OnTriggerStay2D(Collider2D col)
		{
			Debug.Log("STAY hasPlayerTouched: " + hasPlayerTouched + "  isGrown: " + isGrown + "  canCatch: " + canCatch);
			if (hasPlayerTouched && isGrown && canCatch)
			{
				if (col.CompareTag("Player") && myCollider.bounds.Contains(col.transform.position))
				{
					Debug.Log("Glove mast work. Must work...");
					hasPlayerTouched = false;
					canCatch = false;
					playerMoveControl.EmergencyStop();
					StartCoroutine(Disappear());
				}
			}
		}

		void OnTriggerExit2D(Collider2D col)
		{
			if (hasPlayerTouched && isGrown && canCatch)
			{
				if (col.CompareTag("Player"))
				{
					hasPlayerTouched = false;
					Debug.Log("Glove was detouched");
				}
			}
		}
    }
}