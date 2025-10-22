/*
 * Created by SharpDevelop.
 * User: aliss
 * Date: 22/10/2025
 * Time: 11:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace AlissonSGBD.Engine
{
	/// <summary>
	/// Description of Attributes.
	/// </summary>
	public class DBAttribute
	{
		public string name;
		public string[] restrictions;
		public string type {get; set;}
		
		public DBAttribute(string name, string[] restrictions, string type)
		{
			this.name = name;
			this.restrictions = restrictions;
			this.type = type;
		}
	}
}
