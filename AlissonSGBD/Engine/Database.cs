/*
 * Created by SharpDevelop.
 * User: aliss
 * Date: 22/10/2025
 * Time: 11:50
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Text;

namespace AlissonSGBD.Engine
{
	/// <summary>
	/// Description of Database.
	/// </summary>
	public class Database
	{
		string name;
		List<Entity> entities = new List<Entity>();
		
		public Database(string name, List<Entity> entities)
		{
			this.name = name;
			this.entities = entities;
		}
		
		public void CreateEntity(string name){
			entities.Add(new Entity(name));
		}
		
		public void CreateEntity(string name, List<DBAttribute> att){
			entities.Add(new Entity(name, att));
		}
		
		public void SaveDatabase(){
			if(!Directory.Exists("databases/"+name)){
				Directory.CreateDirectory("databases/"+name);
			}
			
			if(!Directory.Exists("databases/"+name+"/entities")){
				Directory.CreateDirectory("databases/"+name+"/entities");
			}
			
			foreach(Entity en in entities){
				string path = "databases/"+name+"/entities/"+en.name+".txt";
				try{
					FileStream fs = File.Create(path);
					
					// COL(type)[restrictions,restrictions]|^^LINE1|LINE2
					string config = "";
					foreach(DBAttribute att in en.GetAttributes()){
						config+=att.name+"("+att.type+")"+"[";
						foreach(string restriction in att.restrictions){
							config+=restriction+",";
						}
						config = config.Remove(config.Length-1);
						config+="]|";
					}
					config+="^^";
					byte[] data = Encoding.UTF8.GetBytes(config);
					fs.Write(data, 0, data.Length);
					fs.Close();
				} catch (Exception error) {
					throw;
				}
			}
		}
	}
}
