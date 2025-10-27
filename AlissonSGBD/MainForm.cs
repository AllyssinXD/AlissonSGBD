/*
 * Criado por SharpDevelop.
 * Usuário: Alunos
 * Data: 21/10/2025
 * Hora: 18:41
 * 
 * Para alterar este modelo use Ferramentas | Opções | Codificação | Editar Cabeçalhos Padrão.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AlissonSGBD.Engine;
using AlissonSGBD.Engine.SQL.Parsers.Errors;

namespace AlissonSGBD
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public Core engine;
		
		public MainForm()
		{
			InitializeComponent();
		}

		
		void MainFormLoad(object sender, EventArgs e)
		{
			engine = new Core();
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			try
			{
				engine.TestLexer(textBox1.Text);
			}
			catch (Exception err) {
				if (err is ParserError) { 
					MessageBox.Show("[Code " +
						(err as ParserError).Code + "] " + err.Message);
                }
                else
					throw;
			}

        }
    }
}
