using System;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant
{
    /// <summary>
    /// <see cref="Muzzle"/>を一定間隔で発射するクラス
    /// </summary>
    public sealed class FireMuzzleInterval : MonoBehaviour
    {
        [SerializeField]
        private Muzzle[] muzzles;

        [SerializeField]
        private float waitTime = 0.0f;

        [SerializeField]
        private float interval = 1.0f;

        [SerializeField]
        private int count = 1;

        [SerializeField]
        private TaskMonoBehavior[] completeTasks = null;

        private void Start()
        {
            this.StartFireInterval();
        }

        private void StartFireInterval()
        {
            Observable.Timer(TimeSpan.FromSeconds(this.waitTime))
                .SubscribeWithState(this, (_, _this) => _this.Fire())
                .AddTo(this);
        }

        private void Fire()
        {
            foreach(var m in this.muzzles)
            {
                m.Fire();
            }

            this.count--;
            if(this.count > 0)
            {
                Observable.Timer(TimeSpan.FromSeconds(this.interval))
                    .SubscribeWithState(this, (_, _this) => _this.Fire())
                    .AddTo(this);
            }
            else
            {
                foreach(var t in this.completeTasks)
                {
                    t.Invoke();
                }
            }
        }
    }
}
