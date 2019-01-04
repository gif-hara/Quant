using System;
using UniRx;
using UniRx.Triggers;
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
        private BulletStatus status;

        [SerializeField]
        private float coolTime;

        private Transform cachedTransform;

        private float currentCoolTime;

        void Awake()
        {
            this.cachedTransform = this.transform;

            this.UpdateAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.currentCoolTime -= Time.deltaTime;
                })
                .AddTo(this);
        }

        public void Fire()
        {
            if(this.currentCoolTime > 0.0f)
            {
                return;
            }

            this.currentCoolTime = this.coolTime;
            this.bullet.Spawn(
                this.cachedTransform.position,
                this.cachedTransform.rotation,
                Layers.GetBulletLayerId((Layers.Id)this.gameObject.layer),
                this.status
                );
        }
    }
}
