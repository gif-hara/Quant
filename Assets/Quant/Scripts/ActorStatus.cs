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
        public int HitPoint => this.hitPoint;
        [SerializeField]
        private int hitPoint = 0;

        /// <summary>
        /// 移動速度
        /// </summary>
        public float MoveSpeed => this.moveSpeed;
        [SerializeField]
        private float moveSpeed = 1.0f;
    }
}
