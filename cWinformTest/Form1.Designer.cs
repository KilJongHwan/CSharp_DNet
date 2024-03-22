using MetroFramework;
using System;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Animation;
using System.Threading.Tasks;
using MetroFramework.Controls;
using System.Xml;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Collections.Generic;

namespace cWinformTest
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            // Dispose 메서드를 오버라이드하여 SqlConnection을 닫습니다.
            if (disposing && (SVR_CN != null))
            {
                SVR_CN.Close();
                SVR_CN.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.RadioButton1 = new MetroFramework.Controls.MetroRadioButton();
            this.RadioButton2 = new MetroFramework.Controls.MetroRadioButton();
            this.RadioButton3 = new MetroFramework.Controls.MetroRadioButton();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTunum = new MetroFramework.Controls.MetroTextBox();
            this.cmdJoje = new MetroFramework.Controls.MetroButton();
            this.cboPatient = new MetroFramework.Controls.MetroComboBox();
            this.cmdEmg = new MetroFramework.Controls.MetroButton();
            this.cmdCancle = new MetroFramework.Controls.MetroButton();
            this.cmdExit = new MetroFramework.Controls.MetroButton();
            this.Label1 = new MetroFramework.Controls.MetroLabel();
            this.Label2 = new MetroFramework.Controls.MetroLabel();
            this.ProgressBar1 = new MetroFramework.Controls.MetroProgressBar();
            this.txtDate = new System.Windows.Forms.DateTimePicker();
            this.Panel2 = new MetroFramework.Controls.MetroPanel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.SS = new System.Windows.Forms.DataGridView();
            this.RP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupBox1.SuspendLayout();
            this.Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SS)).BeginInit();
            this.SuspendLayout();
            // 
            // RadioButton1
            // 
            this.RadioButton1.AutoSize = true;
            this.RadioButton1.Location = new System.Drawing.Point(29, 22);
            this.RadioButton1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RadioButton1.Name = "RadioButton1";
            this.RadioButton1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RadioButton1.Size = new System.Drawing.Size(50, 17);
            this.RadioButton1.TabIndex = 10;
            this.RadioButton1.TabStop = true;
            this.RadioButton1.Text = "외래";
            this.RadioButton1.UseVisualStyleBackColor = true;
            // 
            // RadioButton2
            // 
            this.RadioButton2.AutoSize = true;
            this.RadioButton2.Location = new System.Drawing.Point(29, 45);
            this.RadioButton2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RadioButton2.Name = "RadioButton2";
            this.RadioButton2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RadioButton2.Size = new System.Drawing.Size(50, 17);
            this.RadioButton2.TabIndex = 11;
            this.RadioButton2.TabStop = true;
            this.RadioButton2.Text = "입원";
            this.RadioButton2.UseVisualStyleBackColor = true;
            // 
            // RadioButton3
            // 
            this.RadioButton3.AutoSize = true;
            this.RadioButton3.Location = new System.Drawing.Point(29, 68);
            this.RadioButton3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RadioButton3.Name = "RadioButton3";
            this.RadioButton3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RadioButton3.Size = new System.Drawing.Size(89, 17);
            this.RadioButton3.TabIndex = 12;
            this.RadioButton3.TabStop = true;
            this.RadioButton3.Text = "응급실처방";
            this.RadioButton3.UseVisualStyleBackColor = true;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.RadioButton3);
            this.GroupBox1.Controls.Add(this.RadioButton2);
            this.GroupBox1.Controls.Add(this.RadioButton1);
            this.GroupBox1.Location = new System.Drawing.Point(518, 78);
            this.GroupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GroupBox1.Size = new System.Drawing.Size(144, 101);
            this.GroupBox1.TabIndex = 13;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "처방선택";
            // 
            // txtTunum
            // 
            this.txtTunum.Location = new System.Drawing.Point(97, 55);
            this.txtTunum.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTunum.Name = "txtTunum";
            this.txtTunum.Size = new System.Drawing.Size(193, 30);
            this.txtTunum.TabIndex = 2;
            this.txtTunum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTunum_KeyPress);
            // 
            // cmdJoje
            // 
            this.cmdJoje.Location = new System.Drawing.Point(296, 18);
            this.cmdJoje.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmdJoje.Name = "cmdJoje";
            this.cmdJoje.Size = new System.Drawing.Size(174, 68);
            this.cmdJoje.TabIndex = 3;
            this.cmdJoje.Text = "시작";
            this.cmdJoje.Click += new System.EventHandler(this.cmdJoje_Click);
            // 
            // cboPatient
            // 
            this.cboPatient.FormattingEnabled = true;
            this.cboPatient.ItemHeight = 24;
            this.cboPatient.Location = new System.Drawing.Point(23, 99);
            this.cboPatient.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboPatient.Name = "cboPatient";
            this.cboPatient.Size = new System.Drawing.Size(447, 30);
            this.cboPatient.TabIndex = 4;
            // 
            // cmdEmg
            // 
            this.cmdEmg.Location = new System.Drawing.Point(22, 149);
            this.cmdEmg.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmdEmg.Name = "cmdEmg";
            this.cmdEmg.Size = new System.Drawing.Size(181, 35);
            this.cmdEmg.TabIndex = 5;
            this.cmdEmg.Text = "긴급조제";
            // 
            // cmdCancle
            // 
            this.cmdCancle.Location = new System.Drawing.Point(209, 149);
            this.cmdCancle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmdCancle.Name = "cmdCancle";
            this.cmdCancle.Size = new System.Drawing.Size(81, 35);
            this.cmdCancle.TabIndex = 6;
            this.cmdCancle.Text = "취소";
            this.cmdCancle.Click += new System.EventHandler(this.cmdCancle_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Location = new System.Drawing.Point(296, 149);
            this.cmdExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(174, 35);
            this.cmdExit.TabIndex = 7;
            this.cmdExit.Text = "종료";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(22, 19);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(69, 20);
            this.Label1.TabIndex = 8;
            this.Label1.Text = "접수일자";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(22, 55);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(69, 20);
            this.Label2.TabIndex = 9;
            this.Label2.Text = "투약번호";
            // 
            // ProgressBar1
            // 
            this.ProgressBar1.Enabled = false;
            this.ProgressBar1.Location = new System.Drawing.Point(22, 199);
            this.ProgressBar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ProgressBar1.Name = "ProgressBar1";
            this.ProgressBar1.Size = new System.Drawing.Size(448, 40);
            this.ProgressBar1.TabIndex = 13;
            // 
            // txtDate
            // 
            this.txtDate.Location = new System.Drawing.Point(97, 18);
            this.txtDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(193, 25);
            this.txtDate.TabIndex = 15;
            // 
            // Panel2
            // 
            this.Panel2.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.Panel2.Controls.Add(this.txtDate);
            this.Panel2.Controls.Add(this.ProgressBar1);
            this.Panel2.Controls.Add(this.Label2);
            this.Panel2.Controls.Add(this.Label1);
            this.Panel2.Controls.Add(this.cmdExit);
            this.Panel2.Controls.Add(this.cmdCancle);
            this.Panel2.Controls.Add(this.cmdEmg);
            this.Panel2.Controls.Add(this.cboPatient);
            this.Panel2.Controls.Add(this.cmdJoje);
            this.Panel2.Controls.Add(this.txtTunum);
            this.Panel2.HorizontalScrollbarBarColor = true;
            this.Panel2.HorizontalScrollbarHighlightOnWheel = false;
            this.Panel2.HorizontalScrollbarSize = 8;
            this.Panel2.Location = new System.Drawing.Point(21, 78);
            this.Panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(491, 258);
            this.Panel2.TabIndex = 16;
            this.Panel2.VerticalScrollbarBarColor = true;
            this.Panel2.VerticalScrollbarHighlightOnWheel = false;
            this.Panel2.VerticalScrollbarSize = 9;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // metroPanel1
            // 
            this.metroPanel1.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(667, 78);
            this.metroPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(543, 258);
            this.metroPanel1.TabIndex = 17;
            this.metroPanel1.VerticalScrollbar = true;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // SS
            // 
            this.SS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SS.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RP,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8});
            this.SS.Location = new System.Drawing.Point(21, 378);
            this.SS.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SS.Name = "SS";
            this.SS.RowHeadersWidth = 51;
            this.SS.RowTemplate.Height = 27;
            this.SS.Size = new System.Drawing.Size(1190, 378);
            this.SS.TabIndex = 18;
            // 
            // RP
            // 
            this.RP.HeaderText = "ＲＰ";
            this.RP.MinimumWidth = 6;
            this.RP.Name = "RP";
            this.RP.Width = 125;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "약품코드";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.Width = 125;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "약품명";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.Width = 125;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "수량";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.Width = 125;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "횟수";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.Width = 125;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "일수";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.Width = 125;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "용법";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            this.Column6.Width = 125;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Pettern";
            this.Column7.MinimumWidth = 6;
            this.Column7.Name = "Column7";
            this.Column7.Width = 125;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Gcode";
            this.Column8.MinimumWidth = 6;
            this.Column8.Name = "Column8";
            this.Column8.Width = 125;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 772);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.SS);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(18, 75, 18, 15);
            this.RightToLeftLayout = true;
            this.Text = "산제분포기Interface";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SS)).EndInit();
            this.ResumeLayout(false);

        }
        private void CustomizeComponents()
        {
            // Form
            this.Style = MetroFramework.MetroColorStyle.Teal;
            this.Theme = MetroFramework.MetroThemeStyle.Light;

            // RadioButtons
            RadioButton1.Style = MetroColorStyle.Teal;
            RadioButton2.Style = MetroColorStyle.Teal;
            RadioButton3.Style = MetroColorStyle.Teal;

            // GroupBox
            GroupBox1.ForeColor = Color.Teal;

            // TextBox
            txtTunum.Style = MetroColorStyle.Teal;

            // Buttons
            cmdJoje.Style = MetroColorStyle.Teal;
            cmdJoje.ForeColor = Color.White; // 버튼 텍스트 색상 변경
            cmdEmg.Style = MetroColorStyle.Teal;
            cmdCancle.Style = MetroColorStyle.Teal;
            cmdExit.Style = MetroColorStyle.Teal;

            // ComboBox
            cboPatient.Style = MetroColorStyle.Teal;

            // Labels
            Label1.Style = MetroColorStyle.Teal;
            Label2.Style = MetroColorStyle.Teal;

            // ProgressBar
            ProgressBar1.Style = MetroColorStyle.Teal;

            // DateTimePicker
            txtDate.CustomFormat = "yyyy-MM-dd";
            txtDate.Format = DateTimePickerFormat.Custom;

            // Panel
            Panel2.HorizontalScrollbarBarColor = true;
            Panel2.HorizontalScrollbarHighlightOnWheel = false;
            Panel2.HorizontalScrollbarSize = 10;
            Panel2.VerticalScrollbarBarColor = true;
            Panel2.VerticalScrollbarHighlightOnWheel = false;
            Panel2.VerticalScrollbarSize = 10;
        }
        #endregion
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

        
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private ErrorProvider errorProvider1;
        private DataGridView SS;
        private MetroPanel Panel2;
        private DataGridViewTextBoxColumn RP;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
    }
}

