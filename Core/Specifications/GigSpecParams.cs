namespace Core.Specifications
{
    public class GigSpecParams
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;
        private int _pageSize = 30;
        public int PageSize
        {
            get => this._pageSize;
            set => this._pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public int? VenueId { get; set; }
        public string Band { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public string Sort { get; set; }
        private string _search;
        public string Search
        {
            get => this._search;
            set => this._search = value.ToLower();
        }
    }
}