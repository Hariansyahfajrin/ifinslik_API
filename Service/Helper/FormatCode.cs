namespace Service.Helper
{
    public class FormatCode
    {
        public string Prefix { get; set; } = "";
        public DateTime? Date { get; set; }
        public int RunNumberLen { get; set; }
        public string Delimiter { get; set; } = "";
        public string DateFormat { get; set; } = "yyMM";
    }
}