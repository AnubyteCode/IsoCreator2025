using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using BER.CDCat.Export;
using System.IO;

namespace IsoCreator.Forms
{
    public partial class MainForm : Form
    {

        #region Fields 

        private Thread m_thread = null;
        private readonly IsoCreator m_creator = null;

        #endregion

        #region Constructors

        public MainForm()
        {
            InitializeComponent();
            //
            MicaHelper.ApplyMicaEffect(this);
            //
            textBoxFolder.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            textBoxIsoPath.Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ISO.iso");
            textBoxVolumeName.Text = "ISO";
            //
            m_creator = new IsoCreator();
            m_creator.Progress += new ProgressDelegate(Creator_Progress);
            m_creator.Finish += new FinishDelegate(Creator_Finished);
            m_creator.Abort += new AbortDelegate(Creator_Abort);
            //
            this.Icon = Properties.Resources.CDCat;
        }

        #endregion

        #region Set Delegates 

        private delegate void SetLabelDelegate(string text);
        private delegate void SetNumericValueDelegate(int value);

        #endregion

        #region Set Methods

        private void SetLabelStatus(string text)
        {
            this.labelStatus.Text = text;
            this.labelStatus.Refresh();
        }

        private void SetProgressValue(int value)
        {
            this.progressBar.Value = value;
        }

        private void SetProgressMaximum(int maximum)
        {
            this.progressBar.Maximum = maximum;
        }

        #endregion

        #region Event Handlers

        private void ButtonStartAbort_Click(object sender, EventArgs e)
        {
            if (m_thread == null || !m_thread.IsAlive)
            {
                if (textBoxVolumeName.Text.Trim() != "")
                {
                    m_thread = new Thread(new ParameterizedThreadStart(m_creator.Folder2Iso));
                    m_thread.Start(new IsoCreator.IsoCreatorFolderArgs(textBoxFolder.Text, textBoxIsoPath.Text, textBoxVolumeName.Text));

                    buttonStartAbort.Text = "Abort";
                }
                else
                {
                    CustomMessageBox.Show("Please insert a name for the volume", "No volume name", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            else
            {
                if (CustomMessageBox.Show("Are you sure you want to abort the process?", "Abort", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    m_thread.Abort();
                }
            }
        }

        void Creator_Abort(object sender, AbortEventArgs e)
        {
            CustomMessageBox.Show(e.Message, "Abort", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            CustomMessageBox.Show("The ISO creating process has been stopped.", "Abort", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            if (!this.InvokeRequired)
            {
                this.EnableStart();
                this.SetProgressMaximum(0);
            }
            else
            {
                this.Invoke(new Action(() =>
                {
                    this.EnableStart();
                    this.SetProgressMaximum(0);
                }));
            }
        }

        void Creator_Finished(object sender, FinishEventArgs e)
        {
            CustomMessageBox.Show(e.Message, "Finish", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (!this.InvokeRequired)
            {
                this.EnableStart();
            }
            else
            {
                this.Invoke(new Action(() =>
                {
                    this.EnableStart();
                }));
            }
        }

        private void EnableStart()
        {
            buttonStartAbort.Enabled = true;
            buttonStartAbort.Text = "Start";
            progressBar.Value = 0;
            labelStatus.Text = "Process not started";
        }

        void Creator_Progress(object sender, ProgressEventArgs e)
        {
            if (e.Action != null)
            {
                if (!this.InvokeRequired)
                {
                    this.SetLabelStatus(e.Action);
                }
                else
                {
                    this.Invoke(new SetLabelDelegate(SetLabelStatus), e.Action);
                }
            }

            if (e.Maximum != -1)
            {
                if (!this.InvokeRequired)
                {
                    this.SetProgressMaximum(e.Maximum);
                }
                else
                {
                    this.Invoke(new SetNumericValueDelegate(SetProgressMaximum), e.Maximum);
                }
            }

            if (!this.InvokeRequired)
            {
                progressBar.Value = (e.Current <= progressBar.Maximum) ? e.Current : progressBar.Maximum;
            }
            else
            {
                int value = (e.Current <= progressBar.Maximum) ? e.Current : progressBar.Maximum;
                this.Invoke(new SetNumericValueDelegate(SetProgressValue), value);
            }
        }

        private void ButtonBrowseFolder_Click(object sender, EventArgs e)
        {
            string folderPath = ModernFolderPicker.PickFolder();
            if (!string.IsNullOrEmpty(folderPath))
            {
                textBoxFolder.Text = folderPath;
            }
        }


        private void ButtonBrowseIso_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "CD Images (*.iso)|*.iso"
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                textBoxIsoPath.Text = dialog.FileName;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_creator != null && m_thread != null && m_thread.IsAlive)
            {
                m_thread.Abort();
            }
        }

        #endregion
    }
}