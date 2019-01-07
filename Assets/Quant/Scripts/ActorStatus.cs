using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant
{
    /// <summary>
    /// アクターのステータス
    /// </summary>
    [CreateAssetMenu(menuName = "Quant/ActorStatus")]
    public sealed class ActorStatus : ScriptableObject
    {
        /// <summary>
        /// ヒットポイント
        /// </summary>
        public int HitPoint { get { return this.hitPoint; } set { this.hitPoint = value; } }
        [SerializeField]
        private int hitPoint = 0;

        /// <summary>
        /// 移動速度
        /// </summary>
        public float MoveSpeed { get { return this.moveSpeed; } set { this.moveSpeed = value; } }
        [SerializeField]
        private float moveSpeed = 1.0f;

        public EffectPool DeadEffect => this.deadEffect;
        [SerializeField]
        private EffectPool deadEffect = null;

        public float MoveAnimationSpeed => this.moveAnimationSpeed;
        [SerializeField]
        private float moveAnimationSpeed = 1.0f;

        public float AttackAnimationSpeed => this.attackAnimationSpeed;
        [SerializeField]
        private float attackAnimationSpeed = 1.0f;

        public ActorStatus Clone()
        {
            var clone = CreateInstance<ActorStatus>();
            clone.hitPoint = this.hitPoint;
            clone.moveSpeed = this.moveSpeed;
            clone.deadEffect = this.deadEffect;
            clone.moveAnimationSpeed = this.moveAnimationSpeed;
            clone.attackAnimationSpeed = this.attackAnimationSpeed;

            return clone;
        }
    }
}
