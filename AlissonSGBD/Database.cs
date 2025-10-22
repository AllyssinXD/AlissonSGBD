/*
 * Criado por SharpDevelop.
 * Usuário: Alunos
 * Data: 21/10/2025
 * Hora: 18:56
 * 
 * Para alterar este modelo use Ferramentas | Opções | Codificação | Editar Cabeçalhos Padrão.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace AlissonSGBD
{
	/// <summary>
	/// Description of Database.
	/// </summary>
	public partial class Database : Form
	{
		public Database()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			loadDatabases();
		}
		
		void loadDatabases(){
			string dir = AppDomain.CurrentDomain.BaseDirectory;
			string[] databases = Directory.GetFiles(dir, "*.txt");
			
			foreach(string database in databases){
				string[] divided = database.Split('\\');
				string name = divided[divided.Length-1].Split('.')[0];
				TreeNode db = treeView1.Nodes.Add(name);
				db.Nodes.Add("Entidades");
				db.Nodes.Add("Indices");
				db.Nodes.Add("Informações");
				treeView1.Refresh();
			}
		}
		
		void SQLCommandsTextChanged(object sender, EventArgs e)
		{
			int original = SQLCommands.SelectionStart;
			SQLCommands.Select(0, SQLCommands.Text.Length);
			SQLCommands.SelectionColor = SQLCommands.ForeColor;
			SQLCommands.Select(original, 0);
			string text = SQLCommands.Text.ToUpper();
			foreach(string command in SQLStatics.commands){
				int lastCommand = text.IndexOf(command);
				Debug.WriteLine(lastCommand);
				if(lastCommand < 0){
					continue;
				}
				SQLCommands.Select(lastCommand, command.Length);
				SQLCommands.SelectionColor = Color.Blue;
				SQLCommands.Select(SQLCommands.Text.Length, 0);
				SQLCommands.SelectionColor = SQLCommands.ForeColor;
			}
		}
	}
}
