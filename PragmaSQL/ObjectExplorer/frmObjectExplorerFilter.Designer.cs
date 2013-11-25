namespace PragmaSQL
{
  partial class frmObjectExplorerFilter
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose( bool disposing )
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
    private void InitializeComponent( )
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmObjectExplorerFilter));
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnApply = new System.Windows.Forms.Button();
      this.btnClear = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.edtFilterText = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.cmbFilterType = new System.Windows.Forms.ComboBox();
      this.chkApplyToChildren = new System.Windows.Forms.CheckBox();
      this.btnApplyParentFilter = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(325, 105);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 26);
      this.btnCancel.TabIndex = 4;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // btnApply
      // 
      this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnApply.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnApply.Location = new System.Drawing.Point(244, 105);
      this.btnApply.Name = "btnApply";
      this.btnApply.Size = new System.Drawing.Size(75, 26);
      this.btnApply.TabIndex = 3;
      this.btnApply.Text = "Apply";
      this.btnApply.UseVisualStyleBackColor = true;
      this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
      // 
      // btnClear
      // 
      this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClear.Location = new System.Drawing.Point(12, 105);
      this.btnClear.Name = "btnClear";
      this.btnClear.Size = new System.Drawing.Size(75, 26);
      this.btnClear.TabIndex = 6;
      this.btnClear.Text = "Clear";
      this.btnClear.UseVisualStyleBackColor = true;
      this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 8);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(53, 13);
      this.label1.TabIndex = 3;
      this.label1.Text = "Filter Text";
      // 
      // edtFilterText
      // 
      this.edtFilterText.Location = new System.Drawing.Point(12, 23);
      this.edtFilterText.Name = "edtFilterText";
      this.edtFilterText.Size = new System.Drawing.Size(373, 20);
      this.edtFilterText.TabIndex = 0;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 48);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(56, 13);
      this.label2.TabIndex = 5;
      this.label2.Text = "Filter Type";
      // 
      // cmbFilterType
      // 
      this.cmbFilterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbFilterType.FormattingEnabled = true;
      this.cmbFilterType.Location = new System.Drawing.Point(12, 63);
      this.cmbFilterType.Name = "cmbFilterType";
      this.cmbFilterType.Size = new System.Drawing.Size(165, 21);
      this.cmbFilterType.TabIndex = 1;
      // 
      // chkApplyToChildren
      // 
      this.chkApplyToChildren.AutoSize = true;
      this.chkApplyToChildren.Location = new System.Drawing.Point(200, 67);
      this.chkApplyToChildren.Name = "chkApplyToChildren";
      this.chkApplyToChildren.Size = new System.Drawing.Size(180, 17);
      this.chkApplyToChildren.TabIndex = 2;
      this.chkApplyToChildren.Text = "Apply to all child grouping folders";
      this.chkApplyToChildren.UseVisualStyleBackColor = true;
      // 
      // btnApplyParentFilter
      // 
      this.btnApplyParentFilter.Location = new System.Drawing.Point(140, 105);
      this.btnApplyParentFilter.Name = "btnApplyParentFilter";
      this.btnApplyParentFilter.Size = new System.Drawing.Size(98, 26);
      this.btnApplyParentFilter.TabIndex = 5;
      this.btnApplyParentFilter.Text = "Apply Parent";
      this.btnApplyParentFilter.UseVisualStyleBackColor = true;
      this.btnApplyParentFilter.Click += new System.EventHandler(this.btnApplyParentFilter_Click);
      // 
      // frmObjectExplorerFilter
      // 
      this.AcceptButton = this.btnApply;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(415, 137);
      this.Controls.Add(this.btnApplyParentFilter);
      this.Controls.Add(this.chkApplyToChildren);
      this.Controls.Add(this.cmbFilterType);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.edtFilterText);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.btnClear);
      this.Controls.Add(this.btnApply);
      this.Controls.Add(this.btnCancel);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(423, 171);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(423, 171);
      this.Name = "frmObjectExplorerFilter";
      this.ShowInTaskbar = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Filter";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnApply;
    private System.Windows.Forms.Button btnClear;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox edtFilterText;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox cmbFilterType;
    private System.Windows.Forms.CheckBox chkApplyToChildren;
    private System.Windows.Forms.Button btnApplyParentFilter;
  }
}