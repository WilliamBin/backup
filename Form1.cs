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
            String bakFilePath = sourceFilePath + "bak" + DateTime.Now.ToString("yyyyMMddHHmmss");

            if (Directory.Exists(bakFilePath) == false)
            {
                Directory.CreateDirectory(bakFilePath);
            }
            //string bakFilePath = System.IO.Path.Combine(sourceFilePath, "bak");
            CopyFilefolder(sourceFilePath, targetFilePath, bakFilePath);
            //干完活干掉自己，喜欢多次使用的注释掉这句
            Application.Exit();
        }

        public static void CopyFilefolder(string sourceFilePath, string targetFilePath, string bakFilePath)
        {
            // 统一去掉路径末尾的分隔符，避免相对路径计算错位
            sourceFilePath = sourceFilePath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            targetFilePath = targetFilePath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

            // 确保 bak 根目录存在
            if (!Directory.Exists(bakFilePath))
            {
                Directory.CreateDirectory(bakFilePath);
            }

            // 递归获取目标文件夹及其所有子文件夹中的文件（多层目录一并处理）
            string[] targetFiles = Directory.GetFiles(targetFilePath, "*", SearchOption.AllDirectories);

            foreach (string targetFile in targetFiles)
            {
                // 计算相对路径，用于在 bak 中保留原来的多级目录结构
                string relativePath = targetFile.Substring(targetFilePath.Length)
                                                .TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

                // 仍是原逻辑：源文件夹中存在同（相对）路径的文件，即视为“老版本”，才备份
                string sourceFile = Path.Combine(sourceFilePath, relativePath);
                if (!File.Exists(sourceFile))
                {
                    continue;
                }

                // 在 bak 中重建对应的子目录并复制（覆盖同名备份）
                string destFile = Path.Combine(bakFilePath, relativePath);
                string destDir = Path.GetDirectoryName(destFile);
                if (!string.IsNullOrEmpty(destDir) && !Directory.Exists(destDir))
                {
                    Directory.CreateDirectory(destDir);
                }

                File.Copy(targetFile, destFile, true);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}