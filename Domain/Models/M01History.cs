
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class M01History : BaseModel
    {
        public string? FormTransactionHistoryID { get; set; }
        public string? FlagDetail { get; set; }
        public string? NomorIdentitas { get; set; }
        public string? Cif { get; set; }
        public string? NamaBadanUsaha { get; set; }
        public string? KodeJenisIdentitas { get; set; }
        public string? KodeJenisIdentitasOJKCode { get; set; }
        public string? KodeJenisIdentitasDesc { get; set; }
        public string? NamaPengurus { get; set; }
        public string? JenisKelamin { get; set; }
        public string? JenisKelaminOJKCode { get; set; }
        public string? JenisKelaminDesc { get; set; }
        public string? Alamat { get; set; }
        public string? Kelurahan { get; set; }
        public string? Kecamatan { get; set; }
        public string? KodeDati2 { get; set; }
        public string? KodeDati2OJKCode { get; set; }
        public string? KodeDati2Desc { get; set; }
        public string? KodeJabatan { get; set; }
        public string? KodeJabatanOJKCode { get; set; }
        public string? KodeJabatanDesc { get; set; }
        public int? PangsaKepemilikan { get; set; }
        public string? StatusPengurus { get; set; }
        public string? StatusPengurusOJKCode { get; set; }
        public string? StatusPengurusDesc { get; set; }
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
