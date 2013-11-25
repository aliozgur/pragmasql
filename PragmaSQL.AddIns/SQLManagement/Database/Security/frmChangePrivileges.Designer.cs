namespace SQLManagement
{
  partial class frmChangePrivileges
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChangePrivileges));
      this.chkDelete = new System.Windows.Forms.CheckBox();
      this.chkInsert = new System.Windows.Forms.CheckBox();
      this.chkRefs = new System.Windows.Forms.CheckBox();
      this.chkSelect = new System.Windows.Forms.CheckBox();
      this.chkUpdate = new System.Windows.Forms.CheckBox();
      this.chkExecute = new System.Windows.Forms.CheckBox();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOk = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // chkDelete
      // 
      this.chkDelete.AutoSize = true;
      this.chkDelete.Location = new System.Drawing.Point(153, 54);
      this.chkDelete.Name = "chkDelete";
      this.chkDelete.Size = new System.Drawing.Size(57, 17);
      this.chkDelete.TabIndex = 0;
      this.chkDelete.Text = "Delete";
      this.chkDelete.UseVisualStyleBackColor = true;
      // 
      // chkInsert
      // 
      this.chkInsert.AutoSize = true;
      this.chkInsert.Location = new System.Drawing.Point(20, 54);
      this.chkInsert.Name = "chkInsert";
      this.chkInsert.Size = new System.Drawing.Size(52, 17);
      this.chkInsert.TabIndex = 1;
      this.chkInsert.Text = "Insert";
      this.chkInsert.UseVisualStyleBackColor = true;
      // 
      // chkRefs
      // 
      this.chkRefs.AutoSize = true;
      this.chkRefs.Location = new System.Drawing.Point(153, 16);
      this.chkRefs.Name = "chkRefs";
      this.chkRefs.Size = new System.Drawing.Size(48, 17);
      this.chkRefs.TabIndex = 2;
      this.chkRefs.Text = "Refs";
      this.chkRefs.UseVisualStyleBackColor = true;
      // 
      // chkSelect
      // 
      this.chkSelect.AutoSize = true;
      this.chkSelect.Location = new System.Drawing.Point(20, 16);
      this.chkSelect.Name = "chkSelect";
      this.chkSelect.Size = new System.Drawing.Size(56, 17);
      this.chkSelect.TabIndex = 3;
      this.chkSelect.Text = "Select";
      this.chkSelect.UseVisualStyleBackColor = true;
      // 
      // chkUpdate
      // 
      this.chkUpdate.AutoSize = true;
      this.chkUpdate.Location = new System.Drawing.Point(82, 54);
      this.chkUpdate.Name = "chkUpdate";
      this.chkUpdate.Size = new System.Drawing.Size(61, 17);
      this.chkUpdate.TabIndex = 4;
      this.chkUpdate.Text = "Update";
      this.chkUpdate.UseVisualStyleBackColor = true;
      // 
      // chkExecute
      // 
      this.chkExecute.AutoSize = true;
      this.chkExecute.Location = new System.Drawing.Point(82, 16);
      this.chkExecute.Name = "chkExecute";
      this.chkExecute.Size = new System.Drawing.Size(65, 17);
      this.chkExecute.TabIndex = 5;
      this.chkExecute.Text = "Execute";
      this.chkExecute.UseVisualStyleBackColor = true;
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(204, 112);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(57, 25);
      this.btnCancel.TabIndex = 6;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOk.Location = new System.Drawing.Point(141, 112);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(57, 25);
      this.btnOk.TabIndex = 7;
      this.btnOk.Text = "OK";
      this.btnOk.UseVisualStyleBackColor = true;
      // 
      // frmChangePrivileges
      // 
      this.AcceptButton = this.btnOk;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(269, 144);
      this.Controls.Add(this.btnOk);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.chkExecute);
      this.Controls.Add(this.chkUpdate);
      this.Controls.Add(this.chkSelect);
      this.Controls.Add(this.chkRefs);
      this.Controls.Add(this.chkInsert);
      this.Controls.Add(this.chkDelete);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmChangePrivileges";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Change Object Privileges";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.CheckBox chkDelete;
    private System.Windows.Forms.CheckBox chkInsert;
    private System.Windows.Forms.CheckBox chkRefs;
    private System.Windows.Forms.CheckBox chkSelect;
    private System.Windows.Forms.CheckBox chkUpdate;
    private System.Windows.Forms.CheckBox chkExecute;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOk;
  }
}