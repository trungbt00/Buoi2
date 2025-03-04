using System;
using System.Data.SqlClient;

namespace Phong_ban__Quan_ly__Nhan_vien
{
    class Program
    {
        static string connectionString = "Data Source=172.16.7.236;Initial Catalog=ManagementEmployee;User ID=trungdev;Password=Abc1234567;";
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1. Them thong tin nhan vien");
                Console.WriteLine("2. Sua thong tin nhan vien");
                Console.WriteLine("3. Xoa nhan vien");
                Console.WriteLine("4. Xem thong tin nhan vien bang ID");
                Console.WriteLine("5. Tim kiem nhan vien bang ten");
                Console.WriteLine("6. Phan trang");
                Console.WriteLine("7. Thoat");
                Console.Write("Thao tac: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ThemNhanVien();
                        break;
                    case "2":
                        CapNhatNhanVien();
                        break;
                    case "3":
                        XoaNhanVien();
                        break;
                    case "4":
                        GetNhanVienById();
                        break;
                    case "5":
                        SearchNhanVienByTenNV();
                        break;
                    case "6":
                        GetPagedNhanVien();
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Khong hop le");
                        break;
                }
            }
        }

        static void ThemNhanVien()
        {
            Console.Write("ID: ");
            string ID = Console.ReadLine();
            Console.Write("Ma nhan vien: ");
            string maNV = Console.ReadLine();
            Console.Write("Ten nhan vien: ");
            string tenNV = Console.ReadLine();
            Console.Write("Ngay sinh: ");
            string ngaySinh = Console.ReadLine();
            Console.Write("Dia chi: ");
            string diaChi = Console.ReadLine();
            Console.Write("Cap bac: ");
            string capBac = Console.ReadLine();
            Console.Write("ID Phong ban: ");
            string IDphongBan = Console.ReadLine();
            Console.Write("ID Quan ly: ");
            string IDquanLy = Console.ReadLine();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("InsertNhanVien", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", ID);
                command.Parameters.AddWithValue("@MaNV", maNV);
                command.Parameters.AddWithValue("@TenNV", tenNV);
                command.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                command.Parameters.AddWithValue("@DiaChi", diaChi);
                command.Parameters.AddWithValue("@CapBac", capBac);
                command.Parameters.AddWithValue("@PhongBanId", IDphongBan);
                command.Parameters.AddWithValue("@QuanLyId", IDquanLy);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Thanh cong");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Loi: " + ex.Message);
                }
            }
        }

        static void CapNhatNhanVien()
        {
            Console.Write("ID cap nhat: ");
            string ID = Console.ReadLine();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("UpdateNhanVien", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", ID);
                Console.Write("Ma nhan vien: ");
                string maNV = Console.ReadLine();
                Console.Write("Ten nhan vien: ");
                string tenNV = Console.ReadLine();
                Console.Write("Ngay sinh: ");
                string ngaySinh = Console.ReadLine();
                Console.Write("Dia chi: ");
                string diaChi = Console.ReadLine();
                Console.Write("Cap bac: ");
                string capBac = Console.ReadLine();
                Console.Write("ID Phong ban: ");
                string IDphongBan = Console.ReadLine();
                Console.Write("ID Quan ly: ");
                string IDquanLy = Console.ReadLine();
                command.Parameters.AddWithValue("@MaNV", maNV);
                command.Parameters.AddWithValue("@TenNV", tenNV);
                command.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                command.Parameters.AddWithValue("@DiaChi", diaChi);
                command.Parameters.AddWithValue("@CapBac", capBac);
                command.Parameters.AddWithValue("@PhongBanId", IDphongBan);
                command.Parameters.AddWithValue("@QuanLyId", IDquanLy);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Thanh cong");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Loi: " + ex.Message);
                }
            }
        }

        static void XoaNhanVien()
        {
            Console.Write("ID xoa: ");
            string ID = Console.ReadLine();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("DeleteNhanVien", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", ID);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Thanh cong.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Loi: " + ex.Message);
                }
            }
        }
        static void GetNhanVienById()
        {
            Console.Write("ID: ");
            string id = Console.ReadLine();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("GetNhanVienById", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Console.WriteLine("Thong tin nhan vien:");
                        Console.WriteLine($"Id: {reader["Id"]}, MaNV: {reader["MaNV"]}, TenNV: {reader["TenNV"]}, NgaySinh: {reader["NgaySinh"]}, DiaChi: {reader["DiaChi"]}, CapBac: {reader["CapBac"]}, PhongBan: {reader["TenPB"]}, QuanLy: {reader["TenQL"]}");
                    }
                    else
                    {
                        Console.WriteLine("Khong tim thay nhan vien");
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Loi: " + ex.Message);
                }
            }
        }

        static void SearchNhanVienByTenNV()
        {
            Console.Write("Ten nhan vien: ");
            string tenNV = Console.ReadLine();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SearchNhanVienByTenNV", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TenNV", tenNV);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine($"Id: {reader["Id"]}, MaNV: {reader["MaNV"]}, TenNV: {reader["TenNV"]}, NgaySinh: {reader["NgaySinh"]}, DiaChi: {reader["DiaChi"]}, CapBac: {reader["CapBac"]}, PhongBan: {reader["TenPB"]}, QuanLy: {reader["TenQL"]}");
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Loi: " + ex.Message);
                }
            }
        }

        static void GetPagedNhanVien()
        {
            Console.Write("Trang: ");
            string pageNumber = Console.ReadLine();
            Console.Write("Gioi han: ");
            string pageSize = Console.ReadLine();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("GetPagedNhanVien", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PageNumber", pageNumber);
                command.Parameters.AddWithValue("@PageSize", pageSize);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine($"Id: {reader["Id"]}, MaNV: {reader["MaNV"]}, TenNV: {reader["TenNV"]}, NgaySinh: {reader["NgaySinh"]}, DiaChi: {reader["DiaChi"]}, CapBac: {reader["CapBac"]}, PhongBan: {reader["TenPB"]}, QuanLy: {reader["TenQL"]}");
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Loi: " + ex.Message);
                }
            }
        }
    }
}
