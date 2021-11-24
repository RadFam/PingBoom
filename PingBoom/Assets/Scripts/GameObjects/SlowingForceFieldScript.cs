using UnityEngine;

namespace GameObjects
{
    public class SlowingForceFieldScript : MonoBehaviour
    {
		[SerializeField]
		Collider2D myCollider;
        [SerializeField]
        PlayerMoveControl player;

        bool playerTouched;
        bool playerInside;
        // Use this for initialization
        void Start()
        {
            playerInside = false;
        }

        void OnTriggerEnter2D(Collider2D obj)
        {
            if (!playerTouched && obj.CompareTag("Player") && player.myName != "AntiSlow" && player.myName != "Lazy" && player.myName != "Gas")
            {
                playerTouched = true;
            }
        }
        void OnTriggerStay2D(Collider2D obj)
        {
            if (playerTouched)
            {
                if (!playerInside && myCollider.bounds.Contains(obj.transform.position))
                {
                    player.SetSlowing();
                    playerInside = true;
                }
                if (playerInside && !myCollider.bounds.Contains(obj.transform.position))
                {
                    player.EndSlowing();
                    playerInside = false;
                }
            }
        }
        void OnTriggerExit2D(Collider2D obj)
        {
            if (playerTouched && obj.CompareTag("Player"))
            {
                //player.EndSlowing();
                playerTouched = false;
                playerInside = false;
            }
        }
    }
}