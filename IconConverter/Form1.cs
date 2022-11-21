using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace IconConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = $"Start Convert {DateTime.Now}.";


            var convertResult = this.Convert();
            if (convertResult != null && convertResult.HasValue)
            {
                if (convertResult.Value)
                    label2.Text = $"End Convert {DateTime.Now}.";
                else
                    label2.Text = $"Error while converting.";
            }
            else
                label2.Text = "Convert Canceled.";
        }

        private bool? Convert()
        {
            try
            {
                var filePath = string.Empty;

                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = "c:\\";
                    openFileDialog.Filter = "All Graphics Types|*.jpg;*.jpeg;*.png;";
                    openFileDialog.FilterIndex = 2;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var fileinfo = new FileInfo(openFileDialog.FileName);

                        filePath = fileinfo.Directory.FullName;

                        var result = Path.ChangeExtension(openFileDialog.FileName, ".ico");

                        using (FileStream FS = File.OpenWrite(result))
                        {

                            Bitmap bitmap = (Bitmap)Image.FromFile(openFileDialog.FileName);
                            Icon.FromHandle(bitmap.GetHicon()).Save(FS);
                        }

                        MessageBox.Show($"Your File is ready on :{filePath}");
                    }
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
