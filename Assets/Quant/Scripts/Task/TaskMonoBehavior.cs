using UnityEngine;
using UnityEngine.Assertions;

namespace Quant.Task
{
    /// <summary>
    /// <see cref="MonoBehaviour"/>を継承した<see cref="Task"/>
    /// </summary>
    public abstract class TaskMonoBehavior : MonoBehaviour, ITask
    {
        public abstract void Invoke();
    }
}
