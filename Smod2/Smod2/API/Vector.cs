namespace Smod2.API
{
	public class Vector
	{
		public readonly float x;
		public readonly float y;
		public readonly float z;
		
		public Vector(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		/// <summary>  
		///  Shorthand for writing new Vector(0, 0, 0).
		/// </summary> 
		public static Vector Zero => new Vector(0, 0, 0);

		/// <summary>  
		///  Shorthand for writing new Vector(1, 1, 1).
		/// </summary> 
		public static Vector One => new Vector(1, 1, 1);

		/// <summary>  
		///  Shorthand for writing new Vector(0, 0, 1).
		/// </summary> 
		public static Vector Forward => new Vector(0, 0, 1);

		/// <summary>  
		///  Shorthand for writing new Vector(0, 0, -1).
		/// </summary> 
		public static Vector Back => new Vector(0, 0, -1);

		/// <summary>  
		///  Shorthand for writing new Vector(0, 1, 0).
		/// </summary> 
		public static Vector Up => new Vector(0, 1, 0);

		/// <summary>  
		///  Shorthand for writing new Vector(0, -1, 0).
		/// </summary> 
		public static Vector Down => new Vector(0, -1, 0);

		/// <summary>  
		///  Shorthand for writing new Vector(1, 0, 0).
		/// </summary> 
		public static Vector Right => new Vector(1, 0, 0);

		/// <summary>  
		///  Shorthand for writing new Vector(-1, 0, 0).
		/// </summary> 
		public static Vector Left => new Vector(-1, 0, 0);
	}
}
