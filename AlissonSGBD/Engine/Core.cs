using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace AlissonSGBD.Engine
{
	/// <summary>
	///  Fluxo : 
	///  Usuário abre o APP
	///  Banco espera login e senha
	///  Usuario insere credenciais
	/// 	Banco aceita ou nega :
	///  - Aceita
	/// 	Roda leitura dos bancos salvos no PC
	///  Abre Dashboard
	///  - Nega
	///  Não faz leitura de nada
	/// </summary>
	public class Core
	{
		public int userId = 0;
		
		public Core()
		{
			StartupConfiguration();
		}
		
		void StartupConfiguration() {
			// Verificar se estrutura basica do SGDB está criada
			if(!Directory.Exists("databases")){
				//Criar pasta
				try{
					Directory.CreateDirectory("databases");
				} catch (Exception error) {
					throw;
				}
			}
			
			Database sys = CreateDatabase("sys");
			sys.CreateEntity("users", new List<DBAttribute>
				{
					new DBAttribute("name", new string[] {"unique"}, "string"),
					new DBAttribute("password", new string[] {"not_null"}, "string")
				}
			);
			sys.SaveDatabase();
		}
		
		// Leitura de arquivo - tabelas
		// Leitura de pastas - databases
		public List<string> GetAllDatabases() {
			List <string> dbs = new List<string>(Directory.EnumerateDirectories("databases"));
			return dbs;
		}
		
		public Database CreateDatabase(string name) {
			
			if(!GetAllDatabases().Contains(name)) {
				Database db = new Database(name, new List<Entity>());
				db.SaveDatabase();
				return db;
			} else {
				throw new Exception();
			}
			
		}
		
		public static Database LoadDBFromDir(string path) {
			//Pega uma pasta
			//Cria objeto "Database" com mesmo nome
			//Com base nos arquivos entro de "entities", Criar objetos de entidade
			
			if(!Directory.Exists("databases/"+path)){
				throw new Exception();
			}
			
			string dbName = path;
			
			if(!Directory.Exists("databases/"+path+"/entities")){
				// Base de dados mal estruturada
				throw new Exception();
			}
			
			List<string> entityNames = new List<string>(Directory.EnumerateFiles("databases/"+path+"/entities"));
			
			// Ler arquivos!
			// COL(type)[restrictions,restrictions]|^^LINE1|LINE2
			
			List<Entity> entities = new List<Entity>();
			
			foreach(string entityName in entityNames){
				string pathOfEntity = "databases/"+path+"/entities/"+entityName;
				string content = File.ReadAllText(pathOfEntity);
				
				entities.Add(new Entity(entityName));
				Debug.WriteLine(content);
			}
			
			return new Database(dbName, entities);
		}
	}
}
