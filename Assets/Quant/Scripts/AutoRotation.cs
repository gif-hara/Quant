using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class AutoRotation : MonoBehaviour
    {
        [SerializeField]
        private Vector3 axis;

        [SerializeField]
        private float angle;

        private Transform cachedTransform;

        private void Start()
        {
            this.cachedTransform = this.transform;
            this.UpdateAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.cachedTransform.rotation *= Quaternion.AngleAxis(_this.angle * Time.deltaTime, _this.axis);
                });
        }
    }
}
