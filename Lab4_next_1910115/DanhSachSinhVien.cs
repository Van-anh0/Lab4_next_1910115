using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_next_1910115
{
    public class DanhSachSinhVien
    {
        string fileName = "DS\\DanhSachSV.txt";
        public List<SinhVien> dsSinhVien;
        public DanhSachSinhVien()
        {
            dsSinhVien = new List<SinhVien>();
        }

        public void ThemSinhVien(SinhVien a)
        {
            dsSinhVien.Add(a);
        }

        public void DocTuFile()
        {
            string  t;
            string[] s;
            SinhVien sv;

            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(stream))
                {
                    while ((t = reader.ReadLine()) != null)
                    {
                        s = t.Split('*');
                        sv = new SinhVien();
                        sv.MaSo = s[0];
                        sv.HoVaTen = s[1];
                        sv.Phai = s[2];
                        sv.NgaySinh = DateTime.Parse(s[3]);
                        sv.Lop = s[4];
                        sv.SoDienThoai = s[5];
                        sv.Email = s[6];
                        sv.DiaChi = s[7];
                        sv.Hinh = s[8];
                        ThemSinhVien(sv);
                    }

                }
            }
        }

        public void LuuThayDoi(List<SinhVien> ds)
        {
            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new StreamWriter(stream))
                {
                    foreach (var item in ds)
                    {
                        writer.WriteLine("{0}*{1}*{2}*{3}*{4}*{5}*{6}*{7}*{8}", item.MaSo,
                            item.HoVaTen,item.Phai,item.NgaySinh,item.Lop,item.SoDienThoai,
                            item.Email,item.DiaChi,item.Hinh);
                    }
                }
            }
        }
    }
}
