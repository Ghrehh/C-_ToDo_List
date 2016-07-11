
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;


namespace FormWithButton
{

    public class DeleteButton : Button
    {
        public int index;

        public DeleteButton()
        {

        }
        public DeleteButton(int ind)
        {
            this.index = ind;
        }
    }

    public class Form1 : Form
    {
        public FlowLayoutPanel panel;
        public Button button1;

        public TextBox textbox1;
        public string textbox_content;

       
        public List<Label> label_arr = new List<Label>();
        public List<DeleteButton> delete_arr = new List<DeleteButton>();
        public DeleteButton dbutton;

        public int x;
        public int y;
        

        public Form1()
        {

            this.x = 300;
            this.y = 500;

            this.Size = new Size(this.x, this.y);
            


            this.create_panel();
            this.create_text_box();
            this.create_button();
            this.ResizeEnd += new EventHandler(screen_resize);
            this.Text = "ToDo List";

        }
        private void create_panel()
        {
           
            panel = new FlowLayoutPanel();
            panel.FlowDirection = FlowDirection.LeftToRight;
            panel.Size = new Size(x - 20, y - 110);

            panel.Location = new Point(0, 70);
            panel.AutoScroll = true;
            //panel.WrapContents = false;
            panel.HorizontalScroll.Enabled = false;
            panel.HorizontalScroll.Visible = false;
            this.Controls.Add(panel);
        }
        private void create_text_box()
        {
            textbox1 = new TextBox();
            textbox1.Size = new Size(this.x, 100);
            textbox1.Location = new Point(0, 0);
            this.Controls.Add(textbox1);
        }
        private void create_button()
        {
            button1 = new Button();
            button1.Size = new Size(100, 40);
            button1.Location = new Point(0, 20);
            button1.Text = "Add Note";
            this.Controls.Add(button1);
            button1.Click += new EventHandler(button1_Click);
        }
        private void create_label(string content)
        {
            Label label1 = new Label();

            
            label1.AutoSize = true;
            label1.MinimumSize = new Size(this.Width - 80, 0);
            label1.MaximumSize = new Size(this.Width - 80, 0);
            label1.Text = content;
            label1.BackColor = Color.FromArgb(100, 100, 100);
            label1.ForeColor = Color.White;
            label1.Padding = new Padding(10, 10, 10, 10);
            label1.Margin = new Padding(2,2,2,2);

            this.panel.Controls.Add(label1);
            int index = label_arr.Count;
            label_arr.Add(label1);

            Console.WriteLine(label_arr.Count);

            dbutton = new DeleteButton(index);
            dbutton.Size = new Size(20, 20);
            dbutton.Text = "X";
            dbutton.Click += new EventHandler(dbutton_Click);



            this.panel.Controls.Add(dbutton);

        }
        private void button1_Click(object sender, EventArgs e)
        {
            textbox_content = textbox1.Text;
            if (textbox_content != "") { 
                //this.label1.Text = textbox_content + "\n\n";
                this.create_label(textbox_content);
                this.panel.VerticalScroll.Value = this.panel.VerticalScroll.Maximum; //scrolls to bottom
                textbox1.Text = "";
                this.panel.PerformLayout(); //updates panel
            }

        }

        private void dbutton_Click(object sender, EventArgs e)
        {    
            DeleteButton button = sender as DeleteButton;

            panel.Controls.Remove(button);
            button.Dispose();

            panel.Controls.Remove(label_arr[button.index]);
            label_arr[button.index].Dispose();


        }

        private void screen_resize(object sender, System.EventArgs e)
        {
            

            Console.WriteLine(this.Width);
            textbox1.Width = this.Width;
            panel.Width = this.Width;
            panel.Height = this.Height - 100;

            foreach (Label lb in label_arr)
            {

                lb.MinimumSize = new Size(this.Width - 80, 0);
                lb.MaximumSize = new Size(this.Width - 80, 0);
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new Form1());
            Console.Read();
            
        }
    }
}
