using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace backup
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string SelectPath()

        {

            string targetFilePath = string.Empty;

            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)

            {

                targetFilePath = fbd.SelectedPath;

            }

            return targetFilePath;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            //选择路径改为输入路径
            //string targetFilePath = SelectPath();
            string targetFilePath = textBox1.Text;
            string sourceFilePath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            String bakFilePath = sourceFilePath + "bak" + DateTime.Now.ToString("yyyyMMddhhmmss");

            if (Directory.Exists(bakFilePath) == false)
            {
                Directory.CreateDirectory(bakFilePath);
            }
            //string bakFilePath = System.IO.Path.Combine(sourceFilePath, "bak");
            CopyFilefolder(sourceFilePath, targetFilePath, bakFilePath);


        }

        public static void CopyFilefolder(string sourceFilePath, string targetFilePath, string bakFilePath)
        {
            //获取源文件夹中的所有非目录文件

            string[] filenames1 = Directory.GetFiles(targetFilePath);
            string[] filenames2 = Directory.GetFiles(sourceFilePath);
            string fileName;
            string destFile;
            foreach (string filename1 in filenames1)

            {

                //遍历文件夹2中所有文件

                foreach (string filename2 in filenames2)

                {

                    //获取带扩展的文件名

                    string name1 = Path.GetFileName(filename1);

                    string name2 = Path.GetFileName(filename2);
                    fileName = Path.GetFileName(filename2);
                    destFile = Path.Combine(bakFilePath, fileName);

                    //判断文件名是否相同

                    if (name1 == name2)

                    {

                        //如果是有老版本的文件则存入bak文件夹

                        File.Copy(filename1, destFile, true);

                    }

                }

            }              
            //如果目标文件夹不存在，则新建目标文件夹
            //if (!Directory.Exists(targetFilePath))
            //{
            //   Directory.CreateDirectory(targetFilePath);
            // }
            //将获取到的文件一个一个拷贝到目标文件夹中 
        
            //上面一段在MSDN上可以看到源码 
    
            //获取并存储源文件夹中的文件夹名
            //string[] filefolders = Directory.GetFiles(sourceFilePath);
            //创建Directoryinfo实例 
            //DirectoryInfo dirinfo = new DirectoryInfo(sourceFilePath);
            //获取得源文件夹下的所有子文件夹名
           // DirectoryInfo[] subFileFolder = dirinfo.GetDirectories();
           // for (int j = 0; j < subFileFolder.Length; j++)
           // {
                //获取所有子文件夹名 
             //   string subSourcePath = sourceFilePath + "\\" + subFileFolder[j].ToString();
              //  string subTargetPath = targetFilePath + "\\" + subFileFolder[j].ToString();
                //把得到的子文件夹当成新的源文件夹，递归调用CopyFilefolder
               // CopyFilefolder(subSourcePath, subTargetPath, bakFilePath);
            //}
    
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
