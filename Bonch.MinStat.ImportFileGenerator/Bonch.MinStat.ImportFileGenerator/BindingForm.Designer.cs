namespace Bonch.MinStat.ImportFileGenerator
{
    partial class BindingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxIdentifier = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxPost = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxPostLevel = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxEducationLevel = new System.Windows.Forms.ComboBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Идентификатор:";
            // 
            // comboBoxIdentifier
            // 
            this.comboBoxIdentifier.FormattingEnabled = true;
            this.comboBoxIdentifier.Location = new System.Drawing.Point(141, 22);
            this.comboBoxIdentifier.Name = "comboBoxIdentifier";
            this.comboBoxIdentifier.Size = new System.Drawing.Size(121, 21);
            this.comboBoxIdentifier.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Должность:";
            // 
            // comboBoxPost
            // 
            this.comboBoxPost.FormattingEnabled = true;
            this.comboBoxPost.Location = new System.Drawing.Point(141, 49);
            this.comboBoxPost.Name = "comboBoxPost";
            this.comboBoxPost.Size = new System.Drawing.Size(121, 21);
            this.comboBoxPost.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Рабочая категория:";
            // 
            // comboBoxPostLevel
            // 
            this.comboBoxPostLevel.FormattingEnabled = true;
            this.comboBoxPostLevel.Location = new System.Drawing.Point(141, 76);
            this.comboBoxPostLevel.Name = "comboBoxPostLevel";
            this.comboBoxPostLevel.Size = new System.Drawing.Size(121, 21);
            this.comboBoxPostLevel.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Уровень образования:";
            // 
            // comboBoxEducationLevel
            // 
            this.comboBoxEducationLevel.FormattingEnabled = true;
            this.comboBoxEducationLevel.Location = new System.Drawing.Point(141, 104);
            this.comboBoxEducationLevel.Name = "comboBoxEducationLevel";
            this.comboBoxEducationLevel.Size = new System.Drawing.Size(121, 21);
            this.comboBoxEducationLevel.TabIndex = 7;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(186, 227);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // BindingForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.comboBoxEducationLevel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxPostLevel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxPost);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxIdentifier);
            this.Controls.Add(this.label2);
            this.Name = "BindingForm";
            this.Load += new System.EventHandler(this.BindingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxIdentifier;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxPost;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxPostLevel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxEducationLevel;
        private System.Windows.Forms.Button buttonSave;
    }
}