using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using BER.CDCat.Export;

namespace IsoCreator.Forms
{
    public partial class VirtualIsoCreator : Form
    {

        #region Fields

        private Thread m_thread;
        private readonly IsoCreator m_creator;

        #endregion

        #region Constructors

        public VirtualIsoCreator()
        {
            InitializeComponent();
            //
            MicaHelper.ApplyMicaEffect(this);
            //
            textBoxIsoPath.Text = @"C:\iso.iso";
            textBoxVolumeName.Text = "iso";
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
                    m_thread = new Thread(new ParameterizedThreadStart(m_creator.Tree2Iso));
                    BER.CDCat.Export.TreeNode volume = this.TreeView2TreeNode(this.treeView1);
                    m_thread.Start(new IsoCreator.IsoCreatorTreeArgs(volume, textBoxIsoPath.Text));

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
            buttonStartAbort.Enabled = true;
            buttonStartAbort.Text = "Start";
            progressBar.Value = 0;
            progressBar.Maximum = 0;
            labelStatus.Text = "Process not started";
        }

        void Creator_Finished(object sender, FinishEventArgs e)
        {
            CustomMessageBox.Show(e.Message, "Finish", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    labelStatus.Text = e.Action;
                    labelStatus.Refresh();
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
                    progressBar.Maximum = e.Maximum;
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

        private void VirtualIsoCreator_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_creator != null && m_thread != null && m_thread.IsAlive)
            {
                m_thread.Abort();
            }
        }

        #endregion

        #region TreeNode

        private void GetTreeNodeFromNode(BER.CDCat.Export.TreeNode dir, System.Windows.Forms.TreeNode node)
        {
            foreach (System.Windows.Forms.TreeNode nodeChild in node.Nodes)
            {
                BER.CDCat.Export.TreeNode child = new BER.CDCat.Export.TreeNode
                {
                    Name = nodeChild.Text,
                    Length = 10,
                    IsDirectory = (nodeChild.Nodes.Count > 0),
                    CreationTime = DateTime.Now
                };

                if (child.IsDirectory)
                {
                    GetTreeNodeFromNode(child, nodeChild);
                    dir.Directories.Add(child);
                }
                else
                {
                    dir.Files.Add(child);
                }


            }
        }

        private BER.CDCat.Export.TreeNode TreeView2TreeNode(TreeView treeView)
        {
            BER.CDCat.Export.TreeNode root = new BER.CDCat.Export.TreeNode
            {
                Name = textBoxVolumeName.Text,
                ShortName = textBoxVolumeName.Text.ToUpper(),
                IsDirectory = true,
                CreationTime = DateTime.Now
            };

            foreach (System.Windows.Forms.TreeNode node in treeView.Nodes)
            {
                BER.CDCat.Export.TreeNode child = new BER.CDCat.Export.TreeNode
                {
                    Name = node.Text,
                    Length = 10,
                    IsDirectory = (node.Nodes.Count > 0),
                    CreationTime = DateTime.Now
                };

                if (child.IsDirectory)
                {
                    root.Directories.Add(child);
                    GetTreeNodeFromNode(child, node);
                }
                else
                {
                    root.Files.Add(child);
                }
            }
            //
            return root;
        }

        #endregion
    }
}