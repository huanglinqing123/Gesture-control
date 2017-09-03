using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;//串口命名空间引用


using Microsoft.Win32;
using Microsoft.Office.Interop.PowerPoint;


namespace SerialPortTest
{
    public partial class Form1 : Form
    {


       private _Application ppt = null;

        private Presentation pptp = null; 

        public int i;
        public Form1()
        {
            InitializeComponent();
            CheckRegistryOfOffice();

            ppt = new ApplicationClass();
        }

        //初始化窗体加载串口
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (string com in System.IO.Ports.SerialPort.GetPortNames())
                    comboBox1.Items.Add(com);
            }
            catch (Exception er)
            {
                MessageBox.Show("端口加载失败！" + er.Message, "提示");
            }
            comboBox2.SelectedIndex = 4;
            comboBox3.SelectedIndex = 2;
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 1;
            comboBox1.SelectedIndex = 0;

            
        }

        private void PPTOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Microsoft PowerPoint演示文稿(*.ppt;*pptx)|*.ppt;*.pptx";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                wbPPT.Navigate(ofd.FileName);

                pptp = ppt.Presentations.Open(ofd.FileName, Microsoft.Office.Core.MsoTriState.msoTrue, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoFalse);

            }
        }


        private void btnGetCount_Click(object sender, EventArgs e)
        {
            int count = pptp.Slides.Count;
            txtPPTCount.Text = count.ToString();
        }
        /// <summary>
        /// PPT上一页。
        /// </summary>
        private void btnPrev()
        {
            if (ppt != null)
                ppt.ActivePresentation.SlideShowWindow.View.Previous();
        }
        /// <summary>
        /// PPT下一页。
        /// </summary>
        private void btnNext()
        {
            if (ppt != null)
                ppt.ActivePresentation.SlideShowWindow.View.Next();
        }

       

        //接收数据
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            
            //使用委托进行跨线程读取数据
            Invoke
             (new EventHandler
               (delegate
               {
                   
                 
                   textBox2.Text = serialPort1.ReadExisting();
                   int k = Convert.ToInt32(textBox2.Text);
                   if (k == 0)
                   {
                       btnNext();
                   }
                   else if (k == 1)
                   {
                       btnPrev();
                   }
                  
               }
               )
              );
             
            serialPort1.WriteLine("a");
      
           
        }



     
    
     


    




        //打开串口
        private void btnOpen_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                { serialPort1.Close(); }
                else
                {
                    serialPort1.BaudRate = int.Parse(comboBox2.Text.Trim());
                    serialPort1.DataBits = int.Parse(comboBox3.Text.Trim());
                    serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), comboBox4.Text.Trim());
                    serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), comboBox5.Text.Trim());
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.Open();
                }
                groupBox1.Enabled = !serialPort1.IsOpen;
                if (btnOpen.Text == "打开串口")
                {
                    btnOpen.Text = "关闭串口";
                  
                }
                else if (btnOpen.Text == "关闭串口")
                {
                    btnOpen.Text = "打开串口";
                  
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("串口打开失败!" + er.Message, "提示");
            }
        }

        private void CheckRegistryOfOffice()
        {
            RegistryKey reg = null;

            reg = Registry.LocalMachine;

            RegistryKey subKey = reg.CreateSubKey(@"SOFTWARE\Classes\PowerPoint.Show.8");


            reg = Registry.LocalMachine;

            subKey = reg.CreateSubKey(@"SOFTWARE\Classes\PowerPoint.Show.8");

            subKey.SetValue("EditFlags", 65536, RegistryValueKind.DWord);

            subKey.SetValue("BrowserFlags", 2147483808, RegistryValueKind.QWord);

            reg = Registry.LocalMachine;

            subKey = reg.CreateSubKey(@"SOFTWARE\Classes\PowerPoint.Show.12");

            subKey.SetValue("EditFlags", 65536, RegistryValueKind.DWord);

            subKey.SetValue("BrowserFlags", 2147483808, RegistryValueKind.QWord);

            reg = Registry.LocalMachine;

            subKey = reg.CreateSubKey(@"SOFTWARE\Classes\PowerPoint.SlideShow.8");

            subKey.SetValue("BrowserFlags", 2147483808, RegistryValueKind.QWord);

            reg = Registry.LocalMachine;

            subKey = reg.CreateSubKey(@"SOFTWARE\Classes\PowerPoint.SlideShow.12");

            subKey.SetValue("BrowserFlags", 2147483808, RegistryValueKind.QWord);

            reg = Registry.CurrentUser;


            subKey = reg.CreateSubKey(@"Software\Microsoft\Windows\Shell\AttachmentExecute\{0002DF01-0000-0000-C000-000000000046}");

            subKey.SetValue("PowerPoint.Show.8", 0, RegistryValueKind.DWord);

            reg = Registry.CurrentUser;

            subKey = reg.CreateSubKey(@"Software\Microsoft\Windows\Shell\AttachmentExecute\{0002DF01-0000-0000-C000-000000000046}");

            subKey.SetValue("PowerPoint.Show.12", 0, RegistryValueKind.DWord);

            reg = Registry.CurrentUser;


        }

      
    }
}
