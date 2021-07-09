using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassificationModelBuilder
{
    public partial class Form2 : Form
    {
        public int classNumber;
        public int numClassified = 0;
        public string[] originalFiles;
        public string[] files;
        public bool finishedClassification = false;
        public Entry<string, string>[] classifiedImages;
        public Form1 form1Ptr;
        
        public Form2(int classNumberIn, string[] filePaths, Form1 form1ptr)
        {
            classNumber = classNumberIn;
            files = (string[])filePaths.Clone();
            originalFiles = (string[])filePaths.Clone();
            classifiedImages = new Entry<string, string>[filePaths.Length];
            form1Ptr = form1ptr;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e) {}

        private void updateListView(int classNum)
        {
            label2.Text = "Select all images for class #" + classNum + ":";
            listView1.Clear();
            imageList1.Images.Clear();

            foreach (var file in files)
            {
                try
                {
                    if (!string.IsNullOrEmpty(file))
                    {
                        this.imageList1.Images.Add(Image.FromFile(file));
                    } else
                    {
                        var bitmap = new Bitmap(4, 4);

                        for (var x = 0; x < bitmap.Width; x++)
                        {
                            for (var y = 0; y < bitmap.Height; y++)
                            {
                                bitmap.SetPixel(x, y, Color.White);
                            }
                        }

                        this.imageList1.Images.Add(bitmap);
                    }
                    //MessageBox.Show(file);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid image file error: " + ex.Message);
                }
            }
            this.listView1.View = View.LargeIcon;
            this.imageList1.ImageSize = new Size(128, 128);
            this.listView1.LargeImageList = this.imageList1;
            //or
            //this.listView1.View = View.SmallIcon;
            //this.listView1.SmallImageList = this.imageList1;

            for (int j = 0; j < this.imageList1.Images.Count; j++)
            {
                ListViewItem item = new ListViewItem();
                item.ImageIndex = j;
                this.listView1.Items.Add(item);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode < Keys.A) || (e.KeyCode > Keys.Z))
                e.Handled = true;
        }

        private int indexOfImage(Image image)
        {
            for (int i = 0; i < files.Length; i++)
            {
                var temp = Image.FromFile(files[i]);
                if (image.Equals(temp)) return 1;
            }
            return -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrWhiteSpace(textBox1.Text))
                MessageBox.Show("You must specify a valid class name!");
            else
            {
                foreach (ListViewItem itm in listView1.SelectedItems)
                {
                    int imgIndex = itm.ImageIndex;
                    if (imgIndex >= 0 && imgIndex < this.imageList1.Images.Count)
                    {
                        //pictureBox1.Image = this.imageList1.images[imgIndex];
                        if (classifiedImages[imgIndex] == null)
                        {
                            classifiedImages[imgIndex] = new Entry<string, string>(files[imgIndex], textBox1.Text); //<filepath, class>
                            files[imgIndex] = "";
                        }
                    }

                }

                numClassified++;
                if (numClassified == classNumber)
                {
                    bool hasClassifiedAll = true;

                    //loop through and check to be sure that each image has been classified, otherwise restart.
                    for (int i = 0; i < files.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(files[i])) hasClassifiedAll = false;
                    }

                    if (!hasClassifiedAll)
                    {
                        MessageBox.Show("Not all images were classified. Restarting...");
                        files = (string[])originalFiles.Clone();
                        classifiedImages = null;
                        classifiedImages = new Entry<string, string>[files.Length];
                        numClassified = 0;
                        updateListView(numClassified + 1);
                        textBox1.Clear();
                    } else
                    {
                        finishedClassification = true;
                        MessageBox.Show("Completed classification!");
                        button1.Enabled = false;
                        textBox1.Enabled = false;
                        form1Ptr.indices = classifiedImages;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                } else
                {
                    updateListView(numClassified + 1);
                    textBox1.Clear();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = false;
            updateListView(numClassified + 1);
            updateListView(numClassified + 1);
        }
    }
}
