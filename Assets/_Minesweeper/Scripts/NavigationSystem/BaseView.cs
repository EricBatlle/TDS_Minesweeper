using Cysharp.Threading.Tasks;
using UnityEngine;

namespace NavigationSystem
{
	public abstract class BaseView : MonoBehaviour, IView
	{
		public UniTask AwaitCloseComplete => closeCompleteTcs.Task;
        
		private readonly UniTaskCompletionSource closeCompleteTcs = new UniTaskCompletionSource();

		public virtual void Close()
		{
			closeCompleteTcs?.TrySetResult();
		}
	}
}