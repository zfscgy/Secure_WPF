using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Diagnostics;
using System.ComponentModel;

namespace Secure_WPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        string fileName_1="", fileName_2="";
        string seed_1, seed_2, seed_3;



        string info ="";
        public MainWindow()
        {
            InitializeComponent();
        }






        private void button_encrypt_Click(object sender, RoutedEventArgs e)
        {
            if(fileName_1 == "")
            {
                MessageBox.Show("请打开需要操作的文件！");
                return;
            }
            if(GetSeed()!=0)
            {
                MessageBox.Show("请输入正确格式的密钥！");
                return;
            }
            SaveFile();
            if (fileName_2 == "")
            {
                return;
            }
            info +="准备加密到: "+fileName_2 +".\n正在加密,请等待,"
                +"请观察目录里的生成文件大小变化，最终生成文件与加密文件大小应该相同....\n";
            textBox.Text = info;
            ExecuteCMDexe(1);
            info += "加密完成！感谢您的使用，已生成报告文本在生成文件目录下。";
            textBox.Text = info;

            /////
            fileName_1 = "";
            fileName_2 = "";
        }



        private void button_decrypt_Click(object sender, RoutedEventArgs e)
        {
            if (fileName_1 == "")
            {
                MessageBox.Show("请打开需要操作的文件！");
                return;
            }
            if (GetSeed() != 0)
            {
                MessageBox.Show("请输入正确格式的密钥！");
                return;
            }
            SaveFile();
            if(fileName_2 == "")
            {
                return;
            }
            info += "准备解密到: " + fileName_2 + ".\n正在解密,请等待,"
    + "请观察目录里的生成文件大小变化，最终生成文件与加密文件大小应该相同....\n";
            textBox.Text = info;
            ExecuteCMDexe(2);
            info += "解密完成！感谢您的使用，已生成报告文本在生成文件目录下.";
            textBox.Text = info;
            /////
            fileName_1 = "";
            fileName_2 = "";
        }

        private void ShowAboutWindow(object sender, RoutedEventArgs e)
        {
            Window1 aboutWindow = new Window1();
            aboutWindow.Show();
        }
        private void OpenFile(object sender, RoutedEventArgs e)
        {
            ///            
            var openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "All Files (*.*)|*.*"
            };
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                fileName_1 = openFileDialog.FileName;
            }
            if (fileName_1 == "")
            {
                return;
            }
            ///
            info = "打开文件: " + fileName_1 + ".\n";
            textBox.Text = info;
        }
        private int SaveFile()
        {
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog()
            {
                Filter = "All Files (*.*)|*.*"
            };
            var result_save = saveFileDialog.ShowDialog();
            if (result_save == true)
            {
                fileName_2 = saveFileDialog.FileName;
            }
            if (fileName_1 == fileName_2)
            {
                MessageBox.Show("无法在加密/解密同时覆盖原文件！请保存到新的文件！");
                fileName_2 = "";
                info += "文件选取失败，请重试！\n";
                textBox.Text = info;
                return 1;
            }
            return 0;
        }

        private int ExecuteCMDexe(int choice)
        {

            Process SecureExe = new Process();
            string input;
            SecureExe.StartInfo.UseShellExecute = false;
            SecureExe.StartInfo.FileName = "SecureVC10.exe";
            SecureExe.StartInfo.CreateNoWindow = false;
            SecureExe.StartInfo.RedirectStandardInput = true;
            input =(choice.ToString() + " "
                + fileName_1 + " " + fileName_2 + " " + seed_1.ToString() + " " + seed_2.ToString() + " " + seed_3.ToString());
            SecureExe.Start();

            SecureExe.StandardInput.WriteLine(input);
            SecureExe.WaitForExit();
            SecureExe.Close();
            return 0;
        }

        private int GetSeed()
        {
            seed_1 = textBox1.Text;
            seed_2 = textBox2.Text;
            seed_3 = textBox3.Text;
            int temp;
            if(seed_1 == null ||seed_2 == null|| seed_3 == null)
            {
                return 1;
            }
            if(!Int32.TryParse(seed_1,out temp)|| !Int32.TryParse(seed_2, out temp)|| !Int32.TryParse(seed_3, out temp))
            {
                return 2;
            }
            if(Int32.Parse(seed_1) < 0||Int32.Parse(seed_1)>9999|| Int32.Parse(seed_2) < 0
                ||Int32.Parse(seed_2)>9999|| Int32.Parse(seed_3) < 0||Int32.Parse(seed_3)>999999)
            {
                return 3;
            }
            return 0;
        }

    }
}
