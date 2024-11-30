using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        private Form1 form1;

        public Form2(Form1 form1Instance)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            form1 = form1Instance;
        }

        public string PreItemName
        {
            get { return itemname.Text; }
            set { itemname.Text = value; }
        }

        public string NewItemName
        {
            get { return newvaluetextbox.Text; }
            set { newvaluetextbox.Text = value; }
        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            try
            {
                string newValue = newvaluetextbox.Text;
                form1.EditValue(PreItemName, newValue);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
