

namespace Domain.Models
{
    public class D02History : BaseModel
    {

        public string? FormTransactionHistoryID { get; set; }
        public string? FlagDetail { get; set; }
        public string? Cif { get; set; }
        public string? NomorIdentitasBadanUsaha { get; set; }
        public string? NamaBadanUsaha { get; set; }
        public string? KodeJenisBadanUsaha { get; set; }
        public string? KodeJenisBadanUsahaOjkCode { get; set; }
        public string? KodeJenisBadanUsahaDesc { get; set; }
        public string? TempatPendirian { get; set; }
        public string? NomorAktaPendirian { get; set; }
        public DateTime? TanggalAktaPendirian { get; set; }
        public string? NomorAktaPerubahanTerakhir { get; set; }
        public DateTime? TanggalAktaPerubahanTerakhir { get; set; }
        public string? NomorTelepon { get; set; }
        public string? NomorHandphone { get; set; }
        public string? Email { get; set; }
        public string? Alamat { get; set; }
        public string? Kelurahan { get; set; }
        public string? Kecamatan { get; set; }
        public string? KodeDati2 { get; set; }
        public string? KodeDati2OjkCode { get; set; }
        public string? KodeDati2Desc { get; set; }
        public string? KodePos { get; set; }
        public string? KodeNegaraDomisili { get; set; }
        public string? KodeNegaraDomisiliOjkCode { get; set; }
        public string? KodeNegaraDomisiliDesc { get; set; }
        public string? KodeBidangUsaha { get; set; }
        public string? KodeBidangUsahaOjkCode { get; set; }
        public string? KodeBidangUsahaDesc { get; set; }
        public string? KodeHubunganDenganPelapor { get; set; }
        public string? KodeHubunganDenganPelaporOjkCode { get; set; }
        public string? KodeHubunganDenganPelaporDesc { get; set; }
        public string? MelanggarBmpk { get; set; }
        public string? MelanggarBmpkOjkCode { get; set; }
        public string? MelanggarBmpkDesc { get; set; }
        public string? MelampauiBmpk { get; set; }
        public string? MelampauiBmpkOjkCode { get; set; }
        public string? MelampauiBmpkDesc { get; set; }
        public string? GoPublic { get; set; }
        public string? GoPublicOjkCode { get; set; }
        public string? GoPublicDesc { get; set; }
        public string? KodeGolonganDebitur { get; set; }
        public string? KodeGolonganDebiturOjkCode { get; set; }
        public string? KodeGolonganDebiturDesc { get; set; }
        public string? PeringkatDebitur { get; set; }
        public string? LembagaPemeringkat { get; set; }
        public string? LembagaPemeringkatOjkCode { get; set; }
        public string? LembagaPemeringkatDesc { get; set; }
        public DateTime? TanggalPemeringkatan { get; set; }
        public string? NamaGrupDebitur { get; set; }
        public string? KodeKantorCabang { get; set; }
        public string? OperasiData { get; set; }
        public string? OperasiDataOjkCode { get; set; }
        public string? OperasiDataDesc { get; set; }
        public string? Period { get; set; }

        #region SysCompany    
        public string? SysCompanyKantorCabang { get; set; }
        #endregion


    }
}




