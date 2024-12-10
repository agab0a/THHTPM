using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace BaiTap3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        XmlDocument doc = new XmlDocument();
        string path = @"D:\BaiTap3\DSNhanVien.xml";
        int d;
        private void HienThi()
        {
            dataNhanVien.Rows.Clear();
            doc.Load(path);

            XmlNodeList ds = doc.SelectNodes("/ds/nhanvien");
            int sd = 0;
            dataNhanVien.ColumnCount = 5;
            dataNhanVien.Rows.Add();
            foreach (XmlNode nv in ds)
            {
                XmlNode manv = nv.SelectSingleNode("@manv");
                dataNhanVien.Rows[sd].Cells[0].Value = manv.InnerText.ToString();
                XmlNode hoten = nv.SelectSingleNode("hoten");
                XmlNode ho = hoten.SelectSingleNode("ho");
                dataNhanVien.Rows[sd].Cells[1].Value = ho.InnerText.ToString();
                XmlNode ten = hoten.SelectSingleNode("ten");
                dataNhanVien.Rows[sd].Cells[2].Value = ten.InnerText.ToString();
                XmlNode diachi = nv.SelectSingleNode("diachi");
                dataNhanVien.Rows[sd].Cells[3].Value = diachi.InnerText.ToString();
                dataNhanVien.Rows.Add();
                sd++;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HienThi();
        }
        private void dataNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            d = e.RowIndex;
            txt_manv.Text = dataNhanVien.Rows[d].Cells[0].Value.ToString();
            txt_ho.Text = dataNhanVien.Rows[d].Cells[1].Value.ToString();
            txt_ten.Text = dataNhanVien.Rows[d].Cells[2].Value.ToString();
            txt_diachi.Text = dataNhanVien.Rows[d].Cells[3].Value.ToString();
        }

        private void them_Click(object sender, EventArgs e)
        {
            doc.Load(path);
            XmlElement goc = doc.DocumentElement;
            XmlNode nv = doc.CreateElement("nhanvien");
            XmlAttribute manv = doc.CreateAttribute("manv");
            manv.InnerText = txt_manv.Text;
            nv.Attributes.Append(manv);
            XmlNode hoten = doc.CreateElement("hoten");
            XmlNode ho = doc.CreateElement("ho");
            ho.InnerText = txt_ho.Text;
            hoten.AppendChild(ho);
            XmlNode ten = doc.CreateElement("ten");
            ten.InnerText = txt_ten.Text;
            hoten.AppendChild(ten);
            nv.AppendChild(hoten);
            XmlNode diachi = doc.CreateElement("diachi");
            diachi.InnerText = txt_diachi.Text;
            nv.AppendChild(diachi);
            goc.AppendChild(nv);
            doc.Save(path);
            HienThi();
        }

        private void sua_Click(object sender, EventArgs e)
        {
            doc.Load(path);
            XmlElement goc = doc.DocumentElement;
            XmlNode nv_cu = goc.SelectSingleNode("/ds/nhanvien[@manv='" + txt_manv.Text + "']");
            XmlNode nv_moi = doc.CreateElement("nhanvien");
            XmlAttribute manv = doc.CreateAttribute("manv");
            manv.InnerText = txt_manv.Text;
            nv_moi.Attributes.Append(manv);
            XmlNode hoten = doc.CreateElement("hoten");
            XmlNode ho = doc.CreateElement("ho");
            ho.InnerText = txt_ho.Text;
            hoten.AppendChild(ho);
            XmlNode ten = doc.CreateElement("ten");
            ten.InnerText = txt_ten.Text;
            hoten.AppendChild(ten);
            nv_moi.AppendChild(hoten);
            XmlNode diachi = doc.CreateElement("diachi");
            diachi.InnerText = txt_diachi.Text;
            nv_moi.AppendChild(diachi);
            goc.ReplaceChild(nv_moi, nv_cu);
            doc.Save(path);
            HienThi();
        }

        private void xoa_Click(object sender, EventArgs e)
        {
            doc.Load(path);
            XmlElement goc = doc.DocumentElement;
            XmlNode nv_xoa = doc.SelectSingleNode("/ds/nhanvien[@manv='" + txt_manv.Text + "']");
            goc.RemoveChild(nv_xoa);
            doc.Save(path);
            HienThi();
        }

        private void tim_Click(object sender, EventArgs e)
        {
            doc.Load(path);
            XmlElement goc = doc.DocumentElement;
            XmlNode nv_tim = doc.SelectSingleNode("/ds/nhanvien[@manv='" + txt_manv.Text + "']");
            if (nv_tim == null)
            {
                MessageBox.Show("Không có nhân viên nào có mã NV này!", "Thông báo");
                txt_manv.Text = "";
                return;
            }

            XmlNode hoten = nv_tim.SelectSingleNode("hoten");
            XmlNode ho = hoten.SelectSingleNode("ho");
            txt_ho.Text = ho.InnerText;
            XmlNode ten = hoten.SelectSingleNode("ten");
            txt_ten.Text = ten.InnerText;
            XmlNode diachi = nv_tim.SelectSingleNode("diachi");
            txt_diachi.Text = diachi.InnerText;
        }
    }
}
