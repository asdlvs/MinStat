namespace Bonch.MinStat.ImportFileGenerator
{
    partial class Ribbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public Ribbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ribbon));
            this.ImportTab = this.Factory.CreateRibbonTab();
            this.ImportGroup = this.Factory.CreateRibbonGroup();
            this.ImportButton = this.Factory.CreateRibbonButton();
            this.ImportTab.SuspendLayout();
            this.ImportGroup.SuspendLayout();
            // 
            // ImportTab
            // 
            this.ImportTab.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.ImportTab.Groups.Add(this.ImportGroup);
            this.ImportTab.Label = "Кадры";
            this.ImportTab.Name = "ImportTab";
            // 
            // ImportGroup
            // 
            this.ImportGroup.Items.Add(this.ImportButton);
            this.ImportGroup.Label = "Импорт";
            this.ImportGroup.Name = "ImportGroup";
            // 
            // ImportButton
            // 
            this.ImportButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.ImportButton.Image = ((System.Drawing.Image)(resources.GetObject("ImportButton.Image")));
            this.ImportButton.Label = "Создать";
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.ShowImage = true;
            this.ImportButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ImportButton_Click);
            // 
            // Ribbon
            // 
            this.Name = "Ribbon";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.ImportTab);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon_Load);
            this.ImportTab.ResumeLayout(false);
            this.ImportTab.PerformLayout();
            this.ImportGroup.ResumeLayout(false);
            this.ImportGroup.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab ImportTab;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup ImportGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton ImportButton;
    }

    partial class ThisRibbonCollection
    {
        internal Ribbon Ribbon
        {
            get { return this.GetRibbon<Ribbon>(); }
        }
    }
}
