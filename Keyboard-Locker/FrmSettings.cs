namespace Keyboard_Locker;

public partial class FrmSettings : Form
{
    public FrmSettings()
    {
        InitializeComponent();
    }

    private void FrmSettings_Shown(object sender, EventArgs e)
    {
        if (KeysClass.UnlockList is { Count: > 0 })
        {
            var btnList = GetControls(this);
            foreach (var key in KeysClass.UnlockList)
            {
                foreach (var btn in btnList.Where(btn => key.ToString() == btn.Name.Replace("btn", "")))
                {
                    LockThis((Button)btn, key, false);
                }
            }
        }

        if (KeysClass.CustomUnlockList is { Count: > 0 })
        {
            foreach (var key in KeysClass.CustomUnlockList)
            {
                cbCustomKey.Items.Add(key.ToString());
            }
        }
    }

    private static void LockThis(ButtonBase btn, Keys key, bool enable)
    {
        if (!enable)
        {
            btn.BackColor = Color.LimeGreen;
            btn.FlatAppearance.MouseOverBackColor = Color.Lime;
            btn.FlatAppearance.MouseDownBackColor = Color.Green;
            if (key.IsDuplicate(KeysClass.UnlockList)) return;
            KeysClass.UnlockList.Add(key);
        }
        else
        {
            btn.BackColor = Color.Red;
            btn.FlatAppearance.MouseOverBackColor = Color.OrangeRed;
            btn.FlatAppearance.MouseDownBackColor = Color.DarkRed;
            KeysClass.UnlockList.Remove(key);
        }
    }

    private void ClickButtons(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        var keyName = btn.Name.Replace("btn", "");
        var key = KeysClass.String2Key(keyName);
        var result = key.IsDuplicate(KeysClass.UnlockList);
        LockThis(btn, key, result);
    }

    public static List<Control> GetControls(Form form)
    {
        var result = new List<Control>();
        foreach (var obj in form.Controls)
        {
            var control = (Control)obj;
            string[] ctrl = { "Button", "CheckBox", "ComboBox", "DataGridView", "Label", "LinkLabel", "ListBox", "ListView", "RadioButton", "RichTextBox", "TextBox" };
            string[] ctrlChild = { "GroupBox", "Panel", "TabPage" };

            if (CheckController(control, ctrl))
            {
                result.Add(control);
            }
            if (CheckController(control, ctrlChild))
            {
                result.AddRange(GetChildControls(control));
            }
        }

        return result;
    }

    public static List<Control> GetChildControls(Control control)
    {
        var result = new List<Control>();
        foreach (var obj in control.Controls)
        {
            var controlChild = (Control)obj;
            string[] ctrl = { "Button", "CheckBox", "ComboBox", "DataGridView", "Label", "LinkLabel", "ListBox", "ListView", "RadioButton", "RichTextBox", "TextBox" };
            string[] ctrlChild = { "GroupBox", "Panel", "TabPage" };

            if (CheckController(controlChild, ctrl))
            {
                result.Add(controlChild);
            }
            if (CheckController(controlChild, ctrlChild))
            {
                result.AddRange(GetChildControls(controlChild));
            }
        }

        return result;
    }

    private static bool CheckController(Control control, IEnumerable<string> ends)
    {
        return ends.Any(t => control.GetType().Name.EndsWith(t, StringComparison.OrdinalIgnoreCase));
    }

    private void btnAddKey_Click(object sender, EventArgs e)
    {
        cbCustomKey.Items.Add(txtCustomKey.Text);
        KeysClass.CustomUnlockList.Add(KeysClass.String2Key(txtCustomKey.Text));
        cbCustomKey.SelectedIndex = cbCustomKey.Items.Count - 1;
        txtCustomKey.Clear();
    }

    private void btnRemoveKey_Click(object sender, EventArgs e)
    {
        cbCustomKey.Items.RemoveAt(cbCustomKey.SelectedIndex);
        KeysClass.CustomUnlockList.RemoveAt(cbCustomKey.SelectedIndex);
        txtCustomKey.Clear();
    }
}