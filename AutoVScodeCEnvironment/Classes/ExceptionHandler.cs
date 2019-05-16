﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace AutoVScodeCEnvironment.Classes
{
    class ExceptionHandler
    {
        public static void ShowError(string name, Exception e)
        {
            DialogResult result = MessageBox.Show("在处理" + name + "时发生异常：\n" +
                            e.Message + "\n" +
                            e.StackTrace + "\n" +
                            "您是否要向作者提交Issue?", "错误", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (result == DialogResult.Yes)
            {
                // Clipboard.SetText("过程：" + name + "\n信息：" +  e.Message + "\n" + e.StackTrace);
                Thread sta = new Thread(() => SetClipboard(
                    "过程：" + name + "\n信息：" + e.Message + "\n" + e.StackTrace));
                sta.SetApartmentState(ApartmentState.STA);
                sta.Start();
                sta.Join();

                MessageBox.Show("错误信息已复制到您的剪切板，感谢您的反馈！", "O(∩_∩)O谢谢！");
                System.Diagnostics.Process.Start("https://github.com/SDchao/AutoVScodeCEnvironment/issues/new");
            }
            Environment.Exit(0);
        }

        private static void SetClipboard(string text)
        {
            Clipboard.SetText(text.ToString());
        }
    }
}
