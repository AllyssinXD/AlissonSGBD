/*
 * Created by SharpDevelop.
 * User: aliss
 * Date: 24/10/2025
 * Time: 14:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AlissonSGBD.Engine.FileHelpers
{
	/// <summary>
	/// Description of FileHelper.
	/// </summary>
	public class FileHelper
	{
		public FileHelper()
		{
			CreateDirIfDoesntExists("databases");
		}
	
		public static void CreateDirIfDoesntExists(string path){
			if(!Directory.Exists(path)){
				//Criar pasta
				try{
					Directory.CreateDirectory(path);
				} catch (Exception error) {
					throw;
				}
			}
		}
		
		public static void CreateFileIfDoesntExists(string path){
			if(!File.Exists(path)){
				//Criar pasta
				try{
					File.Create(path);
				} catch (Exception error) {
					throw;
				}
			}
		}
		
		public static string GetFileTextContent(string path)
		{
			try{
				FileStream fs = File.Open(path, FileMode.Open);
						
				byte[] buffer = new byte[1024];
				int bytesRead;
				
				string fullContent = "";
				
				while((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0){
					string content = System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead);
					fullContent += content;
				}
				return fullContent;
			} catch (Exception ex) {
				throw;
			}
		}
		
		public static void WriteLineOnFile(string path, string line){
			FileStream fs = File.Open(path, FileMode.Append);
			byte[] data = Encoding.UTF8.GetBytes(line);
			fs.Write(data, 0, data.Length);
			fs.Close();
		}
		
		public static int CountFiles(string path, string condition){
			List<string> files = new List<string>(Directory.EnumerateFiles(path, condition));
			return files.Count;
		}
		
		public static int CountDirs(string path){
			return new List<string>(Directory.EnumerateDirectories(path)).Count;
		}
	}
}
