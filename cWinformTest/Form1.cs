using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using MetroFramework.Controls;
using MetroFramework.Forms;
using SoapHttpClient;
using SoapHttpClient.Enums;

namespace cWinformTest
{
    public partial class Form1 : MetroForm
    {
        // 라디오 버튼
        private MetroRadioButton RadioButton1;
        private MetroRadioButton RadioButton2;
        private MetroRadioButton RadioButton3;

        // 그룹박스
        private GroupBox GroupBox1;

        // 텍스트박스
        private MetroTextBox txtTunum;

        // 버튼
        private MetroButton cmdJoje;
        private MetroButton cmdEmg;
        private MetroButton cmdCancle;
        private MetroButton cmdExit;
        
        // 콤보박스
        private MetroComboBox cboPatient;

        // 라벨
        private MetroLabel Label1;
        private MetroLabel Label2;

        // 프로그레스바
        private MetroProgressBar ProgressBar1;

        // 데이트타임피커
        private DateTimePicker txtDate;
        // 타이머
        private Timer timer1;


        public System.Data.SqlClient.SqlConnection SVR_CN; // SQL Server 연결 객체

        private readonly BackgroundWorker bw = new BackgroundWorker();
        private DataTable dt;

        private bool isRunning;
        private bool bDataFind;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            CustomizeComponents();
            InitializeDatabaseConnection();
            timer1 = new Timer
            {
                Interval = 10 // 타이머 간격 설정
            };
            timer1.Tick += new EventHandler(timer1_Tick); // 타이머 이벤트 핸들러 등록

            // Form의 Resize 이벤트 핸들러 등록
            this.Resize += Form1_Resize;

            bw.DoWork += Bw_DoWork;
            bw.RunWorkerCompleted += Bw_RunWorkerCompleted;

            dt = new DataTable();

            isRunning = false;
            bDataFind = false;
        }
        private async void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            var id = e.Argument as string;
            //dt = await LoadDataPas(id);
        }


        //public async Task<DataTable> LoadDataPas(string patientId)
        //{

        //}

        private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("데이터 로딩 오류: " + e.Error.Message);
                return;
            }

            SetData(dt);

            this.Enabled = true;
        }


        private void txtTunum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!isRunning)
                {
                    if (IsDatabaseConnected())
                    {
                        bw.RunWorkerAsync(txtTunum.Text); // 데이터 로딩 시작
                    }
                    else
                    {
                        MessageBox.Show("데이터베이스에 접속을 실패하였습니다.");
                        return;
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int step = 1;
            ProgressBar1.Value = Math.Min(ProgressBar1.Value + step, ProgressBar1.Maximum);
            if (ProgressBar1.Value == ProgressBar1.Maximum)
            {
                ProgressBar1.Value = ProgressBar1.Minimum;
            }
        }

        private async void cmdJoje_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                // 중지 버튼 클릭 시
                timer1.Stop();
                cmdJoje.Text = "시작";
            }
            else
            {
                // 시작 버튼 클릭 시
                if (IsDatabaseConnected())
                {
                    dt = await LoadDataAsync(txtDate.Value.ToString("yyyy-MM-dd"));
                    await StartProgressBarFillAsync();
                    SetData(dt);
                    cmdJoje.Text = "중지";
                }
                else
                {
                    MessageBox.Show("데이터베이스에 접속을 실패하였습니다.");
                    return;
                }
            }
            // 토글버튼
            isRunning = !isRunning;
        }
        // 데이터베이스 연결 초기화 메서드
        private void InitializeDatabaseConnection()
        {
            string dataSource = "(local)";
            string initialCatalog = "PXDNET";
            string userID = "sa";
            string password = "yuyama";
            bool persistSecurityInfo = true;
            //string provider = "SQLOLEDB.1";
            int connectTimeout = 180;

            string connectionString = $"Data Source={dataSource};Initial Catalog={initialCatalog};User ID={userID};Password={password};Persist Security Info={persistSecurityInfo};Connect Timeout={connectTimeout}";

            SVR_CN = new SqlConnection(connectionString); // SVR_CN 초기화

            try
            {
                SVR_CN.Open();
                Console.WriteLine("데이터베이스 연결 성공!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("데이터베이스 연결 실패: " + ex.Message);
            }
        }


        // 데이터베이스 서버와의 연결을 확인하는 메서드
        public bool CheckServerConnection()
        {
            try
            {
                if (SVR_CN != null && SVR_CN.State == ConnectionState.Open)
                {
                    Console.WriteLine("데이터베이스 서버 연결 상태: 오픈");
                    return true;
                }
                else
                {
                    Console.WriteLine("데이터베이스 서버 연결 상태: 닫힘");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("데이터베이스 서버 연결 상태 확인 실패: " + ex.Message);
                return false;
            }
        }

        // 다른 메서드나 이벤트 핸들러에서 데이터베이스 연결 상태를 확인할 수 있도록 public 메서드 제공
        public bool IsDatabaseConnected()
        {
            return CheckServerConnection();
        }

        private async Task StartProgressBarFillAsync()
        {
            timer1.Start();
            while (isRunning && ProgressBar1.Value < ProgressBar1.Maximum)
            {
                await Task.Delay(100);
                ProgressBar1.Invoke(new Action(() =>
                {
                    // 한 번에 여러 단위로 증가시켜 부드럽게 보이게 함
                    ProgressBar1.Value = Math.Min(ProgressBar1.Value + 10, ProgressBar1.Maximum);
                }));
            }

            if (!isRunning)
            {
                // 중간에 중지 버튼을 눌렀을 경우
                cmdJoje.Invoke(new Action(() => cmdJoje.Text = "시작"));
            }
        }

        // 컨트롤들의 크기와 위치를 조정하는 메서드
        private void Form1_Resize(object sender, EventArgs e)
        {
            AdjustControlSizesAndPositions();
        }

        private void AdjustControlSizesAndPositions()
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetGridView(SS);

            // 긴급 버튼 비활성화
            cmdEmg.Enabled = false;
        }


        private void cmdExit_Click(object sender, EventArgs e)
        {
            SVR_CN.Close();
            Application.ExitThread();
        }

        private void cmdCancle_Click(object sender, EventArgs e)
        {
            txtTunum.Text = "";
        }

        private void txtTunum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public string GetData(Control AnyControl, long Col, long Row)
        {
            if (AnyControl == null)
                throw new ArgumentNullException(nameof(AnyControl));

            switch (AnyControl)
            {
                case TextBox textBox:
                    return textBox.Text;
                case Label label:
                    return label.Text;
                case DataGridView dataGridView:
                    if (dataGridView.Rows.Count > Row && dataGridView.Columns.Count > Col)
                    {
                        return dataGridView.Rows[(int)Row].Cells[(int)Col].Value?.ToString();
                    }
                    break;
            }

            return null;
        }
        // TakeTime 데이터 설정
        public string TakeTimeSet(string strArgCode)
        {
            string takeTimeNm = "";
            string sql = "SELECT vc_TakeTimeNm FROM M_TakeTime WHERE vc_TakeTimeCd = @Code";

            using (var connection = new SqlConnection(SVR_CN.ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Code", strArgCode);

                    try
                    {
                        object result = command.ExecuteScalar();
                        takeTimeNm = result?.ToString() ?? string.Empty;
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception($"TakeTimeSet 오류: {ex.Message}", ex);
                    }
                }
            }

            return takeTimeNm;
        }

        // TakeTime 코드 가져오기
        public string GetTakeTimeCd(string strArg)
        {
            if (strArg.Length > 4 && int.TryParse(strArg.Substring(strArg.Length - 4), out int result))
            {
                return strArg.Substring(strArg.Length - 4); // 뒤에서 4자리 추출
            }

            return "";
        }

        // TakeTime 이름 가져오기
        public string GetTakeTimeNm(string strArg)
        {
            string takeTimeNm = "";
            string sql = "SELECT vc_TakeTimeCd FROM M_TakeTime WHERE vc_TakeTimeNm = @Name";

            using (SqlCommand command = new SqlCommand(sql, SVR_CN))
            {
                command.Parameters.AddWithValue("@Name", strArg);

                try
                {
                    SqlDataReader reader = command.ExecuteReader(); // 데이터 리더 생성

                    if (reader.Read())
                    {
                        takeTimeNm = strArg + " " + reader["vc_TakeTimeCd"].ToString(); // 결과가 있는 경우 값 할당
                        Console.WriteLine(takeTimeNm );
                    }
                    else
                    {
                        Console.WriteLine("해당 내용이 없습니다. 입력후 사용하세요."); // 내용 없음 처리
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error occurred: " + ex.Message); // 오류 처리
                }
            }

            return takeTimeNm;
        }

        // 문자열 패딩 설정
        public string SpaceSet(string strData, int intLen)
        {
            strData = strData.Trim(); // 공백 제거
            return strData.Length <= intLen ? strData.PadRight(intLen) : strData.Substring(0, intLen); // 패딩 설정
        }

        // 빈 문자열 처리
        public string NS(string strData)
        {
            return string.IsNullOrEmpty(strData) ? "" : strData.Trim(); // 빈 문자열 처리
        }

        // Su 값에 따른 패턴 설정
        public string DsuAtcsu(double dblSu)
        {
            char[] pattChars = { '0', 'a', 'b', 'c', '1', 'd', 'e', 'f', '2', 'g', 'h', 'i', '3', 'j', 'k', 'l', '4', 'm', '5', 'n', '6', '7', '8', '9', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'Y', 'Z' };

            int index = (int)(dblSu * 4);
            index = Math.Min(index, pattChars.Length - 1);

            return pattChars[index].ToString(); // 패턴 설정
        }

        // 폼을 화면 중앙에 이동
        public void FormMoveCenter(Form anyForm, bool midChild)
        {
            int offsetX = midChild ? -30 : 0; // X 오프셋 설정
            int offsetY = midChild ? -800 : 345; // Y 오프셋 설정

            anyForm.Left = (Screen.PrimaryScreen.Bounds.Width - anyForm.Width) / 2 + offsetX; // X 좌표 설정
            anyForm.Top = (Screen.PrimaryScreen.Bounds.Height - anyForm.Height) / 2 + offsetY; // Y 좌표 설정

            anyForm.Text += " [ " + anyForm.Name + " ]"; // 제목 변경
        }
        // DataGridView에 데이터 설정하는 함수
        private void SetGridView(DataGridView dataGridView)
        {
            // 예시 데이터를 생성
            List<string[]> data = new List<string[]>
            {
                new string[] { "1", "약품코드1", "약품명1", "수량1", "횟수1", "일수1", "용법1", "패턴1", "Gcode1" },
                new string[] { "2", "약품코드2", "약품명2", "수량2", "횟수2", "일수2", "용법2", "패턴2", "Gcode2" },
            };

            // DataGridView에 행을 추가합니다.
            foreach (string[] row in data)
            {
                dataGridView.Rows.Add(row);
            }

            // 예시 데이터가 행에 잘 추가되었는지 확인합니다.
            Console.WriteLine("데이터가 DataGridView에 설정되었습니다.");
        }
        private async Task<DataTable> LoadDataAsync(string date)
        {
            // 데이터 테이블 객체 만들어서 새롭게 할당
            DataTable dt = new DataTable();
            // 데이터 테이블에 컬럼 추가
            dt.Columns.Add("RP", typeof(string));
            dt.Columns.Add("약품코드", typeof(string));
            dt.Columns.Add("약품명", typeof(string));
            dt.Columns.Add("수량", typeof(string));
            dt.Columns.Add("횟수", typeof(string));
            dt.Columns.Add("일수", typeof(string));
            dt.Columns.Add("용법코드", typeof(string));
            dt.Columns.Add("패턴", typeof(string));
            dt.Columns.Add("Gcode", typeof(string));

            string sql = @"
                USE [PXDNET]

                SELECT TOP 1000
                    M_Drug.vc_DrugCd,
                    M_Drug.vc_DrugNm,
                    M_Method.vc_MethodCd,
                    M_Method.ln_Fraction,
                    M_Method.vc_FreeItem1,
                    M_TakeTime.vc_TakeTimeNm,
                    M_TakeTime.vc_TakeTimeCd
         
                FROM M_Drug
                INNER JOIN dbo.M_Method ON M_Drug.vc_DrugCd = M_Drug.vc_DrugCd
                INNER JOIN M_TakeTime ON M_TakeTime.vc_TakeTimeCd = M_TakeTime.vc_TakeTimeCd";

            using (SqlCommand command = new SqlCommand(sql, SVR_CN))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // 데이터 처리
                    while (reader.Read())
                    {
                         string[] row = new string[]
                        {
                            "0",                // RP
                            reader["vc_DrugCd"].ToString(),   // 약품코드
                            reader["vc_DrugNm"].ToString(),   // 약품명
                            "0.0",               // 수량
                            reader["ln_Fraction"].ToString(),  // 횟수
                            "0",                // 일수
                            reader["vc_MethodCd"].ToString(),  // 용법코드
                            reader["vc_FreeItem1"].ToString(), // 패턴
                            reader["vc_TakeTimeCd"].ToString() // Gcode
                        };

                        dt.Rows.Add(row);
                    }
                }
            }

            return dt;
        }
        private void SetData(DataTable dt)
        {
            //  컬럼 설정
            if (SS.Columns.Count == 0)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    SS.Columns.Add(column.ColumnName, column.DataType.ToString());
                }
            }

            //  데이터 설정
            SS.Rows.Clear();
            foreach (DataRow row in dt.Rows)
            {
                SS.Rows.Add(row.ItemArray);
            }
        }
    }
}

