-- Tạo bảng quan hệ gồm: Phòng ban + quản lý + nhân viên tạo quan hệ các bảng
-- Sau đó làm crud + tìm kiếm + phân trang

-- Tạo bảng
CREATE TABLE PhongBan (
    Id VARCHAR(10) PRIMARY KEY,
    TenPB NVARCHAR(100)
);


CREATE TABLE QuanLy (
    Id VARCHAR(10) PRIMARY KEY,
    MaQL VARCHAR(20) UNIQUE,
    TenQL NVARCHAR(100),
    NgaySinh DATE,
    DiaChi NVARCHAR(255),
    CapBac VARCHAR(50),
	PhongBanId VARCHAR(10) FOREIGN KEY REFERENCES PhongBan(Id)
);


CREATE TABLE NhanVien (
    Id VARCHAR(10) PRIMARY KEY,
    MaNV VARCHAR(20) UNIQUE,
    TenNV NVARCHAR(100),
    NgaySinh DATE,
    DiaChi NVARCHAR(255),
    CapBac VARCHAR(50),
	PhongBanId VARCHAR(10) FOREIGN KEY REFERENCES PhongBan(Id),
    QuanLyId VARCHAR(10) FOREIGN KEY REFERENCES QuanLy(Id)
);

-- Insert NhanVien
CREATE PROCEDURE InsertNhanVien
    @Id VARCHAR(10),
    @MaNV VARCHAR(20),
    @TenNV NVARCHAR(100),
    @NgaySinh DATE,
    @DiaChi NVARCHAR(255),
    @CapBac VARCHAR(50),
    @PhongBanId VARCHAR(10),
    @QuanLyId VARCHAR(10)
AS
BEGIN
    INSERT INTO NhanVien (Id, MaNV, TenNV, NgaySinh, DiaChi, CapBac, PhongBanId, QuanLyId)
    VALUES (@Id, @MaNV, @TenNV, @NgaySinh, @DiaChi, @CapBac, @PhongBanId, @QuanLyId);
END;

-- Update NhanVien
CREATE PROCEDURE UpdateNhanVien
    @Id VARCHAR(10),
    @MaNV VARCHAR(20),
    @TenNV NVARCHAR(100),
    @NgaySinh DATE,
    @DiaChi NVARCHAR(255),
    @CapBac VARCHAR(50),
    @PhongBanId VARCHAR(10),
    @QuanLyId VARCHAR(10)
AS
BEGIN
    UPDATE NhanVien
    SET MaNV = @MaNV,
        TenNV = @TenNV,
        NgaySinh = @NgaySinh,
        DiaChi = @DiaChi,
        CapBac = @CapBac,
        PhongBanId = @PhongBanId,
        QuanLyId = @QuanLyId
    WHERE Id = @Id;
END;

-- Delete NhanVien
CREATE PROCEDURE DeleteNhanVien
    @Id VARCHAR(10)
AS
BEGIN
    DELETE FROM NhanVien WHERE Id = @Id;
END;

-- Get NhanVien by Id
CREATE PROCEDURE GetNhanVienById
    @Id VARCHAR(10)
AS
BEGIN
    SELECT 
	nv.Id,
	nv.MaNV,
	nv.TenNV,
	nv.NgaySinh,
	nv.DiaChi,
	nv.CapBac,
	pb.TenPB,
	ql.TenQL
	FROM NhanVien as nv
	JOIN PhongBan pb ON nv.PhongBanId = pb.Id
    JOIN QuanLy ql ON nv.QuanLyId = ql.Id
	WHERE nv.Id = @Id;	
END;

EXEC GetNhanVienById NV3;

-- Search NhanVien by TenNV
CREATE PROCEDURE SearchNhanVienByTenNV
    @TenNV NVARCHAR(100)
AS
BEGIN
    SELECT 
	nv.Id,
	nv.MaNV,
	nv.TenNV,
	nv.NgaySinh,
	nv.DiaChi,
	nv.CapBac,
	pb.TenPB,
	ql.TenQL
	FROM NhanVien as nv
	JOIN PhongBan pb ON nv.PhongBanId = pb.Id
    JOIN QuanLy ql ON nv.QuanLyId = ql.Id
	WHERE nv.TenNV LIKE '%' + @TenNV + '%';
END;

EXEC SearchNhanVienByTenNV TU;

-- Get Paged NhanVien
CREATE PROCEDURE GetPagedNhanVien
    @PageNumber INT,
    @PageSize INT
AS
BEGIN
    SELECT 
	nv.Id,
	nv.MaNV,
	nv.TenNV,
	nv.NgaySinh,
	nv.DiaChi,
	nv.CapBac,
	pb.TenPB,
	ql.TenQL
	FROM NhanVien as nv
	JOIN PhongBan pb ON nv.PhongBanId = pb.Id
    JOIN QuanLy ql ON nv.QuanLyId = ql.Id
    ORDER BY Id
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END;

EXEC GetPagedNhanVien
    @PageNumber = 1,
    @PageSize = 8;
