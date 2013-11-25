namespace SQLManagement
{
  partial class TableEdit
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.panel1 = new System.Windows.Forms.Panel();
      this.gbTableDefinition = new System.Windows.Forms.GroupBox();
      this.cmbFileGroup = new System.Windows.Forms.ComboBox();
      this.label8 = new System.Windows.Forms.Label();
      this.txtName = new System.Windows.Forms.TextBox();
      this.label7 = new System.Windows.Forms.Label();
      this.cmbOwner = new System.Windows.Forms.ComboBox();
      this.label6 = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.grd = new System.Windows.Forms.DataGridView();
      this.colPk = new System.Windows.Forms.DataGridViewImageColumn();
      this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colDataType = new System.Windows.Forms.DataGridViewComboBoxColumn();
      this.colWidth = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colPrecision = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colScale = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colAllowNull = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.colDefault = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colIdentity = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.colSeed = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colIncrement = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.bsCols = new System.Windows.Forms.BindingSource(this.components);
      this.panel2 = new System.Windows.Forms.Panel();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.gbAdditionalProps = new System.Windows.Forms.GroupBox();
      this.cmbCollation = new System.Windows.Forms.ComboBox();
      this.lblAdditionalPropError = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.chkComputed = new System.Windows.Forms.CheckBox();
      this.txtFormula = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.txtDescription = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.cmbRuleBinding = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.cmbDefaultBinding = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.panel3 = new System.Windows.Forms.Panel();
      this.lblHeader = new System.Windows.Forms.Label();
      this.tpPK = new System.Windows.Forms.TabPage();
      this.tpFK = new System.Windows.Forms.TabPage();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.btnPK = new System.Windows.Forms.ToolStripButton();
      this.splitter1 = new System.Windows.Forms.Splitter();
      this.panel1.SuspendLayout();
      this.gbTableDefinition.SuspendLayout();
      this.groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsCols)).BeginInit();
      this.panel2.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.gbAdditionalProps.SuspendLayout();
      this.panel3.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.gbTableDefinition);
      this.panel1.Controls.Add(this.groupBox1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 25);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(874, 282);
      this.panel1.TabIndex = 0;
      // 
      // gbTableDefinition
      // 
      this.gbTableDefinition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.gbTableDefinition.Controls.Add(this.cmbFileGroup);
      this.gbTableDefinition.Controls.Add(this.label8);
      this.gbTableDefinition.Controls.Add(this.txtName);
      this.gbTableDefinition.Controls.Add(this.label7);
      this.gbTableDefinition.Controls.Add(this.cmbOwner);
      this.gbTableDefinition.Controls.Add(this.label6);
      this.gbTableDefinition.Location = new System.Drawing.Point(7, 3);
      this.gbTableDefinition.Name = "gbTableDefinition";
      this.gbTableDefinition.Size = new System.Drawing.Size(861, 95);
      this.gbTableDefinition.TabIndex = 2;
      this.gbTableDefinition.TabStop = false;
      this.gbTableDefinition.Text = "Table Definition";
      // 
      // cmbFileGroup
      // 
      this.cmbFileGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbFileGroup.FormattingEnabled = true;
      this.cmbFileGroup.Location = new System.Drawing.Point(99, 66);
      this.cmbFileGroup.Name = "cmbFileGroup";
      this.cmbFileGroup.Size = new System.Drawing.Size(121, 21);
      this.cmbFileGroup.TabIndex = 21;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(13, 68);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(55, 13);
      this.label8.TabIndex = 20;
      this.label8.Text = "File Group";
      // 
      // txtName
      // 
      this.txtName.Location = new System.Drawing.Point(99, 41);
      this.txtName.Name = "txtName";
      this.txtName.Size = new System.Drawing.Size(216, 20);
      this.txtName.TabIndex = 19;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(13, 42);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(65, 13);
      this.label7.TabIndex = 18;
      this.label7.Text = "Table Name";
      // 
      // cmbOwner
      // 
      this.cmbOwner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbOwner.FormattingEnabled = true;
      this.cmbOwner.Location = new System.Drawing.Point(99, 15);
      this.cmbOwner.Name = "cmbOwner";
      this.cmbOwner.Size = new System.Drawing.Size(121, 21);
      this.cmbOwner.TabIndex = 15;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(13, 17);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(38, 13);
      this.label6.TabIndex = 14;
      this.label6.Text = "Owner";
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.Controls.Add(this.grd);
      this.groupBox1.Location = new System.Drawing.Point(4, 101);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(864, 178);
      this.groupBox1.TabIndex = 1;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Columns";
      // 
      // grd
      // 
      this.grd.AllowUserToResizeRows = false;
      this.grd.AutoGenerateColumns = false;
      this.grd.BackgroundColor = System.Drawing.Color.White;
      this.grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPk,
            this.colName,
            this.colDataType,
            this.colWidth,
            this.colPrecision,
            this.colScale,
            this.colAllowNull,
            this.colDefault,
            this.colIdentity,
            this.colSeed,
            this.colIncrement});
      this.grd.DataSource = this.bsCols;
      this.grd.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grd.Location = new System.Drawing.Point(3, 16);
      this.grd.MultiSelect = false;
      this.grd.Name = "grd";
      this.grd.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.grd.RowTemplate.Height = 25;
      this.grd.Size = new System.Drawing.Size(858, 159);
      this.grd.TabIndex = 1;
      this.grd.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_CellValueChanged);
      this.grd.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_CellValidated);
      this.grd.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.grd_CellValidating);
      this.grd.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grd_CellPainting);
      // 
      // colPk
      // 
      this.colPk.DataPropertyName = "PkImage";
      this.colPk.HeaderText = "PK";
      this.colPk.Name = "colPk";
      this.colPk.ReadOnly = true;
      this.colPk.Width = 21;
      // 
      // colName
      // 
      this.colName.DataPropertyName = "Name";
      this.colName.HeaderText = "Name";
      this.colName.Name = "colName";
      this.colName.Width = 60;
      // 
      // colDataType
      // 
      this.colDataType.DataPropertyName = "DataType";
      this.colDataType.HeaderText = "Data Type";
      this.colDataType.Name = "colDataType";
      this.colDataType.Width = 63;
      // 
      // colWidth
      // 
      this.colWidth.DataPropertyName = "Width";
      this.colWidth.HeaderText = "Width";
      this.colWidth.Name = "colWidth";
      this.colWidth.Width = 61;
      // 
      // colPrecision
      // 
      this.colPrecision.DataPropertyName = "Precision";
      this.colPrecision.HeaderText = "Precision";
      this.colPrecision.Name = "colPrecision";
      this.colPrecision.Width = 75;
      // 
      // colScale
      // 
      this.colScale.DataPropertyName = "Scale";
      this.colScale.HeaderText = "Scale";
      this.colScale.Name = "colScale";
      this.colScale.Width = 57;
      // 
      // colAllowNull
      // 
      this.colAllowNull.DataPropertyName = "AllowNulls";
      this.colAllowNull.HeaderText = "Allow Null";
      this.colAllowNull.Name = "colAllowNull";
      this.colAllowNull.Width = 63;
      // 
      // colDefault
      // 
      this.colDefault.DataPropertyName = "Default";
      this.colDefault.HeaderText = "Default";
      this.colDefault.Name = "colDefault";
      this.colDefault.ReadOnly = true;
      this.colDefault.Width = 68;
      // 
      // colIdentity
      // 
      this.colIdentity.DataPropertyName = "IsIdentity";
      this.colIdentity.HeaderText = "Identity";
      this.colIdentity.Name = "colIdentity";
      this.colIdentity.Width = 50;
      // 
      // colSeed
      // 
      this.colSeed.DataPropertyName = "Seed";
      this.colSeed.HeaderText = "Seed";
      this.colSeed.Name = "colSeed";
      this.colSeed.Width = 55;
      // 
      // colIncrement
      // 
      this.colIncrement.DataPropertyName = "Increment";
      this.colIncrement.HeaderText = "Increment";
      this.colIncrement.Name = "colIncrement";
      this.colIncrement.Width = 81;
      // 
      // bsCols
      // 
      this.bsCols.AddingNew += new System.ComponentModel.AddingNewEventHandler(this.bsCols_AddingNew);
      this.bsCols.PositionChanged += new System.EventHandler(this.bsCols_PositionChanged);
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.tabControl1);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel2.Location = new System.Drawing.Point(0, 307);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(874, 330);
      this.panel2.TabIndex = 2;
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tpPK);
      this.tabControl1.Controls.Add(this.tpFK);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(874, 330);
      this.tabControl1.TabIndex = 0;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.gbAdditionalProps);
      this.tabPage1.Controls.Add(this.panel3);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(866, 304);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Other Column Properties";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // gbAdditionalProps
      // 
      this.gbAdditionalProps.Controls.Add(this.cmbCollation);
      this.gbAdditionalProps.Controls.Add(this.lblAdditionalPropError);
      this.gbAdditionalProps.Controls.Add(this.label5);
      this.gbAdditionalProps.Controls.Add(this.chkComputed);
      this.gbAdditionalProps.Controls.Add(this.txtFormula);
      this.gbAdditionalProps.Controls.Add(this.label4);
      this.gbAdditionalProps.Controls.Add(this.txtDescription);
      this.gbAdditionalProps.Controls.Add(this.label3);
      this.gbAdditionalProps.Controls.Add(this.cmbRuleBinding);
      this.gbAdditionalProps.Controls.Add(this.label2);
      this.gbAdditionalProps.Controls.Add(this.cmbDefaultBinding);
      this.gbAdditionalProps.Controls.Add(this.label1);
      this.gbAdditionalProps.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gbAdditionalProps.Location = new System.Drawing.Point(3, 23);
      this.gbAdditionalProps.Name = "gbAdditionalProps";
      this.gbAdditionalProps.Size = new System.Drawing.Size(860, 278);
      this.gbAdditionalProps.TabIndex = 0;
      this.gbAdditionalProps.TabStop = false;
      // 
      // cmbCollation
      // 
      this.cmbCollation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbCollation.FormattingEnabled = true;
      this.cmbCollation.Location = new System.Drawing.Point(98, 172);
      this.cmbCollation.Name = "cmbCollation";
      this.cmbCollation.Size = new System.Drawing.Size(389, 21);
      this.cmbCollation.TabIndex = 25;
      this.cmbCollation.SelectedIndexChanged += new System.EventHandler(this.cmbCollation_SelectedIndexChanged);
      // 
      // lblAdditionalPropError
      // 
      this.lblAdditionalPropError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.lblAdditionalPropError.AutoSize = true;
      this.lblAdditionalPropError.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblAdditionalPropError.ForeColor = System.Drawing.Color.Red;
      this.lblAdditionalPropError.Location = new System.Drawing.Point(701, 19);
      this.lblAdditionalPropError.Name = "lblAdditionalPropError";
      this.lblAdditionalPropError.Size = new System.Drawing.Size(142, 13);
      this.lblAdditionalPropError.TabIndex = 24;
      this.lblAdditionalPropError.Text = "Selected column is not valid!";
      this.lblAdditionalPropError.Visible = false;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(13, 172);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(47, 13);
      this.label5.TabIndex = 21;
      this.label5.Text = "Collation";
      // 
      // chkComputed
      // 
      this.chkComputed.AutoSize = true;
      this.chkComputed.Location = new System.Drawing.Point(99, 126);
      this.chkComputed.Name = "chkComputed";
      this.chkComputed.Size = new System.Drawing.Size(85, 17);
      this.chkComputed.TabIndex = 20;
      this.chkComputed.Text = "Is Computed";
      this.chkComputed.UseVisualStyleBackColor = true;
      this.chkComputed.CheckedChanged += new System.EventHandler(this.chkComputed_CheckedChanged);
      // 
      // txtFormula
      // 
      this.txtFormula.Location = new System.Drawing.Point(99, 144);
      this.txtFormula.Name = "txtFormula";
      this.txtFormula.Size = new System.Drawing.Size(388, 20);
      this.txtFormula.TabIndex = 19;
      this.txtFormula.TextChanged += new System.EventHandler(this.txtFormula_TextChanged);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(13, 146);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(44, 13);
      this.label4.TabIndex = 18;
      this.label4.Text = "Formula";
      // 
      // txtDescription
      // 
      this.txtDescription.Location = new System.Drawing.Point(99, 62);
      this.txtDescription.Multiline = true;
      this.txtDescription.Name = "txtDescription";
      this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txtDescription.Size = new System.Drawing.Size(482, 59);
      this.txtDescription.TabIndex = 17;
      this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(13, 69);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(60, 13);
      this.label3.TabIndex = 16;
      this.label3.Text = "Description";
      // 
      // cmbRuleBinding
      // 
      this.cmbRuleBinding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbRuleBinding.FormattingEnabled = true;
      this.cmbRuleBinding.Location = new System.Drawing.Point(99, 38);
      this.cmbRuleBinding.Name = "cmbRuleBinding";
      this.cmbRuleBinding.Size = new System.Drawing.Size(181, 21);
      this.cmbRuleBinding.TabIndex = 15;
      this.cmbRuleBinding.SelectedIndexChanged += new System.EventHandler(this.cmbRuleBinding_SelectedIndexChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(13, 41);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(67, 13);
      this.label2.TabIndex = 14;
      this.label2.Text = "Rule Binding";
      // 
      // cmbDefaultBinding
      // 
      this.cmbDefaultBinding.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
      this.cmbDefaultBinding.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.cmbDefaultBinding.FormattingEnabled = true;
      this.cmbDefaultBinding.Location = new System.Drawing.Point(99, 13);
      this.cmbDefaultBinding.Name = "cmbDefaultBinding";
      this.cmbDefaultBinding.Size = new System.Drawing.Size(181, 21);
      this.cmbDefaultBinding.TabIndex = 13;
      this.cmbDefaultBinding.SelectedIndexChanged += new System.EventHandler(this.cmbDefaultBinding_SelectedIndexChanged);
      this.cmbDefaultBinding.TextChanged += new System.EventHandler(this.cmbDefaultBinding_TextChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(13, 16);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(79, 13);
      this.label1.TabIndex = 12;
      this.label1.Text = "Default Binding";
      // 
      // panel3
      // 
      this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaption;
      this.panel3.Controls.Add(this.lblHeader);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel3.Location = new System.Drawing.Point(3, 3);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(860, 20);
      this.panel3.TabIndex = 27;
      // 
      // lblHeader
      // 
      this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblHeader.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
      this.lblHeader.Location = new System.Drawing.Point(0, 0);
      this.lblHeader.Name = "lblHeader";
      this.lblHeader.Size = new System.Drawing.Size(860, 20);
      this.lblHeader.TabIndex = 0;
      this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // tpPK
      // 
      this.tpPK.Location = new System.Drawing.Point(4, 22);
      this.tpPK.Name = "tpPK";
      this.tpPK.Padding = new System.Windows.Forms.Padding(3);
      this.tpPK.Size = new System.Drawing.Size(866, 304);
      this.tpPK.TabIndex = 1;
      this.tpPK.Text = "Primary Key";
      this.tpPK.UseVisualStyleBackColor = true;
      // 
      // tpFK
      // 
      this.tpFK.Location = new System.Drawing.Point(4, 22);
      this.tpFK.Name = "tpFK";
      this.tpFK.Padding = new System.Windows.Forms.Padding(3);
      this.tpFK.Size = new System.Drawing.Size(866, 304);
      this.tpFK.TabIndex = 2;
      this.tpFK.Text = "Foreign Keys";
      this.tpFK.UseVisualStyleBackColor = true;
      // 
      // toolStrip1
      // 
      this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripSeparator1,
            this.btnPK});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(874, 25);
      this.toolStrip1.TabIndex = 3;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // toolStripButton1
      // 
      this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton1.Image = global::SQLManagement.Properties.Resources.Refresh;
      this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton1.Name = "toolStripButton1";
      this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton1.Text = "Refresh";
      this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
      // 
      // toolStripButton2
      // 
      this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton2.Image = global::SQLManagement.Properties.Resources.save;
      this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton2.Name = "toolStripButton2";
      this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton2.Text = "Save";
      this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // btnPK
      // 
      this.btnPK.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnPK.Image = global::SQLManagement.Properties.Resources.PrimaryKeyHS1;
      this.btnPK.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnPK.Name = "btnPK";
      this.btnPK.Size = new System.Drawing.Size(23, 22);
      this.btnPK.Text = "Primary Key";
      this.btnPK.Visible = false;
      this.btnPK.Click += new System.EventHandler(this.toolStripButton3_Click);
      // 
      // splitter1
      // 
      this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.splitter1.Location = new System.Drawing.Point(0, 304);
      this.splitter1.Name = "splitter1";
      this.splitter1.Size = new System.Drawing.Size(874, 3);
      this.splitter1.TabIndex = 4;
      this.splitter1.TabStop = false;
      // 
      // TableEdit
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.splitter1);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.toolStrip1);
      this.Name = "TableEdit";
      this.Size = new System.Drawing.Size(874, 637);
      this.panel1.ResumeLayout(false);
      this.gbTableDefinition.ResumeLayout(false);
      this.gbTableDefinition.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsCols)).EndInit();
      this.panel2.ResumeLayout(false);
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.gbAdditionalProps.ResumeLayout(false);
      this.gbAdditionalProps.PerformLayout();
      this.panel3.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.DataGridView grd;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tpPK;
    private System.Windows.Forms.BindingSource bsCols;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private System.Windows.Forms.ToolStripButton toolStripButton2;
    private System.Windows.Forms.ToolStripButton btnPK;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.GroupBox gbAdditionalProps;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.CheckBox chkComputed;
    private System.Windows.Forms.TextBox txtFormula;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtDescription;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ComboBox cmbRuleBinding;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox cmbDefaultBinding;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label lblAdditionalPropError;
    private System.Windows.Forms.GroupBox gbTableDefinition;
    private System.Windows.Forms.ComboBox cmbFileGroup;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.TextBox txtName;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.ComboBox cmbOwner;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.ComboBox cmbCollation;
		private System.Windows.Forms.TabPage tpFK;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.Label lblHeader;
		private System.Windows.Forms.DataGridViewImageColumn colPk;
		private System.Windows.Forms.DataGridViewTextBoxColumn colName;
		private System.Windows.Forms.DataGridViewComboBoxColumn colDataType;
		private System.Windows.Forms.DataGridViewTextBoxColumn colWidth;
		private System.Windows.Forms.DataGridViewTextBoxColumn colPrecision;
		private System.Windows.Forms.DataGridViewTextBoxColumn colScale;
		private System.Windows.Forms.DataGridViewCheckBoxColumn colAllowNull;
		private System.Windows.Forms.DataGridViewTextBoxColumn colDefault;
		private System.Windows.Forms.DataGridViewCheckBoxColumn colIdentity;
		private System.Windows.Forms.DataGridViewTextBoxColumn colSeed;
		private System.Windows.Forms.DataGridViewTextBoxColumn colIncrement;
		private System.Windows.Forms.Splitter splitter1;

  }
}
