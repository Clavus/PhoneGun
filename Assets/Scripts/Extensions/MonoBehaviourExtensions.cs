using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public static class MonoBehaviourExtensions {

	public static I GetInterfaceComponent<I>( this MonoBehaviour s ) where I : class
	{
	   return s.GetComponent(typeof(I)) as I;
	}
	 
	public static List<I> FindObjectsOfInterface<I>( this MonoBehaviour s ) where I : class
	{
	   MonoBehaviour[] monoBehaviours = Object.FindObjectsOfType<MonoBehaviour>();
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
	
	//public static void Invoke( this MonoBehaviour s, Task task, float time)
	//{
	//	s.Invoke(task.Method.Name, time);
	//}

}


