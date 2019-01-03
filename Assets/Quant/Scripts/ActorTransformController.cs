using UnityEngine;
using UnityEngine.Assertions;

namespace Quant
{
    /// <summary>
    /// アクターの姿勢部分を制御するクラス
    /// </summary>
    public sealed class ActorTransformController : MonoBehaviour
    {
        private Vector3 velocity;

        private Quaternion rotate;

        private Transform cachedTransform;

        void Awake()
        {
            this.cachedTransform = this.transform;
        }

        void Update()
        {
            this.cachedTransform.localPosition += this.velocity;
            this.velocity = Vector3.zero;

            this.cachedTransform.localRotation = this.rotate;
        }

        public void Move(Vector3 velocity)
        {
            this.velocity += velocity;
        }

        public void Rotate(Quaternion rotate)
        {
            this.rotate = rotate;
        }
    }
}
