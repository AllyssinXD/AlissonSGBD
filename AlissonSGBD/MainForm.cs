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

namespace AlissonSGBD
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
		}
	

		void Button1Click(object sender, EventArgs e)
		{
			this.Hide();
			Access accessForm = new Access();
			accessForm.Show();
		}
	}
}
