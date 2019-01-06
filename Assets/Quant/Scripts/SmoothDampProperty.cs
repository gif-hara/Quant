using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant
{
    [Serializable]
    public class SmoothDamp
    {
        [Serializable]
        public abstract class Property<T>
        {
            protected T currentVelocity;

            public T Target { get; set; }

            [SerializeField]
            protected float smoothTime;

            public Property(Property<T> other)
            {
                this.currentVelocity = other.currentVelocity;
                this.Target = other.Target;
                this.smoothTime = other.smoothTime;
            }

            public abstract T SmoothDamp(T current);
        }

        [Serializable]
        public class Vector3 : Property<UnityEngine.Vector3>
        {
            public Vector3(Property<UnityEngine.Vector3> other) : base(other)
            {
            }

            public override UnityEngine.Vector3 SmoothDamp(UnityEngine.Vector3 current)
            {
                return UnityEngine.Vector3.SmoothDamp(current, this.Target, ref this.currentVelocity, this.smoothTime);
            }

            public UnityEngine.Vector3 SmoothDampAngle(UnityEngine.Vector3 current)
            {
                return new UnityEngine.Vector3(
                    Mathf.SmoothDampAngle(current.x, this.Target.x, ref this.currentVelocity.x, this.smoothTime),
                    Mathf.SmoothDampAngle(current.y, this.Target.y, ref this.currentVelocity.y, this.smoothTime),
                    Mathf.SmoothDampAngle(current.z, this.Target.z, ref this.currentVelocity.z, this.smoothTime)
                    );
            }
        }

        [Serializable]
        public class Float : Property<float>
        {
            public Float(Property<float> other) : base(other)
            {
            }

            public override float SmoothDamp(float current)
            {
                return Mathf.SmoothDamp(current, this.Target, ref this.currentVelocity, this.smoothTime);
            }
        }
    }
}
