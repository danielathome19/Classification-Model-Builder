
namespace ClassificationModelBuilder
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.btnBuildModel = new System.Windows.Forms.Button();
            this.lblNumImages = new System.Windows.Forms.Label();
            this.btnSaveModel = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnTrainModel = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnLoadModel = new System.Windows.Forms.Button();
            this.numOfEpochs = new System.Windows.Forms.TextBox();
            this.numOfClasses = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClassifyImages = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblModelClassPredict = new System.Windows.Forms.Label();
            this.btnPredictImage = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnOpenPredictFile = new System.Windows.Forms.Button();
            this.lblModelPrediction = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(542, 52);
            this.button1.TabIndex = 0;
            this.button1.Text = "Open Directory";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnBuildModel
            // 
            this.btnBuildModel.Enabled = false;
            this.btnBuildModel.Location = new System.Drawing.Point(285, 128);
            this.btnBuildModel.Name = "btnBuildModel";
            this.btnBuildModel.Size = new System.Drawing.Size(269, 119);
            this.btnBuildModel.TabIndex = 1;
            this.btnBuildModel.Text = "Build Model";
            this.btnBuildModel.UseVisualStyleBackColor = true;
            this.btnBuildModel.Click += new System.EventHandler(this.btnBuildModel_Click);
            // 
            // lblNumImages
            // 
            this.lblNumImages.AutoSize = true;
            this.lblNumImages.Location = new System.Drawing.Point(560, 20);
            this.lblNumImages.Name = "lblNumImages";
            this.lblNumImages.Size = new System.Drawing.Size(374, 37);
            this.lblNumImages.TabIndex = 2;
            this.lblNumImages.Text = "Number of images to train on:";
            // 
            // btnSaveModel
            // 
            this.btnSaveModel.Enabled = false;
            this.btnSaveModel.Location = new System.Drawing.Point(12, 484);
            this.btnSaveModel.Name = "btnSaveModel";
            this.btnSaveModel.Size = new System.Drawing.Size(542, 196);
            this.btnSaveModel.TabIndex = 3;
            this.btnSaveModel.Text = "Save Model Weights";
            this.btnSaveModel.UseVisualStyleBackColor = true;
            this.btnSaveModel.Click += new System.EventHandler(this.btnSaveModel_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 70);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1633, 52);
            this.progressBar1.TabIndex = 4;
            // 
            // btnTrainModel
            // 
            this.btnTrainModel.Enabled = false;
            this.btnTrainModel.Location = new System.Drawing.Point(3, 157);
            this.btnTrainModel.Name = "btnTrainModel";
            this.btnTrainModel.Size = new System.Drawing.Size(534, 63);
            this.btnTrainModel.TabIndex = 5;
            this.btnTrainModel.Text = "Train Model";
            this.btnTrainModel.UseVisualStyleBackColor = true;
            this.btnTrainModel.Click += new System.EventHandler(this.btnTrainModel_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.White;
            this.richTextBox1.Location = new System.Drawing.Point(560, 128);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(1085, 350);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "";
            // 
            // btnLoadModel
            // 
            this.btnLoadModel.Enabled = false;
            this.btnLoadModel.Location = new System.Drawing.Point(12, 128);
            this.btnLoadModel.Name = "btnLoadModel";
            this.btnLoadModel.Size = new System.Drawing.Size(269, 119);
            this.btnLoadModel.TabIndex = 8;
            this.btnLoadModel.Text = "Load Model Weights";
            this.btnLoadModel.UseVisualStyleBackColor = true;
            this.btnLoadModel.Click += new System.EventHandler(this.btnLoadModel_Click);
            // 
            // numOfEpochs
            // 
            this.numOfEpochs.Location = new System.Drawing.Point(310, 6);
            this.numOfEpochs.Name = "numOfEpochs";
            this.numOfEpochs.PlaceholderText = "# of Epochs";
            this.numOfEpochs.Size = new System.Drawing.Size(227, 43);
            this.numOfEpochs.TabIndex = 9;
            this.numOfEpochs.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numOfEpochs_KeyPress);
            // 
            // numOfClasses
            // 
            this.numOfClasses.Location = new System.Drawing.Point(310, 55);
            this.numOfClasses.Name = "numOfClasses";
            this.numOfClasses.PlaceholderText = "# of Classes";
            this.numOfClasses.Size = new System.Drawing.Size(227, 43);
            this.numOfClasses.TabIndex = 10;
            this.numOfClasses.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnClassifyImages);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.numOfEpochs);
            this.panel1.Controls.Add(this.numOfClasses);
            this.panel1.Controls.Add(this.btnTrainModel);
            this.panel1.Location = new System.Drawing.Point(12, 253);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(542, 225);
            this.panel1.TabIndex = 11;
            // 
            // btnClassifyImages
            // 
            this.btnClassifyImages.Enabled = false;
            this.btnClassifyImages.Location = new System.Drawing.Point(3, 104);
            this.btnClassifyImages.Name = "btnClassifyImages";
            this.btnClassifyImages.Size = new System.Drawing.Size(534, 47);
            this.btnClassifyImages.TabIndex = 13;
            this.btnClassifyImages.Text = "Classify Images";
            this.btnClassifyImages.UseVisualStyleBackColor = true;
            this.btnClassifyImages.Click += new System.EventHandler(this.btnClassifyImages_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 37);
            this.label3.TabIndex = 12;
            this.label3.Text = "# of Classes:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(299, 37);
            this.label1.TabIndex = 11;
            this.label1.Text = "# of Epochs to Train On:";
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.Color.Black;
            this.richTextBox2.ForeColor = System.Drawing.Color.White;
            this.richTextBox2.Location = new System.Drawing.Point(560, 484);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.Size = new System.Drawing.Size(1085, 445);
            this.richTextBox2.TabIndex = 12;
            this.richTextBox2.Text = "";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblModelClassPredict);
            this.panel2.Controls.Add(this.btnPredictImage);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.btnOpenPredictFile);
            this.panel2.Location = new System.Drawing.Point(12, 686);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(542, 438);
            this.panel2.TabIndex = 13;
            // 
            // lblModelClassPredict
            // 
            this.lblModelClassPredict.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblModelClassPredict.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblModelClassPredict.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblModelClassPredict.Location = new System.Drawing.Point(5, 245);
            this.lblModelClassPredict.Name = "lblModelClassPredict";
            this.lblModelClassPredict.Size = new System.Drawing.Size(532, 76);
            this.lblModelClassPredict.TabIndex = 17;
            this.lblModelClassPredict.Text = "Most likely class: ";
            // 
            // btnPredictImage
            // 
            this.btnPredictImage.Enabled = false;
            this.btnPredictImage.Location = new System.Drawing.Point(3, 324);
            this.btnPredictImage.Name = "btnPredictImage";
            this.btnPredictImage.Size = new System.Drawing.Size(535, 109);
            this.btnPredictImage.TabIndex = 16;
            this.btnPredictImage.Text = "Perform Prediction";
            this.btnPredictImage.UseVisualStyleBackColor = true;
            this.btnPredictImage.Click += new System.EventHandler(this.btnPredictImage_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(5, 61);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(532, 181);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // btnOpenPredictFile
            // 
            this.btnOpenPredictFile.Enabled = false;
            this.btnOpenPredictFile.Location = new System.Drawing.Point(3, 3);
            this.btnOpenPredictFile.Name = "btnOpenPredictFile";
            this.btnOpenPredictFile.Size = new System.Drawing.Size(535, 52);
            this.btnOpenPredictFile.TabIndex = 14;
            this.btnOpenPredictFile.Text = "Open File to Predict";
            this.btnOpenPredictFile.UseVisualStyleBackColor = true;
            this.btnOpenPredictFile.Click += new System.EventHandler(this.btnOpenPredictFile_Click);
            // 
            // lblModelPrediction
            // 
            this.lblModelPrediction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblModelPrediction.Location = new System.Drawing.Point(560, 932);
            this.lblModelPrediction.Name = "lblModelPrediction";
            this.lblModelPrediction.Size = new System.Drawing.Size(1085, 192);
            this.lblModelPrediction.TabIndex = 14;
            this.lblModelPrediction.Text = "Model Prediction:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1657, 1136);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblModelPrediction);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnLoadModel);
            this.Controls.Add(this.btnBuildModel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnSaveModel);
            this.Controls.Add(this.lblNumImages);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Classification Model Builder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnBuildModel;
        private System.Windows.Forms.Label lblNumImages;
        private System.Windows.Forms.Button btnSaveModel;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnTrainModel;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnLoadModel;
        private System.Windows.Forms.TextBox numOfEpochs;
        private System.Windows.Forms.TextBox numOfClasses;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button btnClassifyImages;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblModelPrediction;
        private System.Windows.Forms.Button btnPredictImage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnOpenPredictFile;
        private System.Windows.Forms.Label lblModelClassPredict;
    }
}

