using _4Time.DataCore;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Security;
using System.Windows.Forms;

namespace Time4SellersApp;

public class StringInputForm : Form
{
    private TextBox _inputBox;
    private Button _okButton;
    private Label _promptLabel;

    public string? Result { get; private set; }

    public StringInputForm(string title, string prompt)
    {
        this.Text = title;
        this.Width = 300;
        this.Height = 150;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.StartPosition = FormStartPosition.CenterScreen;
        _ = this.Focus();

        _promptLabel = new Label { Left = 10, Top = 10, Text = prompt, Width = 260 };
        _inputBox = new TextBox { Left = 10, Top = 35, Width = 260, PasswordChar = '*' };
        _okButton = new Button { Text = "OK", Left = 200, Width = 70, Top = 70 };

        _okButton.Click += (sender, e) =>
        {
            Result = _inputBox.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        };

        this.Controls.Add(_promptLabel);
        this.Controls.Add(_inputBox);
        this.Controls.Add(_okButton);
    }

    public sealed override string Text
    {
        get => base.Text;
        set => base.Text = value;
    }

    public static string? ShowStringInputForm(string title, string prompt)
    {
        using var form = new StringInputForm(title, prompt);

        // Make form.Result to a Secure String and save it securely
        SecureString My4SELLERSpwd = new SecureString();
        foreach (char c in form._inputBox.Text)
        {
            My4SELLERSpwd.AppendChar(c);
        }
        WindowsCredentialManager.SavePassword("4Time/My4SELLERSpwd", My4SELLERSpwd, WindowsCredentialManager.CRED_PERSIST.LOCAL_MACHINE);

        return form.ShowDialog() == DialogResult.OK ? form.Result : null;
    }
}