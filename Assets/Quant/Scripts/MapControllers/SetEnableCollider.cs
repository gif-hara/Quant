﻿using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant.MapControllers
{
    /// <summary>
    /// 対象のコライダーのアクティブフラグを設定するマップイベント
    /// </summary>
    public sealed class SetEnableCollider : MapEvent
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
            private Collider target = null;

            [SerializeField]
            private bool isEnable = false;

            public void Invoke()
            {
                this.target.enabled = this.isEnable;
            }
        }
    }
}