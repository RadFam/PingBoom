using UnityEngine;

namespace GameObjects
{
    public class BoostingForceFieldScript : MonoBehaviour
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
            if (!playerTouched && obj.CompareTag("Player") && player.myName != "AntiBoost")
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
                    player.SetBoosting();
                    playerInside = true;
                }
                if (playerInside && !myCollider.bounds.Contains(obj.transform.position))
                {
                    player.EndBoosting();
                    playerInside = false;
                }
            }
        }
        void OnTriggerExit2D(Collider2D obj)
        {
            if (playerTouched && obj.CompareTag("Player"))
            {
                player.EndBoosting();
                playerTouched = false;
                playerInside = false;
            }
        }
    }
}