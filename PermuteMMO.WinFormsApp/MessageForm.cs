namespace PermuteMMO.WinFormsApp
{
    public partial class MessageForm : Form
    {
        public MessageForm()
        {
            InitializeComponent();
            Lib.ApiPermuter.Message += Message;
        }

        public void Message(string text)
        {
            textBox.Text += text + "\n";
        }
    }
}
