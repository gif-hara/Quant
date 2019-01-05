using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant
{
    /// <summary>
    /// カメラのプロパティを上書きするクラス
    /// </summary>
    [Serializable]
    public sealed class OverrideCameraProperty
    {
        [SerializeField]
        private TransformProperty lookAt = new TransformProperty();
        public TransformProperty LookAt => this.lookAt;

        [SerializeField]
        private Vector3Property pivot = new Vector3Property();
        public Vector3Property Pivot => this.pivot;

        [SerializeField]
        private FloatProperty distance = new FloatProperty();
        public FloatProperty Distance => this.distance;

        [SerializeField]
        private Vector3Property rig = new Vector3Property();
        public Vector3Property Rig => this.rig;

        [Serializable]
        public abstract class Property<T>
        {
            [SerializeField]
            private bool apply = false;
            public bool CanApply { get { return this.apply; } set { this.apply = value; } }

            [SerializeField]
            private T value = default;
            public T Value { get { return this.value; } set { this.value = value; } }
        }

        [Serializable]
        public class Vector3Property : Property<Vector3> { }

        [Serializable]
        public class FloatProperty : Property<float> { }

        [Serializable]
        public class TransformProperty : Property<Transform> { }
    }
}
