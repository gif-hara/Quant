using HK.Framework.Extensions;
using Quant.Task;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant
{
    /// <summary>
    /// OnTriggerEnterに反応してタスクを実行するクラス
    /// </summary>
    public sealed class InvokeMapEventOnTriggerEnter : MonoBehaviour
    {
        [SerializeField]
        private LayerMask includeLayerMask = new LayerMask();

        [SerializeField]
        private TaskMonoBehavior[] events = null;

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
