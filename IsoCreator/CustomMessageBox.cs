using System;
using System.Drawing;
using System.Windows.Forms;

namespace IsoCreator.Forms
{
    public partial class CustomMessageBox : Form
    {
        public CustomMessageBox(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            // Set basic properties
            Text = title;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Width = 350;
            Height = 150;

            // Apply Mica to the message box
            MicaHelper.ApplyMicaEffect(this);

            // Set message label
            Label lblMessage = new Label
            {
                Text = message,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                ForeColor = Color.White
            };

            // Define button panel
            FlowLayoutPanel buttonPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.RightToLeft,
                Dock = DockStyle.Bottom,
                Padding = new Padding(10),
                AutoSize = true
            };

            // Create buttons dynamically
            if (buttons == MessageBoxButtons.OK || buttons == MessageBoxButtons.OKCancel)
            {
                buttonPanel.Controls.Add(CreateButton("OK", DialogResult.OK));
            }
            if (buttons == MessageBoxButtons.YesNo || buttons == MessageBoxButtons.YesNoCancel)
            {
                buttonPanel.Controls.Add(CreateButton("Yes", DialogResult.Yes));
                buttonPanel.Controls.Add(CreateButton("No", DialogResult.No));
            }
            if (buttons == MessageBoxButtons.OKCancel || buttons == MessageBoxButtons.YesNoCancel)
            {
                buttonPanel.Controls.Add(CreateButton("Cancel", DialogResult.Cancel));
            }

            // Set icon
            PictureBox iconBox = new PictureBox
            {
                Size = new Size(40, 40),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Margin = new Padding(10),
                Image = GetIconImage(icon)
            };

            // Layout
            TableLayoutPanel mainPanel = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 2,
                Dock = DockStyle.Fill
            };

            mainPanel.Controls.Add(iconBox, 0, 0);
            mainPanel.Controls.Add(lblMessage, 1, 0);
            mainPanel.Controls.Add(buttonPanel, 1, 1);
            Controls.Add(mainPanel);
        }

        private Button CreateButton(string text, DialogResult result)
        {
            return new Button
            {
                Text = text,
                DialogResult = result,
                AutoSize = true,
                BackColor = Color.FromArgb(64, 64, 64),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
        }

        private Image GetIconImage(MessageBoxIcon icon)
        {
            switch (icon)
            {
                case MessageBoxIcon.Information:
                    return SystemIcons.Information.ToBitmap();
                case MessageBoxIcon.Warning:
                    return SystemIcons.Warning.ToBitmap();
                case MessageBoxIcon.Error:
                    return SystemIcons.Error.ToBitmap();
                case MessageBoxIcon.Question:
                    return SystemIcons.Question.ToBitmap();
                default:
                    return null;
            }
        }

        public static DialogResult Show(string message, string title = "Message", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None)
        {
            using (CustomMessageBox box = new CustomMessageBox(message, title, buttons, icon))
            {
                return box.ShowDialog();
            }
        }
    }
}
