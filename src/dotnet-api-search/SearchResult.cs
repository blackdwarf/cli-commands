namespace DotnetApiSearch {
    public class SearchResult
    {
        public PackageDetails PackageDetails { get; set; }
        public string FullTypeName { get; set; }
        public string Signature { get; set; }
        public string[] Tfms { get; set; }
        public string FullTypeNameHighlighted { get; set; }
        public string SignatureHighlighted { get; set; }
    }
}