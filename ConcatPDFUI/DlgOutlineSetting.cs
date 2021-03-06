using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using System.Collections.Specialized;
using Ujihara.PDF;

namespace Ujihara.ConcatPDF
{
	public class DlgOutlineSetting : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboLocZoom;
		private System.Windows.Forms.CheckBox checkAddOutlines;
		private System.Windows.Forms.CheckBox checkCopyOutlines;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.ComponentModel.Container components = null;

		public DlgOutlineSetting()
		{
			InitializeComponent();

			//
			// Constructor Codes after InitializeComponent
			//

			Init();
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Codes generated by Windows Form Designer 
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgOutlineSetting));
            this.comboLocZoom = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkAddOutlines = new System.Windows.Forms.CheckBox();
            this.checkCopyOutlines = new System.Windows.Forms.CheckBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboLocZoom
            // 
            resources.ApplyResources(this.comboLocZoom, "comboLocZoom");
            this.comboLocZoom.Name = "comboLocZoom";
            this.comboLocZoom.Validated += new System.EventHandler(this.comboLocZoom_Validated);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // checkAddOutlines
            // 
            resources.ApplyResources(this.checkAddOutlines, "checkAddOutlines");
            this.checkAddOutlines.Name = "checkAddOutlines";
            // 
            // checkCopyOutlines
            // 
            resources.ApplyResources(this.checkCopyOutlines, "checkCopyOutlines");
            this.checkCopyOutlines.Name = "checkCopyOutlines";
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // DlgOutlineSetting
            // 
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.buttonCancel;
            this.ControlBox = false;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.checkCopyOutlines);
            this.Controls.Add(this.checkAddOutlines);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboLocZoom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DlgOutlineSetting";
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

		}
		#endregion

		private ArrayList destTags = new ArrayList();
		private ArrayList destDisps = new ArrayList();

		private void Init()
		{
			AddDestTag("/XYZ null null null", "Retain.");
			AddDestTag("/Fit", "Fit the entire page.");
			AddDestTag("/FitB", "Fit the bounding box.");
			AddDestTag("/FitH null", "Fit the entire width of the page.");
			AddDestTag("/FitV null", "Fit the entire height of the page.");
			//AddDestTag("/FitR null null null null", "Fit the rectangle specified by the coordinates.");
			AddDestTag("/FitBH null", "Fit the entire width of its bounding box.");
			AddDestTag("/FitBV null", "Fit the entire height of its bounding box.");
		}

		private void AddDestTag(string tag, string display)
		{
			destTags.Add(tag);
			destDisps.Add(display);
			comboLocZoom.Items.Add(display);
		}

		private void buttonOK_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}

		private string PDFTagToDisplay(string text)
		{
			if (destTags.Contains(text))
			{
				int idx = destTags.IndexOf(text);
				return (string)destDisps[idx];
			}
			return text;
		}

		private string PDFDisplayToTag(string text)
		{
			if (destDisps.Contains(text))
			{
				int idx = destDisps.IndexOf(text);
				return (string)destTags[idx];
			}
			return text;
		}

		private void comboLocZoom_Validated(object sender, System.EventArgs e)
		{
			string original = comboLocZoom.Text.Trim();
			string evaluted = PDFTagToDisplay(original);
			if (evaluted != comboLocZoom.Text)
				comboLocZoom.Text = evaluted;
		}

		public void GetSetting(PdfConcatenatorOption setting)
        {
			setting.AddOutlines = this.checkAddOutlines.Checked;
			setting.CopyOutlines = this.checkCopyOutlines.Checked;
			setting.FittingStyle = PDFDisplayToTag(this.comboLocZoom.Text);
        }

        public void SetSetting(PdfConcatenatorOption value)
        {
			this.checkAddOutlines.Checked = value.AddOutlines;
			this.checkCopyOutlines.Checked = value.CopyOutlines;
			this.comboLocZoom.Text = PDFTagToDisplay(value.FittingStyle);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
	}
}
