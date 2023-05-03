using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopiaEIncollaDetector
{
    public partial class Form1 : Form
    {
        private string apiC = "";
        public Form1()
        {
            InitializeComponent();
        }
        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
        private void cambiaCodiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string value = "XXX";

            if (InputBox("Codice API", "Inserisci codice:", ref value) == DialogResult.OK)
            {
                apiC = value;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebClient wb = new WebClient();
            string json = wb.DownloadString("https://www.googleapis.com/customsearch/v1?key=" + apiC + "&cx=97eab5e72d021418b&q=\"" + textBox1.Text+"\"");

            JObject JsonDe = JsonConvert.DeserializeObject<JObject>(json);
            int num = Convert.ToInt32((string)JsonDe["searchInformation"]["totalResults"]);
            if (num > 5)
            {
                label2.Text = "Copiato! (" + num + " possibili siti)";
                label2.ForeColor = Color.Red;
            } else if (num < 5 && num != 0)
            {
                label2.Text = "Forse copiato (" + num + " siti)";
                label2.ForeColor = Color.Orange;
            } else if (num == 0)
            {
                label2.Text = "Scritto a mano";
                label2.ForeColor = Color.Green;
            }
        }
    }
}
