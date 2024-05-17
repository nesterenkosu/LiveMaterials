
namespace WindowsFormsApp_TcpServer
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.lb_server = new System.Windows.Forms.ListBox();
            this.lb_client = new System.Windows.Forms.ListBox();
            this.tb_message = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lb_server
            // 
            this.lb_server.FormattingEnabled = true;
            this.lb_server.ItemHeight = 16;
            this.lb_server.Location = new System.Drawing.Point(23, 29);
            this.lb_server.Name = "lb_server";
            this.lb_server.Size = new System.Drawing.Size(381, 388);
            this.lb_server.TabIndex = 0;
            // 
            // lb_client
            // 
            this.lb_client.FormattingEnabled = true;
            this.lb_client.ItemHeight = 16;
            this.lb_client.Location = new System.Drawing.Point(438, 29);
            this.lb_client.Name = "lb_client";
            this.lb_client.Size = new System.Drawing.Size(414, 244);
            this.lb_client.TabIndex = 0;
            // 
            // tb_message
            // 
            this.tb_message.Location = new System.Drawing.Point(438, 296);
            this.tb_message.Name = "tb_message";
            this.tb_message.Size = new System.Drawing.Size(414, 22);
            this.tb_message.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(438, 342);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(414, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Отправить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tb_message);
            this.Controls.Add(this.lb_client);
            this.Controls.Add(this.lb_server);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lb_server;
        private System.Windows.Forms.ListBox lb_client;
        private System.Windows.Forms.TextBox tb_message;
        private System.Windows.Forms.Button button1;
    }
}

