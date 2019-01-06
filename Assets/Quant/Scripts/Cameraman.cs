using UnityEngine;
using UnityEngine.Assertions;

namespace Quant
{
    /// <summary>
    /// カメラを制御するクラス
    /// </summary>
    public sealed class Cameraman : MonoBehaviour
    {
        [SerializeField]
        private Transform root = null;

        [SerializeField]
        private Transform pivot = null;

        [SerializeField]
        private Transform distance = null;

        [SerializeField]
        private Transform rig = null;

        [SerializeField]
        private Camera controllerdCamera = null;

        public Vector3 Position { get { return this.root.position; } set { this.root.position = value; } }

        public Quaternion Pivot { get { return this.pivot.localRotation; } set { this.pivot.localRotation = value; } }

        public float Distance { get { return Mathf.Abs(this.distance.localPosition.z); } set { this.distance.localPosition = new Vector3(0.0f, 0.0f, -Mathf.Abs(value)); } }

        public Quaternion Rig { get { return this.rig.localRotation; } set { this.rig.localRotation = value; } }

        public Camera Camera => this.controllerdCamera;

        private void Awake()
        {
            GameEnvironment.Instance.Cameraman = this;
        }

        private void OnDestroy()
        {
            var gameEnvironment = GameEnvironment.Instance;
            if(gameEnvironment == null)
            {
                return;
            }

            gameEnvironment.Cameraman = null;
        }
    }
}
