using Core.Entities;

namespace Core.Specifications
{
    public class BandsSpecification : BaseSpecification<Band>
    {
        public BandsSpecification(BandSpecParams bandParams)
            : base()
        {
            ApplyPaging(bandParams.PageSize * (bandParams.PageIndex - 1), bandParams.PageSize);

            if (!string.IsNullOrEmpty(bandParams.Sort))
            {
                switch (bandParams.Sort)
                {
                    case "name":
                        AddOrderBy(v => v.Name);
                        break;
                    default:
                        AddOrderBy(v => v.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(v => v.Name);
            }
        }

        // Pass the criteria of Product Id == id to the Base Specification 
        // method and include the product type and product brand
        public BandsSpecification(int id)
            : base(x => x.Id == id)
        {
        }
    }
}