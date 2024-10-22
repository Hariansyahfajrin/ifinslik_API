
namespace Domain.Models
{
    public class P01 : BaseModel
    {
        public string? FormTransactionID { get; set; }
        public string? FlagDetail { get; set; }
        public string? NomorIdentitas { get; set; }
        public string? NomorRekening { get; set; }
        public string? Cif { get; set; }
        public string? NamaLengkap { get; set; }
        public string? KodeJenisSegmenFasilitas { get; set; }
        public string? KodeJenisSegmenFasilitasOJKCode { get; set; }
        public string? KodeJenisSegmenFasilitasDesc { get; set; }
        public string? KodeJenisIdentitas { get; set; }
        public string? KodeJenisIdentitasOJKCode { get; set; }
        public string? KodeJenisIdentitasDesc { get; set; }
        public string? NamaPenjamin { get; set; }
        public string? NamaLengkapPenjamin { get; set; }
        public string? KodeGolonganPenjamin { get; set; }
        public string? KodeGolonganPenjaminOJKCode { get; set; }
        public string? KodeGolonganPenjaminDesc { get; set; }
        public string? AlamatPenjamin { get; set; }
        public int? PresentaseDijamin { get; set; }
        public string? Keterangan { get; set; }
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