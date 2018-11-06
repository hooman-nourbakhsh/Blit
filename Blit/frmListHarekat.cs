﻿using Connection_Class;
using Stimulsoft.Report;
using System;
using System.Windows.Forms;

namespace Blit
{
    public partial class frmListHarekat : DevComponents.DotNetBar.Office2007Form
    {
        Connection_Query query = new Connection_Query();
        public frmListHarekat()
        {
            InitializeComponent();
        }
        void Display()
        {
            query.OpenConection();
            try
            {
                dgvHarekat.DataSource = query.ShowData(string.Format("select * from tblHarekat where Tarikh Between '{0}' AND '{1}'", mskTarikhAz.Text, mskTarikhTa.Text));
            }
            catch (Exception)
            {
                MessageBox.Show("در اتصال به پایگاه داده خطایی رخ داده است ، لطفا مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            query.CloseConnection();
        }
        private void frmListHarekat_Load(object sender, EventArgs e)
        {
            //sakht shey az PersianCalendar
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            //meghdar dehi date tavasot P , agar month yekraghmi bod yek 0 gharar bde
            mskTarikhAz.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");
            mskTarikhTa.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");

            Display();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            query.OpenConection();
            try
            {
                int x = Convert.ToInt32(dgvHarekat.SelectedCells[0].Value);
                query.ExecuteQueries("delte from tblHarekat where ID=" + x);
                MessageBox.Show("عملیات با موفقیت انجام شد", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Display();
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی رخ داده است، مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            query.CloseConnection();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            StiReport report = new StiReport();
            report.Load("Report/rptListHarekat.mrt");
            report.Compile();
            report["tarikh_az"] = mskTarikhAz.Text;
            report["tarikh_ta"] = mskTarikhTa.Text;
            report.ShowWithRibbonGUI();
        }

        private void mskTarikhAz_TextChanged(object sender, EventArgs e)
        {
            Display();
        }

        private void mskTarikhTa_TextChanged(object sender, EventArgs e)
        {
            Display();
        }
    }
}