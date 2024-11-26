using System;
using System.Collections.Generic;
using System.Text;
using CuahangNongduoc.DataLayer;
using CuahangNongduoc.BusinessObject;
using System.Windows.Forms;
using System.Data;

namespace CuahangNongduoc.Controller
{
    
    public class PhieuThanhToanController
    {
        PhieuThanhToanFactory factory = new PhieuThanhToanFactory();


        public DataRow NewRow()
        {
            return factory.NewRow();
        }
        public void Add(DataRow row)
        {
            factory.Add(row);
        }
        public void Save()
        {
            factory.Save();
        }

        public PhieuThanhToan LayPhieuThanhToan(String id)
        {
            PhieuThanhToan ph = null;
            DataTable tbl = factory.LayPhieuThanhToan(id);
            if (tbl.Rows.Count > 0 )
            {
                ph = new PhieuThanhToan();
                ph.Id = Convert.ToString(tbl.Rows[0]["ID"]);
                KhachHangController ctrlKH = new KhachHangController();
                ph.KhachHang = ctrlKH.LayKhachHang(Convert.ToString(tbl.Rows[0]["ID_KHACH_HANG"]));
                ph.NgayThanhToan = Convert.ToDateTime(tbl.Rows[0]["NGAY_THANH_TOAN"]);
                ph.TongTien = Convert.ToInt64(tbl.Rows[0]["TONG_TIEN"]);
                ph.GhiChu = Convert.ToString(tbl.Rows[0]["GHI_CHU"]);
            }
            return ph;
        }
        
        public void HienthiPhieuThanhToan(BindingNavigator bn, DataGridView dg,ComboBox cmb, TextBox txt, DateTimePicker dt, NumericUpDown numTongTien, TextBox txtGhichu, TextBox txtDV, NumericUpDown phiDV, NumericUpDown phiVC)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = factory.DanhsachPhieuThanhToan();
            bn.BindingSource = bs;
            dg.DataSource = bs;
            


            txt.DataBindings.Clear();
            txt.DataBindings.Add("Text", bs, "ID");

            cmb.DataBindings.Clear();
            cmb.DataBindings.Add("SelectedValue", bs, "ID_KHACH_HANG");

            dt.DataBindings.Clear();
            dt.DataBindings.Add("Value", bs, "NGAY_THANH_TOAN");

            numTongTien.DataBindings.Clear();
            numTongTien.DataBindings.Add("Value", bs, "TONG_TIEN");

            txtGhichu.DataBindings.Clear();
            txtGhichu.DataBindings.Add("Text", bs, "GHI_CHU");

            txtDV.DataBindings.Clear();
            txtDV.DataBindings.Add("Text", bs, "DICH_VU");

            phiDV.DataBindings.Clear();
            // Khởi tạo binding với giá trị mặc định là 0 nếu giá trị là DBNull
            phiDV.DataBindings.Add(new Binding("Value", bs, "PHI_DICH_VU", true, DataSourceUpdateMode.OnPropertyChanged, 0));


            phiVC.DataBindings.Clear();
            // Khởi tạo binding với giá trị mặc định là 0 nếu giá trị là DBNull
            phiVC.DataBindings.Add(new Binding("Value", bs, "PHI_VAN_CHUYEN", true, DataSourceUpdateMode.OnPropertyChanged, 0));


        }
        public void TimPhieuThanhToan(BindingNavigator bn, DataGridView dg, ComboBox cmb, TextBox txt, DateTimePicker dt, NumericUpDown numTongTien, TextBox txtGhichu,
            String idKH, DateTime dtNgayThu)
        {
            
            BindingSource bs = new BindingSource();
            bs.DataSource = factory.TimPhieuThanhToan(idKH, dtNgayThu);
            bn.BindingSource = bs;
            dg.DataSource = bs;


            txt.DataBindings.Clear();
            txt.DataBindings.Add("Text", bs, "ID");

            cmb.DataBindings.Clear();
            cmb.DataBindings.Add("SelectedValue", bs, "ID_KHACH_HANG");

            dt.DataBindings.Clear();
            dt.DataBindings.Add("Value", bs, "NGAY_THANH_TOAN");

            numTongTien.DataBindings.Clear();
            numTongTien.DataBindings.Add("Value", bs, "TONG_TIEN");

            txtGhichu.DataBindings.Clear();
            txtGhichu.DataBindings.Add("Text", bs, "GHI_CHU");
        }

        
    }
}
