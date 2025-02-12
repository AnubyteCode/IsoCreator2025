using System;
using System.Collections.Generic;
using System.Text;

namespace BER.CDCat.Export
{

    /// <summary>
    /// This class implements the interface required by CDCat for its export plugins.
    /// It is used only when compiled as external library, and only by CDCat.
    /// </summary>
    public class ExportIso : BER.CDCat.Export.IExportPlugin
    {
        #region Fields

        private IsoCreator.IsoCreator m_creator = new IsoCreator.IsoCreator();
        private BER.CDCat.Export.TreeNode m_volume;
        private string m_fileName;

        #endregion

        #region Constructors

        public ExportIso()
        {
            m_creator.Progress += new ProgressDelegate(Creator_Progress);
            m_creator.Abort += new AbortDelegate(Creator_Abort);
            m_creator.Finish += new FinishDelegate(Creator_Finished);
        }

        void Creator_Finished(object sender, FinishEventArgs e)
        {
            this.Finished?.Invoke(sender, e);
        }

        void Creator_Abort(object sender, AbortEventArgs e)
        {
            this.Abort?.Invoke(sender, e);
        }

        void Creator_Progress(object sender, ProgressEventArgs e)
        {
            this.Progress?.Invoke(sender, e);
        }

        #endregion

        #region IExportPlugin Members

        public string ID
        {
            get
            {
                return "ExportISO";
            }
        }

        public string Name
        {
            get
            {
                return "ISO";
            }
        }

        public string Extension
        {
            get
            {
                return "iso";
            }
        }

        public string Description
        {
            get
            {
                return "CD image with virtual files";
            }
        }

        public TreeNode Volume
        {
            get
            {
                return m_volume;
            }
            set
            {
                m_volume = value;
            }
        }

        public string FileName
        {
            get
            {
                return m_fileName;
            }
            set
            {
                m_fileName = value;
            }
        }

        public void DoExport()
        {
            if (m_volume == null || m_fileName == null)
            {
                return;
            }

            m_creator.Tree2Iso(m_volume, m_fileName);
        }

        public void DoExport(BER.CDCat.Export.TreeNode volume, string fileName)
        {
            m_volume = volume;
            m_fileName = fileName;
            m_creator.Tree2Iso(m_volume, m_fileName);
        }

        #endregion

        #region Events

        public event ProgressDelegate Progress;

        public event FinishDelegate Finished;

        public event AbortDelegate Abort;

        #endregion

        #region Override

        public override string ToString()
        {
            return this.Name;
        }

        #endregion
    }
}
