using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace CuahangNongduoc.DataLayer
{
    public class PhieuThanhToanFactory
    {
        DataService m_Ds = new DataService();

        public DataTable DanhsachPhieuThanhToan()
        {
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM PHIEU_THANH_TOAN ");
            m_Ds.Load(cmd);
            if(!m_Ds.Columns.Contains("Tong"))  m_Ds.Columns.Add("Tong", typeof(System.Int32));
            return TinhTongTien(m_Ds);
        }
        public DataTable TimPhieuThanhToan(String kh, DateTime ngay)
        {
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM PHIEU_THANH_TOAN WHERE ID_KHACH_HANG=@kh AND NGAY_THANH_TOAN = @ngay");
            cmd.Parameters.Add("kh", OleDbType.VarChar).Value = kh;
            cmd.Parameters.Add("ngay", OleDbType.Date).Value = ngay;

            m_Ds.Load(cmd);

            return m_Ds;
        }
      
        public DataTable LayPhieuThanhToan(String id)
        {
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM PHIEU_THANH_TOAN WHERE ID = @id");
            cmd.Parameters.Add("id", OleDbType.VarChar,50).Value = id;
            m_Ds.Load(cmd);
            return m_Ds;
        }


        public static long LayTongTien(String kh, int thang, int nam)
        {
            DataService ds = new DataService();
            OleDbCommand cmd = new OleDbCommand("SELECT SUM(TONG_TIEN) FROM PHIEU_THANH_TOAN WHERE ID_KHACH_HANG = @kh AND MONTH(NGAY_THANH_TOAN)=@thang AND YEAR(NGAY_THANH_TOAN)= @nam");
            cmd.Parameters.Add("kh", OleDbType.VarChar, 50).Value = kh;
            cmd.Parameters.Add("thang", OleDbType.Integer).Value = thang;
            cmd.Parameters.Add("nam", OleDbType.Integer).Value = nam;

            object obj = ds.ExecuteScalar(cmd);
            
            if (obj == null)
                return 0;
            else
                return Convert.ToInt64(obj);
        }

        public DataTable TinhTongTien(DataTable dt)
        {
            foreach(DataRow row in dt.Rows)
            {
                long sotien = Convert.ToInt64(row["TONG_TIEN"] == DBNull.Value ? 0 : row["TONG_TIEN"]);
                long phi_DV = Convert.ToInt64(row["PHI_DICH_VU"] == DBNull.Value ? 0 : row["PHI_DICH_VU"]);
                long phi_VC = Convert.ToInt64(row["PHI_VAN_CHUYEN"] == DBNull.Value ? 0 : row["PHI_VAN_CHUYEN"]);
                long tongtien = sotien + phi_DV + phi_VC;
                row["Tong"] = tongtien;
            }    
            return dt;
        }    

        public DataRow NewRow()
        {
            return m_Ds.NewRow();
        }
        public void Add(DataRow row)
        {
            m_Ds.Rows.Add(row);
        }
       public bool Save()
        {
           
            return m_Ds.ExecuteNoneQuery() > 0;
        }
    }
}
