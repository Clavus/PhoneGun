using UnityEngine;

public static class MonoBehaviourExtensions {

	public I GetInterfaceComponent<I>( this MonoBehaviour s ) where I : class
	{
	   return s.GetComponent(typeof(I)) as I;
	}
	 
	public List<I> FindObjectsOfInterface<I>( this MonoBehaviour s ) where I : class
	{
	   MonoBehaviour[] monoBehaviours = s.FindObjectsOfType<MonoBehaviour>();
	   List<I> list = new List<I>();
	 
	   foreach(MonoBehaviour behaviour in monoBehaviours)
	   {
		  I component = behaviour.GetComponent(typeof(I)) as I;
	 
		  if(component != null)
		  {
			 list.Add(component);
		  }
	   }
	 
	   return list;
	}
	
	public void Invoke( this MonoBehaviour s, Task task, float time)
	{
		s.Invoke(task.Method.Name, time);
	}

}


