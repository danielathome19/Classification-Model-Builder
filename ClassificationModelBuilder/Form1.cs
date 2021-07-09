using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Numpy;
using Keras;
using Keras.Models;
using Keras.Layers;
using Keras.PreProcessing.Image;
using Tensorflow;
//using Sharpkit.Learn;
using SharpLearning.CrossValidation;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using SharpLearning.CrossValidation.TrainingTestSplitters;
using System.Threading;

namespace ClassificationModelBuilder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _trainingSet = new List<Bitmap>();
            //_trainingSetAvgRGB = new List<double[]>();
        }
        //private List<double[]> _trainingSetAvgRGB;
        private List<Bitmap> _trainingSet;
        private string[] _files;

        private void button1_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = "";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    _files = Directory.GetFiles(fbd.SelectedPath);
                    BackgroundWorker objBackgroundWorker = new BackgroundWorker();
                    objBackgroundWorker.WorkerReportsProgress = true;
                    objBackgroundWorker.DoWork += (o, ea) =>
                    {
                        int i = 1;
                        foreach (string file in _files)
                        {
                            Bitmap bmpImage = ResizeImage(file, new Size(img_width, img_height));
                            _trainingSet.add(bmpImage);
                            //double[] processResult = GetAverageRGB(bmpImage);
                            //_trainingSetAvgRGB.Add(processResult);
                            double percent = (100.0 * i) / _files.Length;
                            ((BackgroundWorker)o).ReportProgress((int)percent);
                            i++;
                        }
                    };
                    objBackgroundWorker.ProgressChanged += (o, ea) => { progressBar1.Value = ea.ProgressPercentage; };
                    objBackgroundWorker.RunWorkerCompleted += (o, ea) => { 
                        lblNumImages.Text = "Number of images to train on: " + _trainingSet.Count;
                        btnBuildModel.Enabled = true;
                        btnLoadModel.Enabled = true;
                        btnClassifyImages.Enabled = true;
                    };
                    objBackgroundWorker.RunWorkerAsync();
                }
                               
            }
        }

        Bitmap ResizeImage(string file, Size size)
        {
            return new Bitmap(Image.FromFile(file), size);
        }

        double[] GetAverageRGB(Bitmap bmpImage)
        {
            double[] result = new double[3];
            int numberOfPixlels = 0;

            for (int i = 0; i < bmpImage.Width; i++)
            {
                for (int j = 0; j < bmpImage.Height; j++)
                {
                    Color c = bmpImage.GetPixel(i, j);
                    result[0] += c.R;
                    result[1] += c.G;
                    result[2] += c.B;
                    numberOfPixlels++;
                }
            }

            bmpImage.Dispose();

            result[0] /= numberOfPixlels;
            result[1] /= numberOfPixlels;
            result[2] /= numberOfPixlels;
            return result;
        }

        Shape input_shape;
        Sequential model;
        private void btnBuildModel_Click(object sender, EventArgs e)
        {
            buildModel();
        }

        private void buildModel()
        {
            input_shape = new Shape(img_width, img_height, 3);
            model = new Sequential();
            model.Add(new Conv2D(32, kernel_size: (2, 2).ToTuple(), activation: "relu", input_shape: input_shape));
            model.Add(new MaxPooling2D(pool_size: (2, 2).ToTuple()));

            model.Add(new Conv2D(32, kernel_size: (2, 2).ToTuple(), activation: "relu"));
            model.Add(new MaxPooling2D(pool_size: (2, 2).ToTuple()));

            model.Add(new Conv2D(64, kernel_size: (2, 2).ToTuple(), activation: "relu"));
            model.Add(new MaxPooling2D(pool_size: (2, 2).ToTuple()));

            model.Add(new Conv2D(32, kernel_size: (2, 2).ToTuple(), activation: "relu"));
            model.Add(new MaxPooling2D(pool_size: (2, 2).ToTuple()));

            model.Add(new Flatten());
            model.Add(new Dense(64));
            model.Add(new Activation("relu"));
            model.Add(new Dropout(0.05));
            model.Add(new Dense(1));
            model.Add(new Activation("sigmoid"));

            model.Compile(optimizer: "rmsprop", loss: "binary_crossentropy", metrics: new string[] { "accuracy" });

            richTextBox1.Text = "Model Summary:\n========================================\n" + JValue.Parse(model.ToJson()).ToString(Formatting.Indented);
            btnTrainModel.Enabled = true;
            btnSaveModel.Enabled = true;
            btnClassifyImages.Enabled = true;
        }

        int img_width = 400;
        int img_height = 400;
        int batchSize;
        bool imagesClassified = false;
        private void btnTrainModel_Click(object sender, EventArgs e)
        {
            if (imagesClassified)
            {
                batchSize = (int)Math.Sqrt(_trainingSet.Count);
                int numEpochs = -1;
                try
                {
                    numEpochs = Convert.ToInt32(numOfEpochs.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Error: number of epochs must be specified!");
                }
                if (numEpochs != -1)
                {
                    //Begin training
                    richTextBox2.Text = "Beginning training...";
                    Thread.Sleep(500);
                    //Creates training test splitter, observations are shuffled randomly
                    var splitter = new RandomTrainingTestIndexSplitter<Entry<string,string>>(trainingPercentage: 0.7, seed: 24);

                    var trainingTestSplit = splitter.Split(indices);
                    //var trainingTestSplit = splitter.SplitSet(observations, targets);
                    var trainingSet = trainingTestSplit.TrainingIndices;
                    var testSet = trainingTestSplit.TestIndices;

                    if (!buildDirectories(trainingSet, testSet))
                    {
                        MessageBox.Show("Could not copy to temporary training directories.");
                        return;
                    }

                    int nb_train_samples = trainingSet.Length;
                    int nb_test_samples = testSet.Length;
                    
                    var train_datagen = new ImageDataGenerator(
                        rescale: 1.0f / 255, 
                        shear_range: 0.2f, 
                        zoom_range: 0.2f, 
                        horizontal_flip: true);

                    var test_datagen = new ImageDataGenerator(rescale: 1.0f / 255);

                    string train_data_dir = Path.Combine(Path.GetTempPath(), Path.Combine("ClassificationModelBuilder", "Training"));
                    string test_data_dir = Path.Combine(Path.GetTempPath(), Path.Combine("ClassificationModelBuilder", "Validation"));

                    var train_generator = train_datagen.FlowFromDirectory(
                        train_data_dir,
                        target_size: new Tuple<int, int>(img_width, img_height),
                        batch_size: batchSize,
                        class_mode: "binary"
                        );

                    var test_generator = test_datagen.FlowFromDirectory(
                        test_data_dir,
                        target_size: new Tuple<int, int>(img_width, img_height),
                        batch_size: batchSize,
                        class_mode: "binary"
                        );

                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    var history = model.FitGenerator(train_generator,
                                    steps_per_epoch: (int)Math.Floor(nb_train_samples / (double)batchSize), 
                                    epochs: numEpochs,
                                    validation_data: test_generator,
                                    validation_steps: (int)Math.Floor(nb_test_samples / (double)batchSize), verbose: 0);
                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds;

                    var y_train_pred = model.PredictGenerator(train_generator);       //prediction on the train data
                    double[] y_test_pred = model.EvaluateGenerator(test_generator);

                    string[] modelClasses = getClasses();

                    richTextBox2.Text += "\nTraining complete!\nExecution time: " + elapsedMs.ToString() + "ms\n========================================\n";
                    richTextBox2.Text += "Model classes:\n[";
                    for (int i = 0; i < modelClasses.Length; i++)
                    {
                        richTextBox2.Text += $"[{i}] => {modelClasses[i]},\n";
                    }
                    richTextBox2.Text += "]";
                    richTextBox2.Text += "\n========================================\n";
                    richTextBox2.Text += "Model prediction on training data:\n" + y_train_pred.ToString();
                    richTextBox2.Text += "\n========================================\n";
                    richTextBox2.Text += "Model evaluation on testing data" + ":\n[";
                    foreach (var y in y_test_pred)
                    {
                        richTextBox2.Text += y.ToString() + ", ";
                    }
                    richTextBox2.Text += "]";
                    richTextBox2.Text += "\n========================================\n";
                    richTextBox2.Text += "Parameters: \n{ ";
                    foreach (var x in history.Parameters)
                    {
                        richTextBox2.Text += x.Key + ": " + x.Value.ToString() + ", ";
                    }
                    richTextBox2.Text += "} \n========================================\nHistory: \n{\n";
                    foreach (var x in history.HistoryLogs)
                    {
                        richTextBox2.Text += x.Key + ":\n[";
                        foreach (var y in x.Value)
                        {
                            richTextBox2.Text += y.ToString() + ", ";
                        }
                        richTextBox2.Text += "]\n";
                    }
                    richTextBox2.Text += "}";

                    btnOpenPredictFile.Enabled = true;
                }
            } else
            {
                MessageBox.Show("All images must be classified first!");
            }
        }

        private bool buildDirectories(int[] trainingIndices, int[] testingIndices)
        {
            try
            {
                string[] classes = getClasses();

                string path = Path.Combine(Path.GetTempPath(), "ClassificationModelBuilder");

                if (Directory.Exists(path)) DeleteDirectory(path);
                Directory.CreateDirectory(path);
                Directory.CreateDirectory(Path.Combine(path, "Training"));
                Directory.CreateDirectory(Path.Combine(path, "Validation"));

                foreach (string imgclass in classes)
                {
                    path = Path.Combine(Path.GetTempPath(), Path.Combine("ClassificationModelBuilder", Path.Combine("Training", imgclass)));
                    if (!(Directory.Exists(path))) Directory.CreateDirectory(path);
                    path = Path.Combine(Path.GetTempPath(), Path.Combine("ClassificationModelBuilder", Path.Combine("Validation", imgclass)));
                    if (!(Directory.Exists(path))) Directory.CreateDirectory(path);
                }

                foreach (int i in trainingIndices)
                {
                    path = Path.Combine(Path.GetTempPath(),
                        Path.Combine("ClassificationModelBuilder",
                        Path.Combine("Training",
                        Path.Combine(indices[i].Value, $"{i}")))); //indices<filepath, class>

                    File.Copy(_files[i], path + Path.GetExtension(_files[i]));
                }

                foreach (int i in testingIndices)
                {
                    path = Path.Combine(Path.GetTempPath(),
                        Path.Combine("ClassificationModelBuilder",
                        Path.Combine("Validation",
                        Path.Combine(indices[i].Value, $"{i}")))); //indices<filepath, class>

                    File.Copy(_files[i], path + Path.GetExtension(_files[i]));
                }

                return true;
            } catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
                return false;
            }
        }

        private void DeleteDirectory(string path)
        {
            // Delete all files from the Directory  
            foreach (string filename in Directory.GetFiles(path))
            {
                File.Delete(filename);
            }
            // Check all child Directories and delete files  
            foreach (string subfolder in Directory.GetDirectories(path))
            {
                DeleteDirectory(subfolder);
            }
            Directory.Delete(path);
            //MessageBox.Show("Directory deleted successfully");
        }

        private string[] getClasses()
        {
            var classes = new List<string>();

            foreach (var entry in indices)
            {
                if (!classes.Contains(entry.Value))
                {
                    classes.Add(entry.Value);
                }
            }

            string[] clsList = classes.ToArray().OrderBy(d => d).ToArray();
            return clsList;
        }

        private void btnSaveModel_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            fbd.Description = "Please choose a directory to save the model weights.";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string json = model.ToJson();
                //File.WriteAllText(fbd.SelectedPath + "/classificationmodel.json", json);
                model.SaveWeight(fbd.SelectedPath + "/classificationmodel.h5");
                //MessageBox.Show("Model and weights saved successfully!");
                MessageBox.Show("Model weights saved successfully!");
            }
        }

        private void btnLoadModel_Click(object sender, EventArgs e)
        {
            OpenFileDialog fbd = new OpenFileDialog();
            fbd.Filter = "Model Weights|*.h5;";
            //fbd.InitialDirectory = @"C:\";
            fbd.CheckFileExists = true;
            fbd.CheckPathExists = true;
            fbd.Multiselect = false;
            fbd.Title = "Please select a model weight file to load.";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string file = fbd.FileName;
                //model = Sequential.ModelFromJson(File.ReadAllText(fbd.SelectedPath + "/classificationmodel.json"));
                buildModel();
                model.LoadWeight(file);
                richTextBox1.Text = "Model Summary:\n========================================\n" + JValue.Parse(model.ToJson()).ToString(Formatting.Indented);
                btnTrainModel.Enabled = true;
                btnSaveModel.Enabled = true;
                btnClassifyImages.Enabled = true;
                btnOpenPredictFile.Enabled = true;
                MessageBox.Show("Model loaded successfully!");
            }
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void numOfEpochs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public Entry<string, string>[] indices;    //<filepath, class>
        private void btnClassifyImages_Click(object sender, EventArgs e)
        {
            int numClasses = -1;
            try
            {
                numClasses = Convert.ToInt32(numOfClasses.Text);
                var classifyForm = new Form2(numClasses, _files, this);
                //classifyForm.ShowDialog();
                if (classifyForm.ShowDialog() == DialogResult.OK)
                {
                    indices = classifyForm.classifiedImages;
                    if (indices.Length == 0 || indices == null || indices.Length < _files.Length || indices.Contains(null)) throw new IndexOutOfRangeException("Images not classified");
                    btnTrainModel.Enabled = true;
                    //MessageBox.Show("Indices contains " + indices.Length + " items");
                    imagesClassified = true;
                } else
                {
                    throw new IndexOutOfRangeException("Images not classified");
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception)
            {
                MessageBox.Show("Error: number of classes must be specified!");
            }
            //Prompt dialogue with listview - make user select all images for class X, then name class X
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try {
                DeleteDirectory(Path.Combine(Path.GetTempPath(), "ClassificationModelBuilder"));
            } catch (Exception) { }
        }

        private void btnOpenPredictFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fbd = new OpenFileDialog();
            fbd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            //fbd.InitialDirectory = @"C:\";
            fbd.CheckFileExists = true;
            fbd.CheckPathExists = true;
            fbd.Multiselect = false;
            fbd.Title = "Please select an image file for classification.";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string file = fbd.FileName;
                predictionImgPath = file;
                //MessageBox.Show(file);
                predictionBmp = ResizeImage(file, new Size(img_width, img_height));
                pictureBox1.Image = predictionBmp;
                btnPredictImage.Enabled = true;
            } else
            {
                predictionImgPath = "";
                predictionBmp = null;
            }
        }

        Bitmap predictionBmp = null;
        string predictionImgPath = "";

        private void btnPredictImage_Click(object sender, EventArgs e)
        {
            try
            {
                lblModelPrediction.Text = "";
                var train_datagen = new ImageDataGenerator(
                        rescale: 1.0f / 255,
                        shear_range: 0.2f,
                        zoom_range: 0.2f,
                        horizontal_flip: true);

                string train_data_dir = Path.Combine(Path.GetTempPath(), Path.Combine("ClassificationModelBuilder", "Predicting"));
                if (Directory.Exists(train_data_dir)) DeleteDirectory(train_data_dir);
                Directory.CreateDirectory(train_data_dir);
                string fileDir = Path.Combine(train_data_dir, "PredictionFile");
                Directory.CreateDirectory(fileDir);
                File.Copy(predictionImgPath, Path.Combine(fileDir, "predict_temp" + Path.GetExtension(predictionImgPath)));
                //Need to add a button to save class list, another to load class list, then generate CLASSNUMBER number of folders
                var train_generator = train_datagen.FlowFromDirectory(
                    train_data_dir,
                    target_size: new Tuple<int, int>(img_width, img_height),
                    batch_size: 32,
                    class_mode: "binary"
                    );

                var y_train_pred = model.PredictGenerator(train_generator);       //prediction on the train data
                lblModelPrediction.Text = y_train_pred.ToString();
            } catch (Exception ex)
            {
                MessageBox.Show("Error occurred during prediction: " + ex.Message);
            }
        }
    }
}
