namespace WinTestAsyncCodeBlock
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.rbAsync = new System.Windows.Forms.RadioButton();
            this.rbWaitableAsync = new System.Windows.Forms.RadioButton();
            this.rbUserCreatedThreadPool = new System.Windows.Forms.RadioButton();
            this.rbDependentAsync = new System.Windows.Forms.RadioButton();
            this.rbExecutionCompletionDelegate = new System.Windows.Forms.RadioButton();
            this.rbDependentAsyncArray = new System.Windows.Forms.RadioButton();
            this.rbWinFormsControlUpdate = new System.Windows.Forms.RadioButton();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.rbExceptionHandling = new System.Windows.Forms.RadioButton();
            this.rbWaitAllEx = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(301, 22);
            this.label1.TabIndex = 4;
            this.label1.Text = "Asynchronous Code Blocks Demo";
            // 
            // rbAsync
            // 
            this.rbAsync.AutoSize = true;
            this.rbAsync.Location = new System.Drawing.Point(13, 58);
            this.rbAsync.Name = "rbAsync";
            this.rbAsync.Size = new System.Drawing.Size(89, 17);
            this.rbAsync.TabIndex = 0;
            this.rbAsync.TabStop = true;
            this.rbAsync.Text = "async sample";
            this.rbAsync.UseVisualStyleBackColor = true;
            // 
            // rbWaitableAsync
            // 
            this.rbWaitableAsync.AutoSize = true;
            this.rbWaitableAsync.Location = new System.Drawing.Point(13, 81);
            this.rbWaitableAsync.Name = "rbWaitableAsync";
            this.rbWaitableAsync.Size = new System.Drawing.Size(128, 17);
            this.rbWaitableAsync.TabIndex = 1;
            this.rbWaitableAsync.TabStop = true;
            this.rbWaitableAsync.Text = "waitableasync sample";
            this.rbWaitableAsync.UseVisualStyleBackColor = true;
            // 
            // rbUserCreatedThreadPool
            // 
            this.rbUserCreatedThreadPool.AutoSize = true;
            this.rbUserCreatedThreadPool.Location = new System.Drawing.Point(13, 104);
            this.rbUserCreatedThreadPool.Name = "rbUserCreatedThreadPool";
            this.rbUserCreatedThreadPool.Size = new System.Drawing.Size(253, 17);
            this.rbUserCreatedThreadPool.TabIndex = 2;
            this.rbUserCreatedThreadPool.TabStop = true;
            this.rbUserCreatedThreadPool.Text = "User created ManagedIOCP ThreadPool sample";
            this.rbUserCreatedThreadPool.UseVisualStyleBackColor = true;
            // 
            // rbDependentAsync
            // 
            this.rbDependentAsync.AutoSize = true;
            this.rbDependentAsync.Location = new System.Drawing.Point(13, 172);
            this.rbDependentAsync.Name = "rbDependentAsync";
            this.rbDependentAsync.Size = new System.Drawing.Size(143, 17);
            this.rbDependentAsync.TabIndex = 5;
            this.rbDependentAsync.TabStop = true;
            this.rbDependentAsync.Text = "dependent async sample";
            this.rbDependentAsync.UseVisualStyleBackColor = true;
            // 
            // rbExecutionCompletionDelegate
            // 
            this.rbExecutionCompletionDelegate.AutoSize = true;
            this.rbExecutionCompletionDelegate.Location = new System.Drawing.Point(13, 149);
            this.rbExecutionCompletionDelegate.Name = "rbExecutionCompletionDelegate";
            this.rbExecutionCompletionDelegate.Size = new System.Drawing.Size(209, 17);
            this.rbExecutionCompletionDelegate.TabIndex = 4;
            this.rbExecutionCompletionDelegate.TabStop = true;
            this.rbExecutionCompletionDelegate.Text = "Execution Completion Delegate sample";
            this.rbExecutionCompletionDelegate.UseVisualStyleBackColor = true;
            // 
            // rbDependentAsyncArray
            // 
            this.rbDependentAsyncArray.AutoSize = true;
            this.rbDependentAsyncArray.Location = new System.Drawing.Point(13, 195);
            this.rbDependentAsyncArray.Name = "rbDependentAsyncArray";
            this.rbDependentAsyncArray.Size = new System.Drawing.Size(169, 17);
            this.rbDependentAsyncArray.TabIndex = 6;
            this.rbDependentAsyncArray.TabStop = true;
            this.rbDependentAsyncArray.Text = "dependent async array sample";
            this.rbDependentAsyncArray.UseVisualStyleBackColor = true;
            // 
            // rbWinFormsControlUpdate
            // 
            this.rbWinFormsControlUpdate.AutoSize = true;
            this.rbWinFormsControlUpdate.Location = new System.Drawing.Point(13, 242);
            this.rbWinFormsControlUpdate.Name = "rbWinFormsControlUpdate";
            this.rbWinFormsControlUpdate.Size = new System.Drawing.Size(179, 17);
            this.rbWinFormsControlUpdate.TabIndex = 8;
            this.rbWinFormsControlUpdate.TabStop = true;
            this.rbWinFormsControlUpdate.Text = "WinForms control update sample";
            this.rbWinFormsControlUpdate.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(13, 316);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(526, 147);
            this.listBox1.TabIndex = 10;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 287);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "&Execute";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rbExceptionHandling
            // 
            this.rbExceptionHandling.AutoSize = true;
            this.rbExceptionHandling.Location = new System.Drawing.Point(13, 219);
            this.rbExceptionHandling.Name = "rbExceptionHandling";
            this.rbExceptionHandling.Size = new System.Drawing.Size(151, 17);
            this.rbExceptionHandling.TabIndex = 7;
            this.rbExceptionHandling.TabStop = true;
            this.rbExceptionHandling.Text = "Exception handling sample";
            this.rbExceptionHandling.UseVisualStyleBackColor = true;
            // 
            // rbWaitAllEx
            // 
            this.rbWaitAllEx.AutoSize = true;
            this.rbWaitAllEx.Location = new System.Drawing.Point(13, 127);
            this.rbWaitAllEx.Name = "rbWaitAllEx";
            this.rbWaitAllEx.Size = new System.Drawing.Size(144, 17);
            this.rbWaitAllEx.TabIndex = 3;
            this.rbWaitAllEx.TabStop = true;
            this.rbWaitAllEx.Text = "WaitAllEx method sample";
            this.rbWaitAllEx.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(122, 287);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "&Clear";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 482);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.rbExceptionHandling);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.rbWinFormsControlUpdate);
            this.Controls.Add(this.rbDependentAsyncArray);
            this.Controls.Add(this.rbDependentAsync);
            this.Controls.Add(this.rbExecutionCompletionDelegate);
            this.Controls.Add(this.rbWaitAllEx);
            this.Controls.Add(this.rbUserCreatedThreadPool);
            this.Controls.Add(this.rbWaitableAsync);
            this.Controls.Add(this.rbAsync);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Asynchronous Code Blocks Demo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbAsync;
        private System.Windows.Forms.RadioButton rbWaitableAsync;
        private System.Windows.Forms.RadioButton rbUserCreatedThreadPool;
        private System.Windows.Forms.RadioButton rbDependentAsync;
        private System.Windows.Forms.RadioButton rbExecutionCompletionDelegate;
        private System.Windows.Forms.RadioButton rbDependentAsyncArray;
        private System.Windows.Forms.RadioButton rbWinFormsControlUpdate;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton rbExceptionHandling;
        private System.Windows.Forms.RadioButton rbWaitAllEx;
        private System.Windows.Forms.Button button2;
    }
}

