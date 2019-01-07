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
    public sealed class ModelPool : MonoBehaviour
    {
        private readonly ObjectPoolBundle<ModelPool> objectPoolBundle = new ObjectPoolBundle<ModelPool>();

        public ModelPool Spawn(Vector3 position, Quaternion rotation)
        {
            var objectPool = objectPoolBundle.Get(this);
            var clone = objectPool.Rent();
            clone.transform.position = position;
            clone.transform.rotation = rotation;

            return clone;
        }
    }
}
