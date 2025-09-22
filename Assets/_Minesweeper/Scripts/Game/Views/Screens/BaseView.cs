using Cysharp.Threading.Tasks;
using NavigationSystem;
using UnityEngine;

namespace Game
{
	public abstract class BaseView : MonoBehaviour, IView
	{
		public UniTask AwaitCloseComplete => closeCompleteTcs.Task;
        
		private readonly UniTaskCompletionSource closeCompleteTcs = new UniTaskCompletionSource();

		public virtual void Close()
		{
			closeCompleteTcs.TrySetResult();
		}

		protected virtual void OnDestroy()
		{
			closeCompleteTcs?.TrySetResult();
		}
	}
}