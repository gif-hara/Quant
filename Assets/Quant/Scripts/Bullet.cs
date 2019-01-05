using HK.Framework;
using Quant.Events;
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

        private Rigidbody cachedRigidbody;

        private float currentLifeTime;

        public BulletStatus Status { get; private set; }

        void Awake()
        {
            this.cachedTransform = this.transform;
            this.cachedRigidbody = this.GetComponent<Rigidbody>();
            this.UpdateAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.currentLifeTime -= Time.deltaTime;
                    if (_this.currentLifeTime <= 0.0f)
                    {
                        _this.objectPool.Return(this);
                    }
                });
            this.FixedUpdateAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.cachedRigidbody.MovePosition(_this.cachedTransform.position + _this.cachedTransform.forward * _this.Status.Speed * Time.deltaTime);
                });
            this.OnTriggerEnterAsObservable()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.OnCollision(x);
                });
        }

        public Bullet Spawn(
            Vector3 position,
            Quaternion rotation,
            Layers.Id layer,
            BulletStatus status
            )
        {
            var objectPool = objectPoolBundle.Get(this);
            var original = objectPool.Rent();
            original.cachedTransform.localPosition = position;
            original.cachedTransform.localRotation = rotation;
            original.gameObject.SetLayerRecursive(layer);
            original.currentLifeTime = status.LifeTime;
            original.Status = status;
            original.objectPool = objectPool;

            return original;
        }

        private void OnCollision(Collider other)
        {
            this.objectPool.Return(this);
            var actor = other.GetComponentInParent<Actor>();
            if(actor == null)
            {
                return;
            }

            actor.Broker.Publish(CollisionedBullet.Get(this));
        }
    }
}
