using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public static class MicaHelper
{
    // Import DwmSetWindowAttribute for applying Mica
    [DllImport("dwmapi.dll", PreserveSig = true)]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

    // Import RtlGetVersion to detect OS version
    [DllImport("ntdll.dll", SetLastError = true)]
    private static extern int RtlGetVersion(out OSVERSIONINFOEX versionInfo);

    // Constants for window attributes
    private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20; // Dark mode
    private const int DWMWA_SYSTEMBACKDROP_TYPE = 38;     // Backdrop type
    private const int DWMSBT_MAINWINDOW = 2;             // Mica effect
    private const int DWMSBT_TABBEDWINDOW = 4;           // Transparent Mica

    // OS version structure
    [StructLayout(LayoutKind.Sequential)]
    private struct OSVERSIONINFOEX
    {
        public int dwOSVersionInfoSize;
        public int dwMajorVersion;
        public int dwMinorVersion;
        public int dwBuildNumber;
        public int dwPlatformId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szCSDVersion;
        public ushort wServicePackMajor;
        public ushort wServicePackMinor;
        public ushort wSuiteMask;
        public byte wProductType;
        public byte wReserved;
    }

    /// <summary>
    /// Applies Mica and dark mode to the specified form.
    /// </summary>
    /// <param name="form">The form to modify.</param>
    public static void ApplyMicaEffect(Form form)
    {
        if (form == null)
            throw new ArgumentNullException(nameof(form));

        var (osMajor, osBuild) = GetActualOSVersion();
        IntPtr hwnd = form.Handle;

        if (osMajor >= 10 && osBuild >= 22000) // Windows 11
        {
            // Apply dark mode to title bar
            int darkMode = 1;
            DwmSetWindowAttribute(hwnd, DWMWA_USE_IMMERSIVE_DARK_MODE, ref darkMode, sizeof(int));

            // Apply Mica (Tabbed Window for full transparency)
            int backdropType = DWMSBT_TABBEDWINDOW;
            DwmSetWindowAttribute(hwnd, DWMWA_SYSTEMBACKDROP_TYPE, ref backdropType, sizeof(int));
        }

        // Apply dark theme to form and controls
        form.BackColor = Color.FromArgb(32, 32, 32); // Dark gray background
        ApplyDarkThemeToControls(form);
    }

    // Import necessary Windows APIs
    [DllImport("uxtheme.dll", SetLastError = true, EntryPoint = "#135")]
    private static extern int SetPreferredAppMode(int appMode);

    [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
    private static extern int SetWindowTheme(IntPtr hWnd, string subAppName, string subIdList);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

    /// <summary>
    /// Recursively applies a dark theme to all controls in the form.
    /// </summary>
    private static void ApplyDarkThemeToControls(Control parent)
    {
        foreach (Control ctrl in parent.Controls)
        {
            if (ctrl is Button button)
            {
                button.BackColor = Color.FromArgb(64, 64, 64);
                button.ForeColor = Color.White;
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderColor = Color.FromArgb(80, 80, 80);
            }
            else if (ctrl is TextBox textBox)
            {
                textBox.BackColor = Color.FromArgb(40, 40, 40);
                textBox.ForeColor = Color.White;
                textBox.BorderStyle = BorderStyle.FixedSingle;
            }
            else if (ctrl is ProgressBar progressBar)
            {
                progressBar.BackColor = Color.FromArgb(55, 55, 55);
            }
            else if (ctrl is Label label)
            {
                label.ForeColor = Color.White;
            }
            else if (ctrl is GroupBox groupBox)
            {
                groupBox.ForeColor = Color.White;
            }

            // Recursively apply to child controls (for panels, group boxes, etc.)
            if (ctrl.HasChildren)
            {
                ApplyDarkThemeToControls(ctrl);
            }
        }
    }

    /// <summary>
    /// Retrieves the actual OS version using RtlGetVersion.
    /// </summary>
    private static (int Major, int Build) GetActualOSVersion()
    {
        OSVERSIONINFOEX osInfo = new OSVERSIONINFOEX();
        osInfo.dwOSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX));

        if (RtlGetVersion(out osInfo) == 0) // Success
        {
            return (osInfo.dwMajorVersion, osInfo.dwBuildNumber);
        }

        // Fallback if RtlGetVersion fails
        var osVersion = Environment.OSVersion;
        return (osVersion.Version.Major, osVersion.Version.Build);
    }

    // Constants for dark mode
    private const int ALLOW_DARK_MODE = 1;

    /// <summary>
    /// Enables dark mode for Windows dialogs, including the file picker.
    /// </summary>
    public static void EnableDarkMode()
    {
        // Enable dark mode for file dialogs
        SetPreferredAppMode(ALLOW_DARK_MODE);
    }

    /// <summary>
    /// Applies dark mode to the currently active window (e.g., file dialog).
    /// </summary>
    public static void ApplyDarkThemeToActiveWindow()
    {
        IntPtr hwnd = GetForegroundWindow();
        if (hwnd != IntPtr.Zero)
        {
            SetWindowTheme(hwnd, "DarkMode_Explorer", null);
        }
    }

    /// <summary>
    /// Opens a Win32 folder dialog in dark mode.
    /// </summary>
    public static string OpenDarkModeFolderDialog()
    {
        using (FolderBrowserDialog dialog = new FolderBrowserDialog())
        {
            // Show the dialog
            DialogResult result = dialog.ShowDialog();

            // Apply dark mode to the dialog
            ApplyDarkThemeToActiveWindow();

            return result == DialogResult.OK ? dialog.SelectedPath : string.Empty;
        }
    }
}
