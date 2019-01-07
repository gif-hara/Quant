using UnityEngine;
using UnityEngine.Assertions;

namespace Quant
{
    /// <summary>
    /// アクターのアニメーションを制御するクラス
    /// </summary>
    public sealed class ActorAnimationController
    {
        private Animator animator;

        private Transform cachedTransform;

        public static class Parameter
        {
            public static readonly string Forward = "Forward";

            public static readonly string Right = "Right";
        }

        public ActorAnimationController(Animator animator, Transform rootTransform)
        {
            this.animator = animator;
            this.cachedTransform = rootTransform;
        }

        public void SetMove(Vector3 velocity)
        {
            var rotation = this.cachedTransform.rotation * Quaternion.Euler(0.0f, 0.0f, 180.0f);
            velocity = rotation * velocity;
            this.animator.SetFloat(Parameter.Forward, velocity.z);
            this.animator.SetFloat(Parameter.Right, -velocity.x);
        }
    }
}
