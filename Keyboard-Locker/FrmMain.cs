using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Keyboard_Locker;

public partial class FrmMain : Form
{
    #region Import

    private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int id, LowLevelKeyboardProc? callback, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool UnhookWindowsHookEx(IntPtr hook);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr CallNextHookEx(IntPtr hook, int nCode, IntPtr wp, IntPtr lp);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string name);

    #endregion Import

    #region Struct

    [StructLayout(LayoutKind.Sequential)]
    private struct KeyboardDllHook
    {
        public readonly Keys key;
        private readonly int scanCode;
        private readonly int flags;
        private readonly int time;
        private readonly IntPtr extra;
    }

    #endregion Struct

    #region Variable

    private static IntPtr _ptrHook;
    private static LowLevelKeyboardProc? _keyProcess;
    private static bool _isLock;

    #endregion Variable

    public FrmMain()
    {
        InitializeComponent();
    }

    #region Load/Close

    private void FrmMain_Load(object sender, EventArgs e)
    {
        if (HookKeyboard())
        {
            KeysClass.LoadXml();
            return;
        }
        MessageBox.Show("Can not Hook Keyboard", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        Application.Exit();
    }

    private static bool HookKeyboard()
    {
        var mainModule = Process.GetCurrentProcess().MainModule;
        if (mainModule == null) return false;
        _keyProcess = CaptureKey;
        _ptrHook = SetWindowsHookEx(13, _keyProcess, GetModuleHandle(mainModule.ModuleName), 0);
        return true;
    }

    private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
    {
        UnhookWindowsHookEx(_ptrHook);
        if (!KeysClass.SaveXml(KeysClass.UnlockList, "List.xml"))
        {
            MessageBox.Show("Can not Save Settings", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        Dispose();
    }

    #endregion Load/Close

    private void btnLock_Click(object sender, EventArgs e)
    {
        _isLock = !_isLock;
        btnLock.Text = _isLock ? "UnLock" : "Lock";
        LockToolStripMenuItem.Text = _isLock ? "UnLock" : "Lock";
        notifyIcon1.ShowBalloonTip(0, Text, $"Keyboard now is {(_isLock ? "Lock" : "UnLock")}", ToolTipIcon.Info);

        GC.Collect();
    }

    private static IntPtr CaptureKey(int nCode, IntPtr wp, IntPtr lp)
    {
        if (nCode >= 0 && _isLock)
        {
            var objKey = (KeyboardDllHook)Marshal.PtrToStructure(lp, typeof(KeyboardDllHook))!;

            if (KeysClass.UnlockList.Contains(objKey.key))
            {
                return CallNextHookEx(_ptrHook, nCode, wp, lp);
            }

            //if (LockedList.Contains(objKey.key))
            //{
            //    return (IntPtr)1;
            //}

            return (IntPtr)1;
        }
        return CallNextHookEx(_ptrHook, nCode, wp, lp);
    }

    #region Form Size

    private void FrmMain_SizeChanged(object sender, EventArgs e)
    {
        if (WindowState == FormWindowState.Minimized)
        {
            Hide();
            ShowInTaskbar = false;
            WindowState = FormWindowState.Minimized;
            notifyIcon1.Tag = string.Empty;
            notifyIcon1.ShowBalloonTip(3000, Text, "Background running", ToolTipIcon.Info);
        }
    }

    private void ShowForm()
    {
        if (WindowState == FormWindowState.Minimized)
        {
            Show();
            ShowInTaskbar = true;
            WindowState = FormWindowState.Normal;
        }
    }

    #endregion Form Size

    #region Notify & Content

    private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        ShowForm();
    }

    private void OpenWindowToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ShowForm();
    }

    private void LockToolStripMenuItem_Click(object sender, EventArgs e)
    {
        btnLock.PerformClick();
    }

    private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    #endregion Notify & Content

    private void btnSettings_Click(object sender, EventArgs e)
    {
        var frmSettings = new FrmSettings();
        frmSettings.ShowDialog();
        frmSettings.Dispose();
    }
}
