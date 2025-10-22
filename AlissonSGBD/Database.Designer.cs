/*
 * Criado por SharpDevelop.
 * Usuário: Alunos
 * Data: 21/10/2025
 * Hora: 18:56
 * 
 * Para alterar este modelo use Ferramentas | Opções | Codificação | Editar Cabeçalhos Padrão.
 */
namespace AlissonSGBD
{
	partial class Database
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.label1 = new System.Windows.Forms.Label();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.SQLCommands = new System.Windows.Forms.RichTextBox();
			this.pushedData = new System.Windows.Forms.RichTextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.Location = new System.Drawing.Point(12, 75);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(238, 479);
			this.treeView1.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 35);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(238, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Visão geral";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Location = new System.Drawing.Point(304, 404);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(633, 150);
			this.tableLayoutPanel1.TabIndex = 2;
			this.tableLayoutPanel1.Visible = false;
			// 
			// SQLCommands
			// 
			this.SQLCommands.Location = new System.Drawing.Point(304, 75);
			this.SQLCommands.Name = "SQLCommands";
			this.SQLCommands.Size = new System.Drawing.Size(633, 297);
			this.SQLCommands.TabIndex = 3;
			this.SQLCommands.Text = "";
			this.SQLCommands.TextChanged += new System.EventHandler(this.SQLCommandsTextChanged);
			// 
			// pushedData
			// 
			this.pushedData.Location = new System.Drawing.Point(955, 503);
			this.pushedData.Name = "pushedData";
			this.pushedData.Size = new System.Drawing.Size(62, 66);
			this.pushedData.TabIndex = 4;
			this.pushedData.Text = "";
			this.pushedData.Visible = false;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(304, 25);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(163, 33);
			this.button1.TabIndex = 5;
			this.button1.Text = "Rodar";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// Database
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1029, 605);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.pushedData);
			this.Controls.Add(this.SQLCommands);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.treeView1);
			this.Name = "Database";
			this.Text = "Database";
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.RichTextBox pushedData;
		private System.Windows.Forms.RichTextBox SQLCommands;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TreeView treeView1;
	}
}
