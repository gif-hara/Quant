using UnityEngine;
using UnityEngine.Assertions;

namespace Quant
{
    /// <summary>
    /// アクターのアニメーションを制御するクラス
    /// </summary>
    public sealed class ActorAnimationController
    {
        private Actor owner;

        private Animator animator;

        private Transform cachedTransform;

        public static class Parameter
        {
            public static readonly string Forward = "Forward";

            public static readonly string Right = "Right";
        }

        public static class State
        {
            public static readonly string Idle = "Idle";

            public static readonly string Attack0 = "Attack0";

            public static string GetAttack(int id)
            {
                switch(id)
                {
                    case 0:
                        return Attack0;
                    default:
                        Assert.IsTrue(false, $"{id}は未対応の値です");
                        return "";
                }
            }
        }

        public ActorAnimationController(Actor owner, Animator animator, Transform rootTransform)
        {
            this.owner = owner;
            this.animator = animator;
            this.cachedTransform = rootTransform;
        }

        public void SetMove(Vector3 velocity)
        {
            const float dampTime = 0.1f;
            var rotation = this.cachedTransform.rotation * Quaternion.Euler(0.0f, 0.0f, 180.0f);
            velocity = rotation * velocity;
            this.animator.SetFloat(Parameter.Forward, velocity.z, dampTime, Time.deltaTime);
            this.animator.SetFloat(Parameter.Right, -velocity.x, dampTime, Time.deltaTime);
            this.animator.speed = this.owner.StatusController.MoveAnimationSpeed;
        }

        public void StartIdle()
        {
            this.animator.Play(State.Idle);
        }

        public void StartAttack(int id)
        {
            this.animator.speed = this.owner.StatusController.AttackAnimationSpeed;
            this.animator.Play(State.GetAttack(id));
        }
    }
}
