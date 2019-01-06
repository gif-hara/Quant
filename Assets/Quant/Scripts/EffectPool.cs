using HK.Framework;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EffectPool : MonoBehaviour
    {
        private readonly ObjectPoolBundle<EffectPool> objectPoolBundle = new ObjectPoolBundle<EffectPool>();

        public EffectPool Spawn(Vector3 position, Quaternion rotation, float duration)
        {
            var objectPool = objectPoolBundle.Get(this);
            var clone = objectPool.Rent();
            clone.transform.position = position;
            clone.transform.rotation = rotation;
            Observable.Timer(TimeSpan.FromSeconds(duration))
                .TakeUntilDisable(this)
                .SubscribeWithState2(clone, objectPool, (_, _clone, _objectPool) => _objectPool.Return(_clone))
                .AddTo(clone);

            return clone;
        }
    }
}
