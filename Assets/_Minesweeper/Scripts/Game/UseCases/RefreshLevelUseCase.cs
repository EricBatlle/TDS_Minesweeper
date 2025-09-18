using System;

namespace Game
{
	public class RefreshLevelUseCase
	{
		public event Action RefreshLevel;
        
		public void Execute()
		{
			RefreshLevel?.Invoke();    
		}
	}
}