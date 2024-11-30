using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string[] todoarr = null;
        public string json_data = null;
        public string file_path = "todos.json";
        public bool isFirstActionComplete = false;

        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            isFirstActionComplete = false;
            try
            {
                if (File.Exists(file_path))
                {
                    if (File.ReadAllText(file_path).Length == 0)
                    {
                        File.WriteAllText(file_path, json_data);
                    }

                    listBox1.Items.Clear();
                    todoarr = JsonSerializer.Deserialize<string[]>(File.ReadAllText(file_path));
                    listBox1.Items.AddRange(todoarr);
                    listBox1.Refresh();
                    label2.Text = "Database Path: " + Path.GetFullPath(file_path);
                }
                else
                {
                    File.Create(file_path).Close();
                    label2.Text = "Database Path: " + Path.GetFullPath(file_path);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                listBox1.Items.Add(textBox1.Text);
                listBox1.Refresh();
                textBox1.Clear();
                UpdateTodoArray();
            }
        }

        private void UpdateTodoArray()
        {
            todoarr = new string[listBox1.Items.Count];
            listBox1.Items.CopyTo(todoarr, 0);

            json_data = JsonSerializer.Serialize(todoarr);

            if (isFirstActionComplete == false)
            {
                MessageBox.Show("This list is now in preview mode. You can click the save button to save.");
                isFirstActionComplete = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText(file_path, json_data);
                MessageBox.Show("Data Saved");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", $@"{Path.GetFullPath(file_path).ToString().Replace("todos.json", "")}");
        }

        private void deleteMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                listBox1.Items.Remove(listBox1.Items[listBox1.SelectedIndex]);
                listBox1.Refresh();
                UpdateTodoArray();
            }
            else
            {
                MessageBox.Show("No Items Selected. Please select one of them.");
            }
        }

        private void editMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                Form2 form2 = new Form2(this);
                form2.PreItemName = listBox1.Items[listBox1.SelectedIndex].ToString();
                form2.ShowDialog();
            }
            else
            {
                MessageBox.Show("No Items Selected. Please select one of them.");
            }
        }

        public void EditValue(string oldValue, string newValue)
        {
            listBox1.Items.Remove(oldValue);
            listBox1.Items.Add(newValue);
            listBox1.Refresh();
            UpdateTodoArray();
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                itemLabel.Text = "No Items Selected";
            else
                itemLabel.Text = "Actions For: " + listBox1.Items[listBox1.SelectedIndex].ToString();
        }
    }
}
