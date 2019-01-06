using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant.Task
{
    /// <summary>
    /// 対象のゲームオブジェクトのアクティブフラグを設定するタスク
    /// </summary>
    public sealed class SetActiveGameObject : TaskMonoBehavior
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
