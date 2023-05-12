using System.Reflection.PortableExecutable;

namespace oriwotw_save_tool
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
            this.selectInitialLabel = new System.Windows.Forms.Label();
            this.initialFileSelectionTable = new System.Windows.Forms.TableLayoutPanel();
            this.chooseInitialFilePaddingPanel = new System.Windows.Forms.Panel();
            this.readOnlyInitialFileTextBox = new System.Windows.Forms.TextBox();
            this.chooseInitialFileButton = new System.Windows.Forms.Button();
            this.basicControlsLabel = new System.Windows.Forms.Label();
            this.basicControlsTable = new System.Windows.Forms.TableLayoutPanel();
            this.downShiftButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.upShiftButton = new System.Windows.Forms.Button();
            this.selectReplacementLabel = new System.Windows.Forms.Label();
            this.replacementFileSelectionTable = new System.Windows.Forms.TableLayoutPanel();
            this.chooseReplacementFilePaddingPanel = new System.Windows.Forms.Panel();
            this.readonlyReplacementFileTextBox = new System.Windows.Forms.TextBox();
            this.chooseReplacementFileButton = new System.Windows.Forms.Button();
            this.basePanel = new System.Windows.Forms.Panel();
            this.statusTextLabel = new System.Windows.Forms.Label();
            this.selectReplaceOrInsertTable = new System.Windows.Forms.TableLayoutPanel();
            this.insertButton = new System.Windows.Forms.Button();
            this.replaceButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.selectReplaceOrInsertActionLabel = new System.Windows.Forms.Label();
            this.nameRepInsAddPaddingPanel = new System.Windows.Forms.Panel();
            this.nameRepInsAddTextBox = new System.Windows.Forms.TextBox();
            this.nameRepInsAddLabel = new System.Windows.Forms.Label();
            this.initialFileSelectionTable.SuspendLayout();
            this.chooseInitialFilePaddingPanel.SuspendLayout();
            this.basicControlsTable.SuspendLayout();
            this.replacementFileSelectionTable.SuspendLayout();
            this.chooseReplacementFilePaddingPanel.SuspendLayout();
            this.basePanel.SuspendLayout();
            this.selectReplaceOrInsertTable.SuspendLayout();
            this.nameRepInsAddPaddingPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // selectInitialLabel
            // 
            this.selectInitialLabel.AutoSize = true;
            this.selectInitialLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.selectInitialLabel.Location = new System.Drawing.Point(6, 6);
            this.selectInitialLabel.Margin = new System.Windows.Forms.Padding(0);
            this.selectInitialLabel.Name = "selectInitialLabel";
            this.selectInitialLabel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.selectInitialLabel.Size = new System.Drawing.Size(140, 18);
            this.selectInitialLabel.TabIndex = 0;
            this.selectInitialLabel.Text = "Select file to replace/shift";
            // 
            // initialFileSelectionTable
            // 
            this.initialFileSelectionTable.AutoSize = true;
            this.initialFileSelectionTable.ColumnCount = 2;
            this.initialFileSelectionTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.initialFileSelectionTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.initialFileSelectionTable.Controls.Add(this.chooseInitialFilePaddingPanel, 0, 0);
            this.initialFileSelectionTable.Controls.Add(this.chooseInitialFileButton, 0, 0);
            this.initialFileSelectionTable.Dock = System.Windows.Forms.DockStyle.Top;
            this.initialFileSelectionTable.Location = new System.Drawing.Point(6, 24);
            this.initialFileSelectionTable.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.initialFileSelectionTable.Name = "initialFileSelectionTable";
            this.initialFileSelectionTable.RowCount = 1;
            this.initialFileSelectionTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.initialFileSelectionTable.Size = new System.Drawing.Size(322, 33);
            this.initialFileSelectionTable.TabIndex = 9;
            // 
            // chooseInitialFilePaddingPanel
            // 
            this.chooseInitialFilePaddingPanel.AutoSize = true;
            this.chooseInitialFilePaddingPanel.Controls.Add(this.readOnlyInitialFileTextBox);
            this.chooseInitialFilePaddingPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.chooseInitialFilePaddingPanel.Location = new System.Drawing.Point(100, 3);
            this.chooseInitialFilePaddingPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chooseInitialFilePaddingPanel.Name = "chooseInitialFilePaddingPanel";
            this.chooseInitialFilePaddingPanel.Padding = new System.Windows.Forms.Padding(4, 1, 4, 3);
            this.chooseInitialFilePaddingPanel.Size = new System.Drawing.Size(218, 27);
            this.chooseInitialFilePaddingPanel.TabIndex = 8;
            // 
            // readOnlyInitialFileTextBox
            // 
            this.readOnlyInitialFileTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.readOnlyInitialFileTextBox.Location = new System.Drawing.Point(4, 1);
            this.readOnlyInitialFileTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.readOnlyInitialFileTextBox.Name = "readOnlyInitialFileTextBox";
            this.readOnlyInitialFileTextBox.ReadOnly = true;
            this.readOnlyInitialFileTextBox.Size = new System.Drawing.Size(210, 23);
            this.readOnlyInitialFileTextBox.TabIndex = 0;
            // 
            // chooseInitialFileButton
            // 
            this.chooseInitialFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chooseInitialFileButton.Location = new System.Drawing.Point(4, 3);
            this.chooseInitialFileButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chooseInitialFileButton.Name = "chooseInitialFileButton";
            this.chooseInitialFileButton.Size = new System.Drawing.Size(88, 27);
            this.chooseInitialFileButton.TabIndex = 0;
            this.chooseInitialFileButton.Text = "Choose";
            this.chooseInitialFileButton.UseVisualStyleBackColor = true;
            this.chooseInitialFileButton.Click += new System.EventHandler(this.ChooseInitialFileButton_Click);
            // 
            // basicControlsLabel
            // 
            this.basicControlsLabel.AutoSize = true;
            this.basicControlsLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.basicControlsLabel.Location = new System.Drawing.Point(6, 57);
            this.basicControlsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.basicControlsLabel.Name = "basicControlsLabel";
            this.basicControlsLabel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.basicControlsLabel.Size = new System.Drawing.Size(139, 21);
            this.basicControlsLabel.TabIndex = 10;
            this.basicControlsLabel.Text = "Basic controls for this file";
            // 
            // basicControlsTable
            // 
            this.basicControlsTable.AutoSize = true;
            this.basicControlsTable.ColumnCount = 3;
            this.basicControlsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.basicControlsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.basicControlsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.basicControlsTable.Controls.Add(this.downShiftButton, 0, 0);
            this.basicControlsTable.Controls.Add(this.deleteButton, 1, 0);
            this.basicControlsTable.Controls.Add(this.upShiftButton, 2, 0);
            this.basicControlsTable.Dock = System.Windows.Forms.DockStyle.Top;
            this.basicControlsTable.Location = new System.Drawing.Point(6, 78);
            this.basicControlsTable.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.basicControlsTable.Name = "basicControlsTable";
            this.basicControlsTable.RowCount = 1;
            this.basicControlsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.basicControlsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.basicControlsTable.Size = new System.Drawing.Size(322, 33);
            this.basicControlsTable.TabIndex = 11;
            // 
            // downShiftButton
            // 
            this.downShiftButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.downShiftButton.Location = new System.Drawing.Point(4, 3);
            this.downShiftButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.downShiftButton.Name = "downShiftButton";
            this.downShiftButton.Size = new System.Drawing.Size(99, 27);
            this.downShiftButton.TabIndex = 0;
            this.downShiftButton.Text = "Down (-1)";
            this.downShiftButton.UseVisualStyleBackColor = true;
            this.downShiftButton.Click += new System.EventHandler(this.DownShiftButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteButton.Location = new System.Drawing.Point(111, 3);
            this.deleteButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(99, 27);
            this.deleteButton.TabIndex = 1;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // upShiftButton
            // 
            this.upShiftButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.upShiftButton.Location = new System.Drawing.Point(218, 3);
            this.upShiftButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.upShiftButton.Name = "upShiftButton";
            this.upShiftButton.Size = new System.Drawing.Size(100, 27);
            this.upShiftButton.TabIndex = 2;
            this.upShiftButton.Text = "Up (+1)";
            this.upShiftButton.UseVisualStyleBackColor = true;
            this.upShiftButton.Click += new System.EventHandler(this.UpShiftButton_Click);
            // 
            // selectReplacementLabel
            // 
            this.selectReplacementLabel.AutoSize = true;
            this.selectReplacementLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.selectReplacementLabel.Location = new System.Drawing.Point(6, 111);
            this.selectReplacementLabel.Margin = new System.Windows.Forms.Padding(0);
            this.selectReplacementLabel.Name = "selectReplacementLabel";
            this.selectReplacementLabel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.selectReplacementLabel.Size = new System.Drawing.Size(207, 21);
            this.selectReplacementLabel.TabIndex = 12;
            this.selectReplacementLabel.Text = "Select replacement/insertion/addition";
            // 
            // replacementFileSelectionTable
            // 
            this.replacementFileSelectionTable.AutoSize = true;
            this.replacementFileSelectionTable.ColumnCount = 2;
            this.replacementFileSelectionTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.replacementFileSelectionTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.replacementFileSelectionTable.Controls.Add(this.chooseReplacementFilePaddingPanel, 0, 0);
            this.replacementFileSelectionTable.Controls.Add(this.chooseReplacementFileButton, 0, 0);
            this.replacementFileSelectionTable.Dock = System.Windows.Forms.DockStyle.Top;
            this.replacementFileSelectionTable.Location = new System.Drawing.Point(6, 132);
            this.replacementFileSelectionTable.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.replacementFileSelectionTable.Name = "replacementFileSelectionTable";
            this.replacementFileSelectionTable.RowCount = 1;
            this.replacementFileSelectionTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.replacementFileSelectionTable.Size = new System.Drawing.Size(322, 33);
            this.replacementFileSelectionTable.TabIndex = 13;
            // 
            // chooseReplacementFilePaddingPanel
            // 
            this.chooseReplacementFilePaddingPanel.AutoSize = true;
            this.chooseReplacementFilePaddingPanel.Controls.Add(this.readonlyReplacementFileTextBox);
            this.chooseReplacementFilePaddingPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.chooseReplacementFilePaddingPanel.Location = new System.Drawing.Point(100, 3);
            this.chooseReplacementFilePaddingPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chooseReplacementFilePaddingPanel.Name = "chooseReplacementFilePaddingPanel";
            this.chooseReplacementFilePaddingPanel.Padding = new System.Windows.Forms.Padding(4, 1, 4, 3);
            this.chooseReplacementFilePaddingPanel.Size = new System.Drawing.Size(218, 27);
            this.chooseReplacementFilePaddingPanel.TabIndex = 8;
            // 
            // readonlyReplacementFileTextBox
            // 
            this.readonlyReplacementFileTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.readonlyReplacementFileTextBox.Location = new System.Drawing.Point(4, 1);
            this.readonlyReplacementFileTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.readonlyReplacementFileTextBox.Name = "readonlyReplacementFileTextBox";
            this.readonlyReplacementFileTextBox.ReadOnly = true;
            this.readonlyReplacementFileTextBox.Size = new System.Drawing.Size(210, 23);
            this.readonlyReplacementFileTextBox.TabIndex = 0;
            // 
            // chooseReplacementFileButton
            // 
            this.chooseReplacementFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chooseReplacementFileButton.Location = new System.Drawing.Point(4, 3);
            this.chooseReplacementFileButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chooseReplacementFileButton.Name = "chooseReplacementFileButton";
            this.chooseReplacementFileButton.Size = new System.Drawing.Size(88, 27);
            this.chooseReplacementFileButton.TabIndex = 0;
            this.chooseReplacementFileButton.Text = "Choose";
            this.chooseReplacementFileButton.UseVisualStyleBackColor = true;
            this.chooseReplacementFileButton.Click += new System.EventHandler(this.ChooseReplacementFileButton_Click);
            // 
            // basePanel
            // 
            this.basePanel.AutoSize = true;
            this.basePanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.basePanel.Controls.Add(this.statusTextLabel);
            this.basePanel.Controls.Add(this.selectReplaceOrInsertTable);
            this.basePanel.Controls.Add(this.selectReplaceOrInsertActionLabel);
            this.basePanel.Controls.Add(this.nameRepInsAddPaddingPanel);
            this.basePanel.Controls.Add(this.nameRepInsAddLabel);
            this.basePanel.Controls.Add(this.replacementFileSelectionTable);
            this.basePanel.Controls.Add(this.selectReplacementLabel);
            this.basePanel.Controls.Add(this.basicControlsTable);
            this.basePanel.Controls.Add(this.basicControlsLabel);
            this.basePanel.Controls.Add(this.initialFileSelectionTable);
            this.basePanel.Controls.Add(this.selectInitialLabel);
            this.basePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.basePanel.Location = new System.Drawing.Point(0, 0);
            this.basePanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.basePanel.Name = "basePanel";
            this.basePanel.Padding = new System.Windows.Forms.Padding(6);
            this.basePanel.Size = new System.Drawing.Size(334, 301);
            this.basePanel.TabIndex = 2;
            // 
            // statusTextLabel
            // 
            this.statusTextLabel.AutoSize = true;
            this.statusTextLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.statusTextLabel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.statusTextLabel.Location = new System.Drawing.Point(6, 269);
            this.statusTextLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.statusTextLabel.MinimumSize = new System.Drawing.Size(200, 0);
            this.statusTextLabel.Name = "statusTextLabel";
            this.statusTextLabel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.statusTextLabel.Size = new System.Drawing.Size(200, 18);
            this.statusTextLabel.TabIndex = 19;
            this.statusTextLabel.Text = "Fuck you";
            this.statusTextLabel.Click += new System.EventHandler(this.statusTextLabel_Click);
            // 
            // selectReplaceOrInsertTable
            // 
            this.selectReplaceOrInsertTable.AutoSize = true;
            this.selectReplaceOrInsertTable.ColumnCount = 3;
            this.selectReplaceOrInsertTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33444F));
            this.selectReplaceOrInsertTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33444F));
            this.selectReplaceOrInsertTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33112F));
            this.selectReplaceOrInsertTable.Controls.Add(this.insertButton, 0, 0);
            this.selectReplaceOrInsertTable.Controls.Add(this.replaceButton, 0, 0);
            this.selectReplaceOrInsertTable.Controls.Add(this.addButton, 1, 0);
            this.selectReplaceOrInsertTable.Dock = System.Windows.Forms.DockStyle.Top;
            this.selectReplaceOrInsertTable.Location = new System.Drawing.Point(6, 236);
            this.selectReplaceOrInsertTable.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.selectReplaceOrInsertTable.Name = "selectReplaceOrInsertTable";
            this.selectReplaceOrInsertTable.RowCount = 1;
            this.selectReplaceOrInsertTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.selectReplaceOrInsertTable.Size = new System.Drawing.Size(322, 33);
            this.selectReplaceOrInsertTable.TabIndex = 18;
            // 
            // insertButton
            // 
            this.insertButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.insertButton.Location = new System.Drawing.Point(111, 3);
            this.insertButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.insertButton.Name = "insertButton";
            this.insertButton.Size = new System.Drawing.Size(99, 27);
            this.insertButton.TabIndex = 2;
            this.insertButton.Text = "Insert";
            this.insertButton.UseVisualStyleBackColor = true;
            // 
            // replaceButton
            // 
            this.replaceButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.replaceButton.Location = new System.Drawing.Point(4, 3);
            this.replaceButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.replaceButton.Name = "replaceButton";
            this.replaceButton.Size = new System.Drawing.Size(99, 27);
            this.replaceButton.TabIndex = 0;
            this.replaceButton.Text = "Replace";
            this.replaceButton.UseVisualStyleBackColor = true;
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addButton.Location = new System.Drawing.Point(218, 3);
            this.addButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(100, 27);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            // 
            // selectReplaceOrInsertActionLabel
            // 
            this.selectReplaceOrInsertActionLabel.AutoSize = true;
            this.selectReplaceOrInsertActionLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.selectReplaceOrInsertActionLabel.Location = new System.Drawing.Point(6, 215);
            this.selectReplaceOrInsertActionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.selectReplaceOrInsertActionLabel.Name = "selectReplaceOrInsertActionLabel";
            this.selectReplaceOrInsertActionLabel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.selectReplaceOrInsertActionLabel.Size = new System.Drawing.Size(74, 21);
            this.selectReplaceOrInsertActionLabel.TabIndex = 17;
            this.selectReplaceOrInsertActionLabel.Text = "Select action";
            // 
            // nameRepInsAddPaddingPanel
            // 
            this.nameRepInsAddPaddingPanel.AutoSize = true;
            this.nameRepInsAddPaddingPanel.Controls.Add(this.nameRepInsAddTextBox);
            this.nameRepInsAddPaddingPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.nameRepInsAddPaddingPanel.Location = new System.Drawing.Point(6, 186);
            this.nameRepInsAddPaddingPanel.Name = "nameRepInsAddPaddingPanel";
            this.nameRepInsAddPaddingPanel.Padding = new System.Windows.Forms.Padding(3);
            this.nameRepInsAddPaddingPanel.Size = new System.Drawing.Size(322, 29);
            this.nameRepInsAddPaddingPanel.TabIndex = 15;
            // 
            // nameRepInsAddTextBox
            // 
            this.nameRepInsAddTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.nameRepInsAddTextBox.Location = new System.Drawing.Point(3, 3);
            this.nameRepInsAddTextBox.Name = "nameRepInsAddTextBox";
            this.nameRepInsAddTextBox.Size = new System.Drawing.Size(316, 23);
            this.nameRepInsAddTextBox.TabIndex = 0;
            // 
            // nameRepInsAddLabel
            // 
            this.nameRepInsAddLabel.AutoSize = true;
            this.nameRepInsAddLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.nameRepInsAddLabel.Location = new System.Drawing.Point(6, 165);
            this.nameRepInsAddLabel.Name = "nameRepInsAddLabel";
            this.nameRepInsAddLabel.Padding = new System.Windows.Forms.Padding(3);
            this.nameRepInsAddLabel.Size = new System.Drawing.Size(214, 21);
            this.nameRepInsAddLabel.TabIndex = 14;
            this.nameRepInsAddLabel.Text = "Name replacement/insertion/addition";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(334, 301);
            this.Controls.Add(this.basePanel);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximumSize = new System.Drawing.Size(400, 340);
            this.MinimumSize = new System.Drawing.Size(350, 340);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.initialFileSelectionTable.ResumeLayout(false);
            this.initialFileSelectionTable.PerformLayout();
            this.chooseInitialFilePaddingPanel.ResumeLayout(false);
            this.chooseInitialFilePaddingPanel.PerformLayout();
            this.basicControlsTable.ResumeLayout(false);
            this.replacementFileSelectionTable.ResumeLayout(false);
            this.replacementFileSelectionTable.PerformLayout();
            this.chooseReplacementFilePaddingPanel.ResumeLayout(false);
            this.chooseReplacementFilePaddingPanel.PerformLayout();
            this.basePanel.ResumeLayout(false);
            this.basePanel.PerformLayout();
            this.selectReplaceOrInsertTable.ResumeLayout(false);
            this.nameRepInsAddPaddingPanel.ResumeLayout(false);
            this.nameRepInsAddPaddingPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel basePanel;

        private Label selectInitialLabel;
        private TableLayoutPanel initialFileSelectionTable;
        private Panel chooseInitialFilePaddingPanel;
        private TextBox readOnlyInitialFileTextBox;
        private Button chooseInitialFileButton;

        private Label basicControlsLabel;
        private TableLayoutPanel basicControlsTable;
        private Button downShiftButton;
        private Button deleteButton;
        private Button upShiftButton;

        private Label selectReplacementLabel;
        private TableLayoutPanel replacementFileSelectionTable;
        private Panel chooseReplacementFilePaddingPanel;
        private TextBox readonlyReplacementFileTextBox;
        private Button chooseReplacementFileButton;

        private TableLayoutPanel selectReplaceOrInsertTable;
        private Button insertButton;
        private Button replaceButton;
        private Button addButton;

        private Label selectReplaceOrInsertActionLabel;
        private Panel nameRepInsAddPaddingPanel;
        private TextBox nameRepInsAddTextBox;
        private Label nameRepInsAddLabel;

        private Label statusTextLabel;
    }
}