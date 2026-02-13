using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using UnityEngine;

namespace OutwardArchipelago.Utils
{
    internal class UnityMainThreadDispatcher : MonoBehaviour
    {
        private static readonly Lazy<UnityMainThreadDispatcher> _instance = new(() => CreateInstance());
        public static UnityMainThreadDispatcher Instance => _instance.Value;

        private static UnityMainThreadDispatcher CreateInstance()
        {
            var obj = new GameObject(typeof(UnityMainThreadDispatcher).FullName);
            DontDestroyOnLoad(obj);
            return obj.AddComponent<UnityMainThreadDispatcher>();
        }

        /// <summary>
        /// Queues a function to run on the main-thread and wait for it to complete.
        /// 
        /// Do not await the Task from the Unity main thread.
        /// </summary>
        /// <typeparam name="T">The return type of the delegate.</typeparam>
        /// <param name="action">The delegate.</param>
        /// <returns>The return of the delegate.</returns>
        public static Task<T> Run<T>(Func<T> action)
        {
            var tcs = new TaskCompletionSource<T>(TaskCreationOptions.RunContinuationsAsynchronously);
            Instance._mainThreadQueue.Enqueue(() =>
            {
                try
                {
                    var result = action();
                    tcs.TrySetResult(result);
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
            });
            return tcs.Task;
        }

        /// <summary>
        /// Queues a delegate to run on the main-thread and wait for it to complete.
        /// 
        /// Do not await the Task from the Unity main thread.
        /// </summary>
        /// <param name="action">The delegate.</param>
        public static Task Run(Action action)
        {
            return Run(() =>
            {
                action();
                return true;
            });
        }

        private readonly ConcurrentQueue<Action> _mainThreadQueue = new();

        private void Update()
        {
            while (_mainThreadQueue.TryDequeue(out var action))
            {
                action();
            }
        }
    }
}
