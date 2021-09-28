using UnityEngine;
using GameObjects;

namespace Controls
{
    public class FieldClickController : MonoBehaviour
    {
		Camera camera;

		[SerializeField]
		PlayerMoveControl playerMoveControl;
		[SerializeField]
		CatchingGloveScript glovePrefab;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
			if (Input.GetMouseButtonUp(0))
			{
				CreateGlove();
			}
        }

		void CreateGlove()
		{
			if (playerMoveControl.IsSliding) // also check, if we previously check the glove on menu
			{
				Vector3 touchPlace = camera.ScreenToWorldPoint(Input.mousePosition);
				// Check if there is no any GO colliders
				RaycastHit2D hit = Physics2D.Raycast(camera.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
				if (hit.collider == null)
				{
					CatchingGloveScript cgs = Instantiate(glovePrefab, touchPlace, Quaternion.identity);
					cgs.SetPlayerCtrl(playerMoveControl);
				}
			}
		}
    }
}