using UnityEngine;

namespace GameObjects
{
    public class BaseObstacleScript : MonoBehaviour
    {
        [SerializeField]
        Collider2D myCollider;
        [SerializeField]
        protected Rigidbody2D myRigid;

        public void MakePassible()
        {
            myCollider.enabled = false;
        }

        public void MakeUnpassible()
        {
            myCollider.enabled = true;
        }
    }
}