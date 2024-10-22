
namespace Domain.Models
{
    public class D01History : BaseModel
    {
        public string? FormTransactionHistoryID { get; set; }
        public string? FlagDetail { get; set; }
        public string? Cif { get; set; }
        public string? JenisIdentitas { get; set; }
        public string? JenisIdentitasOJKCode { get; set; }
        public string? JenisIdentitasDesc { get; set; }
        public string? Nik { get; set; }
        public string? Nama { get; set; }
        public string? NamaLengkap { get; set; }
        public string? KodeStatusPendidikan { get; set; }
        public string? KodeStatusPendidikanOJKCode { get; set; }
        public string? KodeStatusPendidikanDesc { get; set; }
        public string? JenisKelamin { get; set; }
        public string? JenisKelaminOJKCode { get; set; }
        public string? JenisKelaminDesc { get; set; }
        public string? TempatLahir { get; set; }
        public DateTime? TanggalLahir { get; set; }
        public string? Npwp { get; set; }
        public string? Alamat { get; set; }
        public string? Kelurahan { get; set; }
        public string? Kecamatan { get; set; }
        public string? KodeDati2 { get; set; }
        public string? KodeDati2OJKCode { get; set; }
        public string? KodeDati2Desc { get; set; }
        public string? KodePos { get; set; }
        public string? NomorTelepon { get; set; }
        public string? NomorHandphone { get; set; }
        public string? Email { get; set; }
        public string? KodeNegaraDomisili { get; set; }
        public string? KodeNegaraDomisiliOJKCode { get; set; }
        public string? KodeNegaraDomisiliDesc { get; set; }
        public string? KodePekerjaan { get; set; }
        public string? KodePekerjaanOJKCode { get; set; }
        public string? KodePekerjaanDesc { get; set; }
        public string? TempatBekerja { get; set; }
        public string? KodeBidangUsaha { get; set; }
        public string? KodeBidangUsahaOJKCode { get; set; }
        public string? KodeBidangUsahaDesc { get; set; }
        public string? AlamatTempatKerja { get; set; }
        public int? PenghasilanKotorPerTahun { get; set; }
        public string? KodeSumberPenghasilan { get; set; }
        public string? KodeSumberPenghasilanOJKCode { get; set; }
        public string? KodeSumberPenghasilanDesc { get; set; }
        public int? JumlahTanggungan { get; set; }
        public string? KodeHubunganDenganPelapor { get; set; }
        public string? KodeHubunganDenganPelaporOJKCode { get; set; }
        public string? KodeHubunganDenganPelaporDesc { get; set; }
        public string? KodeGolonganDebitur { get; set; }
        public string? KodeGolonganDebiturOJKCode { get; set; }
        public string? KodeGolonganDebiturDesc { get; set; }
        public string? StatusPerkawinanDebitur { get; set; }
        public string? StatusPerkawinanDebiturOJKCode { get; set; }
        public string? StatusPerkawinanDebiturDesc { get; set; }
        public string? NomorIdentitasPasangan { get; set; }
        public string? NamaPasangan { get; set; }
        public DateTime? TanggalLahirPasangan { get; set; }
        public string? PerjanjianPisahHarta { get; set; }
        public string? PerjanjianPisahHartaOJKCode { get; set; }
        public string? PerjanjianPisahHartaDesc { get; set; }
        public string? MelanggarBmpk { get; set; }
        public string? MelanggarBmpkOJKCode { get; set; }
        public string? MelanggarBmpkDesc { get; set; }
        public string? MelampauiBmpk { get; set; }
        public string? MelampauiBmpkOJKCode { get; set; }
        public string? MelampauiBmpkDesc { get; set; }
        public string? NamaGadisIbuKandung { get; set; }
        public string? KodeKantorCabang { get; set; }
        public string? OperasiData { get; set; }
        public string? OperasiDataOJKCode { get; set; }
        public string? OperasiDataDesc { get; set; }
        public string? Period { get; set; }

        #region SysCompany    
        public string? SysCompanyKantorCabang { get; set; }
        #endregion


    }
}