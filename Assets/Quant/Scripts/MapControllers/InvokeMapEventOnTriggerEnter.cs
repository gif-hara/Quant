using HK.Framework.Extensions;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant.MapControllers
{
    /// <summary>
    /// OnTriggerEnterに反応してマップイベントを実行するクラス
    /// </summary>
    public sealed class InvokeMapEventOnTriggerEnter : MonoBehaviour
    {
        [SerializeField]
        private LayerMask includeLayerMask = new LayerMask();

        [SerializeField]
        private MapEvent[] events = null;

        private void OnTriggerEnter(Collider other)
        {
            if(this.includeLayerMask.IsIncluded(other.gameObject))
            {
                foreach(var e in this.events)
                {
                    e.Invoke();
                }
            }
        }
    }
}
