namespace Keyboard_Locker;

public partial class FrmKeyDetection : Form
{
    private string? _lastValue;
    public bool Detection;

    public FrmKeyDetection()
    {
        InitializeComponent();
    }

    public void Output(string str)
    {
        if (str == _lastValue) return;
        _lastValue = str;
        txtKey.Text += str + Environment.NewLine;
    }


    private void FrmKeyDetection_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        Hide();
        Program.FrmKeyDetection.Detection = false;
    }
}