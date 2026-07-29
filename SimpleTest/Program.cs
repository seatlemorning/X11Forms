using System;
using System.Windows.Forms;

namespace SimpleTest
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // Включаем визуальные стили
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Запускаем форму
            Application.Run(new MainForm());
        }
    }
}