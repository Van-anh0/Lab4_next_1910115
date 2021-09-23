using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4_next_1910115
{
    public partial class Form1 : Form
    {
        DanhSachSinhVien qlsv;
        int kt = 0;
        public Form1()
        {
            InitializeComponent();
        }

        public void LoadListView()
        {

            this.lvSinhVien.Items.Clear();
            foreach (SinhVien sv in qlsv.dsSinhVien)
            {
                ThemSV(sv);
            }
        }

        private void ThemSV(SinhVien sv)
        {
            ListViewItem lvitem = new ListViewItem(sv.MaSo);
            lvitem.SubItems.Add(sv.HoVaTen);
            lvitem.SubItems.Add(sv.Phai);
            lvitem.SubItems.Add(sv.NgaySinh.ToString("dd/MM/yyyy"));
            lvitem.SubItems.Add(sv.Lop);
            lvitem.SubItems.Add(sv.SoDienThoai);
            lvitem.SubItems.Add(sv.Email);
            lvitem.SubItems.Add(sv.DiaChi);
            lvitem.SubItems.Add(sv.Hinh);
            lvSinhVien.Items.Add(lvitem);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            qlsv = new DanhSachSinhVien();
            qlsv.DocTuFile();
            LoadListView();
        }

        private void btnMacDinh_Click(object sender, EventArgs e)
        {
            mtbMSSV.Text = "0000001";
            txtHoTen.Text = "";
            txtEmail.Text = "";
            txtDiaChi.Text = "";
            dtNgaySinh.Value = DateTime.Now;
            rdbNam.Checked = true;
            cbbLop.Text = cbbLop.Items[0].ToString();
            pbPicture.ImageLocation = "";
            txtHinh.Text = "";
            mtbSDT.Text = "";


        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            SaveChanges();
            Application.Exit();
        }

        private void btnPicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Select Picture"; // add photos
            dlg.Filter = "Image Files (JPEG, GIF, BMF, BMP, etc.)|"
                  + "*.jpg;*.jpeg;*.gif;*.bmp;"
                  + "*.tif;*.tiff;*.png|"
                  + "JPEG files (*.jpg;*.jpeg)|*.jpg;*.jpeg|"
                  + "GIF files (*.gif)|*.gif|"
                  + "BMP files (*.bmp)|*.bmp|"
                  + "TIFF files (*.tif;*.tiff)|*.tif;*tiff|"
                  + "PNG files (*.png)|*.png|"
                  + "All files (*.*)|*.*";
            dlg.InitialDirectory = Environment.CurrentDirectory;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var fileName = dlg.FileName;
                txtHinh.Text = fileName;
                pbPicture.Load(fileName);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            SinhVien sv = GetSinhVien();
            var mssv = qlsv.dsSinhVien.FirstOrDefault(i => i.MaSo == sv.MaSo);
            if (mssv == null)
            {
                // thêm sinh viên
                if (string.IsNullOrWhiteSpace(sv.HoVaTen) || string.IsNullOrWhiteSpace(sv.DiaChi) ||
                 string.IsNullOrWhiteSpace(sv.Lop) || string.IsNullOrWhiteSpace(sv.SoDienThoai))
                {
                    MessageBox.Show("Bạn phải nhập liệu đầy đủ các phần thông tin quan trọng mới có thể thêm sinh viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                qlsv.ThemSinhVien(sv);
            }
            else
            {
                // sửa sinh viên
                // mssv = sv; <= vầy không được đâu
                if (string.IsNullOrWhiteSpace(sv.HoVaTen) || string.IsNullOrWhiteSpace(sv.DiaChi) ||
                    string.IsNullOrWhiteSpace(sv.Lop) || string.IsNullOrWhiteSpace(sv.SoDienThoai))
                {
                    MessageBox.Show("Mã số sinh viên đã tồn tại, bạn phải nhập liệu đầy đủ mới có thể sửa thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                mssv.HoVaTen = sv.HoVaTen;
                mssv.Email = sv.Email;
                mssv.DiaChi = sv.DiaChi;
                mssv.NgaySinh = sv.NgaySinh;
                mssv.Phai = sv.Phai;
                mssv.Lop = sv.Lop;
                mssv.SoDienThoai = sv.SoDienThoai;
                mssv.Hinh = sv.Hinh;
              
            }
            kt = 1;
            LoadListView();
        }

        private SinhVien GetSinhVien()
        {
            SinhVien sv = new SinhVien();
            sv.MaSo = mtbMSSV.Text;
            sv.HoVaTen = txtHoTen.Text;
            sv.Email = txtEmail.Text;
            sv.DiaChi = txtDiaChi.Text;
            sv.NgaySinh = dtNgaySinh.Value;
            sv.Phai = "Nam";
            if (rdbNu.Checked)
            {
                sv.Phai = "Nữ";
            }
            sv.Lop = cbbLop.Text;
            sv.SoDienThoai = mtbSDT.Text;
            sv.Hinh = txtHinh.Text;
            return sv;
        }

        private void lvSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            var count = lvSinhVien.SelectedItems.Count;
            if (count > 0)
            {
                var item = lvSinhVien.SelectedItems[0];
                mtbMSSV.Text = item.SubItems[0].Text;
                txtHoTen.Text = item.SubItems[1].Text;
                if (item.SubItems[2].Text == "Nam")
                {
                    rdbNam.Checked = true;
                }
                else
                {
                    rdbNu.Checked = true;
                }
              
                dtNgaySinh.Value = DateTime.Parse(item.SubItems[3].Text);
                cbbLop.Text = item.SubItems[4].Text;
                mtbSDT.Text = item.SubItems[5].Text;
                txtEmail.Text = item.SubItems[6].Text;
                txtDiaChi.Text = item.SubItems[7].Text;
                txtHinh.Text = item.SubItems[8].Text;
                if (txtHinh.Text != "" && txtHinh.Text != "D:\\Images\\")
                {
                   // pbPicture.Visible = true;
                    pbPicture.Load(txtHinh.Text);
                }
                else
                {
                    pbPicture.Image = null;
                    //pbPicture.Visible = false;
                }

            }
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            var listItemChecked = lvSinhVien.CheckedItems;
            foreach (ListViewItem item in listItemChecked)
            {
                var maSo = item.SubItems[0].Text;
                qlsv.dsSinhVien.RemoveAll(i => i.MaSo == maSo);
                kt = 1;
            }
            LoadListView();
        }

       

        private void tsmiLoadLv_Click(object sender, EventArgs e)
        {
            LoadListView();
        }


        private void SaveChanges()
        {
            if (kt == 1)
            {
                DialogResult dialog = MessageBox.Show("Danh sách sinh viên đã thay đổi, bạn có muốn lưu danh sách đã thay" +
                    "đổi hay không?","Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
                if (dialog == DialogResult.Yes)
                {
                    qlsv.LuuThayDoi(qlsv.dsSinhVien);
                    MessageBox.Show("Cập nhật form thành công!");
                }
            }
        }

    }
}
