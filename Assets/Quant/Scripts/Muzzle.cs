using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant
{
    /// <summary>
    /// 銃口を制御するクラス
    /// </summary>
    public sealed class Muzzle : MonoBehaviour
    {
        [SerializeField]
        private Bullet bullet;

        [SerializeField]
        private float speed;

        [SerializeField]
        private float lifeTime;

        private Transform cachedTransform;

        void Awake()
        {
            this.cachedTransform = this.transform;
        }

        public void Fire()
        {
            this.bullet.Spawn(this.cachedTransform.position, this.cachedTransform.rotation, this.speed, this.lifeTime);
        }
    }
}
