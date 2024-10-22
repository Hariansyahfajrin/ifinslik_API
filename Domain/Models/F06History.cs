namespace Domain.Models
{
    public class F06History : BaseModel
    {
        public string? FormTransactionHistoryID { get; set; }
        public string? FlagDetail { get; set; }
        public string? NomorRekening { get; set; }
        public string? Cif { get; set; }
        public string? NamaLengkap { get; set; }
        public string? KodeJenisFasilitasLainnya { get; set; }
        public string? KodeJenisFasilitasLainnyaOJKCode { get; set; }
        public string? KodeJenisFasilitasLainnyaDesc { get; set; }
        public string? SumberDana { get; set; }
        public string? SumberDanaOJKCode { get; set; }
        public string? SumberDanaDesc { get; set; }
        public DateTime? TanggalMulai { get; set; }
        public DateTime? TanggalJatuhTempo { get; set; }
        public int? PresentaseSukuBunga { get; set; }
        public string? KodeValuta { get; set; }
        public string? KodeValutaOJKCode { get; set; }
        public string? KodeValutaDesc { get; set; }
        public int? NominalJumlahKewajiban { get; set; }
        public int? NilaiDalamMataUangAsal { get; set; }
        public string? KodeKolektibilitas { get; set; }
        public string? KodeKolektibilitasOJKCode { get; set; }
        public string? KodeKolektibilitasDesc { get; set; }
        public DateTime? TanggalMacet { get; set; }
        public string? KodeSebabMacet { get; set; }
        public string? KodeSebabMacetOJKCode { get; set; }
        public string? KodeSebabMacetDesc { get; set; }
        public int? Tunggakan { get; set; }
        public int? JumlahHariTunggakan { get; set; }
        public string? KodeKondisi { get; set; }
        public string? KodeKondisiOJKCode { get; set; }
        public string? KodeKondisiDesc { get; set; }
        public DateTime? TanggalKondisi { get; set; }
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


