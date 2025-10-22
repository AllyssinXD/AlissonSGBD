/*
 * Criado por SharpDevelop.
 * Usuário: Alunos
 * Data: 21/10/2025
 * Hora: 19:08
 * 
 * Para alterar este modelo use Ferramentas | Opções | Codificação | Editar Cabeçalhos Padrão.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AlissonSGBD
{
	/// <summary>
	/// Description of Access.
	/// </summary>
	public partial class Access : Form
	{	
		public Access()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			string[] logins = richTextBox1.Lines;
			for(int i = 0; i<logins.Length; i++){
				string[] login = logins[i].Split('|');
				string username = login[0];
				string password = login[1];
				
				if(textBox1.Text.Length == 0 || textBox2.Text.Length == 0){
					MessageBox.Show("Acesso Negado");
					return;
				}
				
				if(textBox1.Text != username || textBox2.Text != password){
					MessageBox.Show("Acesso Negado");
					return;
				}
				
				MessageBox.Show("Acesso garantido.");
				this.Close();
				new Database().Show();
			}
		}
		
		void AccessLoad(object sender, EventArgs e)
		{
			try {
				richTextBox1.LoadFile("access.txt", RichTextBoxStreamType.PlainText);
			} catch (Exception error) {
				richTextBox1.Text = "root|12345678";
				richTextBox1.SaveFile("access.txt", RichTextBoxStreamType.PlainText);
			}
		}
	}
}
