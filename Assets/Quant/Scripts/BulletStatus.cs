using UnityEngine;
using UnityEngine.Assertions;

namespace Quant
{
    /// <summary>
    /// 弾のステータス
    /// </summary>
    [CreateAssetMenu(menuName = "Quant/BulletStatus")]
    public sealed class BulletStatus : ScriptableObject
    {
        public float Speed => this.speed;
        [SerializeField]
        private float speed;

        public float LifeTime => this.lifeTime;
        [SerializeField]
        private float lifeTime;

        public int Power => this.power;
        [SerializeField]
        private int power;
    }
}
