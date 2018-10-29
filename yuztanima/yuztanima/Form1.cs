using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;


namespace yuztanima
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
            Capture capture = new Capture();
            capture.Start();
            capture.imagegrabbed+=(a,b)=>
            {
                var image = capture.RetrieveBgrFrame();
                var image2 = image.Convert<Gray, byte>();
                HaarCascade haaryuz = new HaarCascade("alt.xml");
                MCvAvgComp[][] yuzler = image2.DetectHaarCascade(haaryuz, 1.2, 5, HAAR_DETECTİON_TYPE.DO_CANNY_PRUNING, new Size(15, 15));
            }

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>

                  {
                      for (int i = 0; i < 10; i++)
                      {
                          if (!recognition.SaveTrainingData(pictureBox2.Image, textBox1.Text)) MessageBox.Show("hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                          Thread.Sleep(100);
                          label1.Text = (i + 1) + "adet profil";
                      }
                      recognition = null;
                      train = null;
                      recognition = new BusinessRecognition("D:\\", "Faces", "yuz.xml");
                      train = new Classifier_Train("D:\\", "Faces", "yuz.xml");

                  });
            }

        BusinessRecognition recognition= new BusinessRecognition("D:\\", "Faces", "yuz.xml");
        Classifier_Train train = new Classifier_Train("D:\\", "Faces", "yuz.xml");

    }
}
