namespace PragmaSQL
{
  partial class DBObjectSearchWhereBuilder
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent( )
    {
      this.panWhere = new System.Windows.Forms.Panel();
      this.grd = new System.Windows.Forms.DataGridView();
      this.colContainment = new System.Windows.Forms.DataGridViewComboBoxColumn();
      this.colObjName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colOperator = new System.Windows.Forms.DataGridViewComboBoxColumn();
      this.panObjectTypes = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
      this.chkSystemObjects = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
      this.chkLike = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
      this.chkTrigger = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
      this.chkView = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
      this.chkTbl = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
      this.chkFn = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
      this.chkSp = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
      this.panWhere.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.panObjectTypes)).BeginInit();
      this.panObjectTypes.SuspendLayout();
      this.SuspendLayout();
      // 
      // panWhere
      // 
      this.panWhere.AutoScroll = true;
      this.panWhere.Controls.Add(this.grd);
      this.panWhere.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panWhere.Location = new System.Drawing.Point(0, 0);
      this.panWhere.Name = "panWhere";
      this.panWhere.Size = new System.Drawing.Size(588, 306);
      this.panWhere.TabIndex = 4;
      // 
      // grd
      // 
      this.grd.AllowUserToOrderColumns = true;
      this.grd.AllowUserToResizeRows = false;
      this.grd.BackgroundColor = System.Drawing.SystemColors.Window;
      this.grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colContainment,
            this.colObjName,
            this.colName,
            this.colOperator});
      this.grd.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grd.Location = new System.Drawing.Point(0, 0);
      this.grd.MultiSelect = false;
      this.grd.Name = "grd";
      this.grd.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.grd.RowTemplate.Height = 25;
      this.grd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.grd.Size = new System.Drawing.Size(588, 306);
      this.grd.TabIndex = 2;
      // 
      // colContainment
      // 
      this.colContainment.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colContainment.DataPropertyName = "Containment";
      this.colContainment.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.colContainment.HeaderText = "Containment";
      this.colContainment.Items.AddRange(new object[] {
            "Contains",
            "Not Contains"});
      this.colContainment.MaxDropDownItems = 2;
      this.colContainment.Name = "colContainment";
      // 
      // colObjName
      // 
      this.colObjName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colObjName.DataPropertyName = "ObjName";
      this.colObjName.HeaderText = "Object Name";
      this.colObjName.Name = "colObjName";
      // 
      // colName
      // 
      this.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colName.DataPropertyName = "Name";
      this.colName.HeaderText = "Search Text";
      this.colName.Name = "colName";
      this.colName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.colName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
      // 
      // colOperator
      // 
      this.colOperator.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colOperator.DataPropertyName = "Operator";
      this.colOperator.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.colOperator.HeaderText = "Operator";
      this.colOperator.Items.AddRange(new object[] {
            "AND",
            "OR"});
      this.colOperator.MaxDropDownItems = 2;
      this.colOperator.Name = "colOperator";
      // 
      // panObjectTypes
      // 
      this.panObjectTypes.Controls.Add(this.chkSystemObjects);
      this.panObjectTypes.Controls.Add(this.chkLike);
      this.panObjectTypes.Controls.Add(this.chkTrigger);
      this.panObjectTypes.Controls.Add(this.chkView);
      this.panObjectTypes.Controls.Add(this.chkTbl);
      this.panObjectTypes.Controls.Add(this.chkFn);
      this.panObjectTypes.Controls.Add(this.chkSp);
      this.panObjectTypes.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panObjectTypes.Location = new System.Drawing.Point(0, 306);
      this.panObjectTypes.Name = "panObjectTypes";
      this.panObjectTypes.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
      this.panObjectTypes.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelClient;
      this.panObjectTypes.Size = new System.Drawing.Size(588, 69);
      this.panObjectTypes.TabIndex = 6;
      // 
      // chkSystemObjects
      // 
      this.chkSystemObjects.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
      this.chkSystemObjects.Location = new System.Drawing.Point(274, 39);
      this.chkSystemObjects.Name = "chkSystemObjects";
      this.chkSystemObjects.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
      this.chkSystemObjects.Size = new System.Drawing.Size(107, 20);
      this.chkSystemObjects.TabIndex = 7;
      this.chkSystemObjects.Text = "System Objects";
      this.chkSystemObjects.Values.ExtraText = "";
      this.chkSystemObjects.Values.Image = null;
      this.chkSystemObjects.Values.Text = "System Objects";
      // 
      // chkLike
      // 
      this.chkLike.Checked = true;
      this.chkLike.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkLike.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
      this.chkLike.Location = new System.Drawing.Point(395, 14);
      this.chkLike.Name = "chkLike";
      this.chkLike.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
      this.chkLike.Size = new System.Drawing.Size(126, 20);
      this.chkLike.TabIndex = 7;
      this.chkLike.Text = "Use LIKE for match";
      this.chkLike.Values.ExtraText = "";
      this.chkLike.Values.Image = null;
      this.chkLike.Values.Text = "Use LIKE for match";
      // 
      // chkTrigger
      // 
      this.chkTrigger.Checked = true;
      this.chkTrigger.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkTrigger.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
      this.chkTrigger.Location = new System.Drawing.Point(274, 14);
      this.chkTrigger.Name = "chkTrigger";
      this.chkTrigger.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
      this.chkTrigger.Size = new System.Drawing.Size(68, 20);
      this.chkTrigger.TabIndex = 7;
      this.chkTrigger.Text = "Triggers";
      this.chkTrigger.Values.ExtraText = "";
      this.chkTrigger.Values.Image = null;
      this.chkTrigger.Values.Text = "Triggers";
      // 
      // chkView
      // 
      this.chkView.Checked = true;
      this.chkView.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkView.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
      this.chkView.Location = new System.Drawing.Point(153, 39);
      this.chkView.Name = "chkView";
      this.chkView.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
      this.chkView.Size = new System.Drawing.Size(55, 20);
      this.chkView.TabIndex = 7;
      this.chkView.Text = "Views";
      this.chkView.Values.ExtraText = "";
      this.chkView.Values.Image = null;
      this.chkView.Values.Text = "Views";
      // 
      // chkTbl
      // 
      this.chkTbl.Checked = true;
      this.chkTbl.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkTbl.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
      this.chkTbl.Location = new System.Drawing.Point(153, 14);
      this.chkTbl.Name = "chkTbl";
      this.chkTbl.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
      this.chkTbl.Size = new System.Drawing.Size(58, 20);
      this.chkTbl.TabIndex = 7;
      this.chkTbl.Text = "Tables";
      this.chkTbl.Values.ExtraText = "";
      this.chkTbl.Values.Image = null;
      this.chkTbl.Values.Text = "Tables";
      // 
      // chkFn
      // 
      this.chkFn.Checked = true;
      this.chkFn.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkFn.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
      this.chkFn.Location = new System.Drawing.Point(16, 39);
      this.chkFn.Name = "chkFn";
      this.chkFn.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
      this.chkFn.Size = new System.Drawing.Size(76, 20);
      this.chkFn.TabIndex = 7;
      this.chkFn.Text = "Functions";
      this.chkFn.Values.ExtraText = "";
      this.chkFn.Values.Image = null;
      this.chkFn.Values.Text = "Functions";
      // 
      // chkSp
      // 
      this.chkSp.Checked = true;
      this.chkSp.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkSp.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
      this.chkSp.Location = new System.Drawing.Point(16, 14);
      this.chkSp.Name = "chkSp";
      this.chkSp.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
      this.chkSp.Size = new System.Drawing.Size(124, 20);
      this.chkSp.TabIndex = 7;
      this.chkSp.Text = "Stored Procedures";
      this.chkSp.Values.ExtraText = "";
      this.chkSp.Values.Image = null;
      this.chkSp.Values.Text = "Stored Procedures";
      // 
      // DBObjectSearchWhereBuilder
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.panWhere);
      this.Controls.Add(this.panObjectTypes);
      this.Name = "DBObjectSearchWhereBuilder";
      this.Size = new System.Drawing.Size(588, 375);
      this.panWhere.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.panObjectTypes)).EndInit();
      this.panObjectTypes.ResumeLayout(false);
      this.panObjectTypes.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panWhere;
    private System.Windows.Forms.DataGridView grd;
    private System.Windows.Forms.DataGridViewComboBoxColumn colContainment;
    private System.Windows.Forms.DataGridViewTextBoxColumn colObjName;
    private System.Windows.Forms.DataGridViewTextBoxColumn colName;
    private System.Windows.Forms.DataGridViewComboBoxColumn colOperator;
    private ComponentFactory.Krypton.Toolkit.KryptonPanel panObjectTypes;
    private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkSp;
    private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkSystemObjects;
    private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkLike;
    private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkTrigger;
    private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkView;
    private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkTbl;
    private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkFn;
  }
}
