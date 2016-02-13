using UnityEngine;

public enum TransformType { Point, Direction, Vector };

public static class TransformExtensions {

	public static Vector3 WorldToLocal( this Transform tf, Vector3 v3, TransformType type = TransformType.Point ) {
		if( type == TransformType.Point )
			return tf.InverseTransformPoint( v3 );
		else if( type == TransformType.Direction )
			return tf.InverseTransformDirection( v3 );
		else
			return tf.InverseTransformVector( v3 );
	}

	public static Vector3 LocalToWorld( this Transform tf, Vector3 v3, TransformType type = TransformType.Point ) {
		if( type == TransformType.Point )
			return tf.TransformPoint( v3 );
		else if( type == TransformType.Direction )
			return tf.TransformDirection( v3 );
		else 
			return tf.TransformVector( v3 );
	}
	
	public static void SetX( this Transform transform, float x )
	{
		transform.position = new Vector3(x, transform.position.y, transform.position.z);
	}
	
	public static void SetY( this Transform transform, float y )
	{
		transform.position = new Vector3(transform.position.x, y, transform.position.z);
	}
	
	public static void SetZ( this Transform transform, float z )
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, z);
	}
	
	public static void SetXY( this Transform transform, float x, float y )
	{
		transform.position = new Vector3(x, y, transform.position.z);
	}
	
	public static void SetXZ( this Transform transform, float x, float z )
	{
		transform.position = new Vector3(x, transform.position.y, z);
	}
	
	public static void SetYZ( this Transform transform, float y, float z )
	{
		transform.position = new Vector3(transform.position.x, y, z);
	}
	
}