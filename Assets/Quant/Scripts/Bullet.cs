using HK.Framework;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant
{
    /// <summary>
    /// 弾丸を制御するクラス
    /// </summary>
    public sealed class Bullet : MonoBehaviour
    {
        private static readonly ObjectPoolBundle<Bullet> objectPoolBundle = new ObjectPoolBundle<Bullet>();

        private ObjectPool<Bullet> objectPool;

        private Transform cachedTransform;

        private float speed;

        private float lifeTime;

        void Awake()
        {
            this.cachedTransform = this.transform;
            this.UpdateAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.cachedTransform.localPosition += _this.cachedTransform.forward * _this.speed * Time.deltaTime;
                    _this.lifeTime -= Time.deltaTime;
                    if(_this.lifeTime <= 0.0f)
                    {
                        _this.objectPool.Return(this);
                    }
                });
        }

        public Bullet Spawn(Vector3 position, Quaternion rotation, float speed, float lifeTime)
        {
            var objectPool = objectPoolBundle.Get(this);
            var original = objectPool.Rent();
            original.cachedTransform.localPosition = position;
            original.cachedTransform.localRotation = rotation;
            original.speed = speed;
            original.lifeTime = lifeTime;
            original.objectPool = objectPool;

            return original;
        }
    }
}
