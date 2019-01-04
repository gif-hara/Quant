using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant.MapControllers
{
    /// <summary>
    /// 対象のゲームオブジェクトのアクティブフラグを設定するマップイベント
    /// </summary>
    public sealed class SetActiveGameObject : MapEvent
    {
        [SerializeField]
        private Parameter[] parameters = null;

        public override void Invoke()
        {
            foreach(var p in this.parameters)
            {
                p.Invoke();
            }
        }

        [Serializable]
        public class Parameter
        {
            [SerializeField]
            private GameObject target = null;

            [SerializeField]
            private bool isActive = false;

            public void Invoke()
            {
                this.target.SetActive(this.isActive);
            }
        }
    }
}
