using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public class ModernFolderPicker
{
    public static string PickFolder()
    {
        var dialog = (IFileOpenDialog)new FileOpenDialog();
        dialog.SetOptions(FOS.FOS_PICKFOLDERS | FOS.FOS_FORCEFILESYSTEM | FOS.FOS_NOCHANGEDIR);
        dialog.SetTitle("Select a Folder");

        int hr = dialog.Show(IntPtr.Zero);
        if (hr == 0) // S_OK
        {
            dialog.GetResult(out IShellItem shellItem);
            shellItem.GetDisplayName(SIGDN.SIGDN_FILESYSPATH, out IntPtr pszPath);

            string selectedPath = Marshal.PtrToStringAuto(pszPath);
            Marshal.FreeCoTaskMem(pszPath);
            return selectedPath;
        }

        return null; // User canceled or error occurred
    }

    #region COM Interfaces & Constants

    [ComImport]
    [Guid("D57C7288-D4AD-4768-BE02-9D969532D960")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IFileOpenDialog
    {
        [PreserveSig] int Show(IntPtr parent);
        void SetFileTypes(); // Not needed
        void SetFileTypeIndex(); // Not needed
        void GetFileTypeIndex(); // Not needed
        void Advise(); // Not needed
        void Unadvise(); // Not needed
        void SetOptions(FOS fos);
        void GetOptions(out FOS fos);
        void SetDefaultFolder(); // Not needed
        void SetFolder(); // Not needed
        void GetFolder(); // Not needed
        void GetCurrentSelection(); // Not needed
        void SetFileName(); // Not needed
        void GetFileName(); // Not needed
        void SetTitle([MarshalAs(UnmanagedType.LPWStr)] string title);
        void SetOkButtonLabel(); // Not needed
        void SetFileNameLabel(); // Not needed
        void GetResult(out IShellItem ppsi);
        void AddPlace(); // Not needed
        void SetDefaultExtension(); // Not needed
        void Close(); // Not needed
        void SetClientGuid(); // Not needed
        void ClearClientData(); // Not needed
        void SetFilter(); // Not needed
        void GetResults(); // Not needed
        void GetSelectedItems(); // Not needed
    }

    [ComImport]
    [Guid("DC1C5A9C-E88A-4DDE-A5A1-60F82A20AEF7")]
    private class FileOpenDialog { }

    [ComImport]
    [Guid("43826D1E-E718-42EE-BC55-A1E261C37BFE")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IShellItem
    {
        void BindToHandler(); // Not needed
        void GetParent(); // Not needed
        void GetDisplayName(SIGDN sigdnName, out IntPtr ppszName);
        void GetAttributes(); // Not needed
        void Compare(); // Not needed
    }

    private enum SIGDN : uint
    {
        SIGDN_FILESYSPATH = 0x80058000,
    }

    [Flags]
    private enum FOS : uint
    {
        FOS_PICKFOLDERS = 0x00000020,
        FOS_FORCEFILESYSTEM = 0x00000040,
        FOS_NOCHANGEDIR = 0x00000008,
    }

    #endregion
}
