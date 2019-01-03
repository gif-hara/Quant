using UnityEngine;
using UnityEngine.Assertions;

namespace Quant
{
    /// <summary>
    /// アクターの姿勢部分を制御するクラス
    /// </summary>
    public sealed class ActorTransformController : MonoBehaviour
    {
        private Vector3 velocity = Vector3.zero;

        private Quaternion rotate = Quaternion.identity;

        private Transform cachedTransform;

        private CharacterController characterController;

        void Awake()
        {
            this.cachedTransform = this.transform;
            this.characterController = this.GetComponent<CharacterController>();
        }

        void FixedUpdate()
        {
            this.characterController.SimpleMove(this.velocity);
            this.velocity = Vector3.zero;

            if(this.rotate != Quaternion.identity)
            {
                this.cachedTransform.localRotation = this.rotate;
                this.rotate = Quaternion.identity;
            }
        }

        public void Move(Vector3 velocity)
        {
            this.velocity += velocity;
        }

        public void Rotate(Quaternion rotate)
        {
            this.rotate = rotate;
        }

        public void RotateImmediate(Quaternion rotate)
        {
            this.cachedTransform.localRotation = rotate;
        }
    }
}
