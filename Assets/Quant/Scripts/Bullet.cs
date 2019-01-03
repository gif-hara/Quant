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

        private Rigidbody cachedRigidbody;

        private float speed;

        private float lifeTime;

        void Awake()
        {
            this.cachedTransform = this.transform;
            this.cachedRigidbody = this.GetComponent<Rigidbody>();
            this.UpdateAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.lifeTime -= Time.deltaTime;
                    if (_this.lifeTime <= 0.0f)
                    {
                        _this.objectPool.Return(this);
                    }
                });
            this.FixedUpdateAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.cachedRigidbody.MovePosition(_this.cachedTransform.position + _this.cachedTransform.forward * _this.speed * Time.deltaTime);
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
            float speed,
            float lifeTime,
            Layers.Id layer
            )
        {
            var objectPool = objectPoolBundle.Get(this);
            var original = objectPool.Rent();
            original.cachedTransform.localPosition = position;
            original.cachedTransform.localRotation = rotation;
            original.gameObject.SetLayerRecursive(layer);
            original.speed = speed;
            original.lifeTime = lifeTime;
            original.objectPool = objectPool;

            return original;
        }

        private void OnCollision(Collider other)
        {
            this.objectPool.Return(this);

            if(other.attachedRigidbody == null)
            {
                return;
            }

            Debug.Log(other.attachedRigidbody.name, other.attachedRigidbody);
        }
    }
}
