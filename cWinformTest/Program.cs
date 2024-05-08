using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cWinformTest
{
    internal static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            const string mutexName = "cWinformTest";

            using (var mutex = new Mutex(true, mutexName))
            {
                if (!mutex.WaitOne(0, false))
                {
                    // 이미 실행 중이므로 종료
                    MessageBox.Show("프로그램이 이미 실행 중입니다.");
                    return;
                }

                // 콘솔 창 표시
                try
                {
                    AllocConsole();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("콘솔 창 생성 실패: " + ex.Message);
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());

                // 뮤텍스 해제
                mutex.ReleaseMutex();
            }
        }
        
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();
    }

}
