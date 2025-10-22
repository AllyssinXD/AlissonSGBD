/*
 * Created by SharpDevelop.
 * User: aliss
 * Date: 22/10/2025
 * Time: 11:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace AlissonSGBD.Engine
{
	/// <summary>
	/// Description of Entity.
	/// </summary>
	public class Entity
	{
		public string name;
		List<DBAttribute> attributes = new List<DBAttribute>();
		
		// Config
		string configFilePath;
		
		public Entity(string name)
		{
			this.name = name;
		}
		
		public Entity(string name, List<DBAttribute> attributes)
		{
			this.name = name;
			this.attributes = attributes;
		}
		
		public List<DBAttribute> GetAttributes(){
			return this.attributes;
		}
		
		public void AddAttributes(DBAttribute att){
			attributes.Add(att);
		}
	}
}
