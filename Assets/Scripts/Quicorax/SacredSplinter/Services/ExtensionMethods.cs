using System.Threading.Tasks;
using UnityEngine;

namespace Quicorax.SacredSplinter.Services
{
    public static class ExtensionMethods
    {
        public static Task ManageTaskException(this Task task) => 
            task.ContinueWith(task => Debug.LogException(task.Exception), TaskContinuationOptions.OnlyOnFaulted);
    }
}