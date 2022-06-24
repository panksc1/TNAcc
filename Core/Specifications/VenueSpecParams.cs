namespace Core.Specifications
{
    public class VenueSpecParams
    {
        private const int MaxPageSize = Int32.MaxValue;
        public int PageIndex { get; set; } = 1;
        private int _pageSize = MaxPageSize;
        public int PageSize
        {
            get => this._pageSize;
            set => this._pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public string At { get; set; }
        public string Sort { get; set; }
        private string _search;
        public string Search
        {
            get => this._search;
            set => this._search = value.ToLower();
        }
    }
}