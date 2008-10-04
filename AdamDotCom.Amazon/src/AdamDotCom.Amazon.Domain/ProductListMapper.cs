using System.Collections.Generic;
using AdamDotCom.Amazon.WebServiceTranslator;

namespace AdamDotCom.Amazon.Domain
{
    public class ProductListMapper: IProductListMapper
    {
        private IAmazonRequest amazonRequest;
        private IList<ListItemDTO> listItems;
        private IList<ProductDTO> products;
        private List<string> errors;

        public ProductListMapper(IAmazonRequest amazonRequest)
        {
            this.amazonRequest = amazonRequest;

            ListMapper listMapper = new ListMapper(amazonRequest.AWSAccessKeyId, amazonRequest.AssociateTag, this.amazonRequest.ListId);
            listItems = listMapper.GetList();

            errors = listMapper.GetErrors();

            List<string> ASINList = new List<string>();
            foreach (ListItemDTO listItem in listItems)
            {
                ASINList.Add(listItem.ASIN);
            }

            ProductMapper productMapper = new ProductMapper(amazonRequest.AWSAccessKeyId, amazonRequest.AssociateTag);
            products = productMapper.GetProducts(ASINList);

            foreach (string error in productMapper.GetErrors())
            {
                errors.Add(error);
            }
        }

        public virtual List<Product> GetList()
        {
            return MapProducts(products);
        }

        public virtual List<string> GetErrors()
        {
            return errors;
        }

        private Product MapProduct(IProductDTO product)
        {
            Product productToReturn = new Product();

            productToReturn.ASIN = product.ASIN;
            productToReturn.Authors = FormatHelper.MapAuthors(product.Authors);
            productToReturn.AuthorsMLA = FormatHelper.MapAuthorsInMlaFormat(product.Authors);
            productToReturn.ImageUrl = AmazonAssetHelper.ProductImageUrl(amazonRequest.AssociateTag, product.ASIN);
            productToReturn.ProductPreviewUrl = AmazonAssetHelper.ProductPreviewUrl(amazonRequest.AssociateTag, product.ASIN);
            productToReturn.Publisher = product.Publisher;
            productToReturn.Title = product.Title;
            productToReturn.Url = product.Url;

            return productToReturn;
        }

        private List<Product> MapProducts(IList<ProductDTO> products)
        {
            List<Product> productsToReturn = new List<Product>();

            foreach(IProductDTO product in products)
            {
                productsToReturn.Add(MapProduct(product));
            }

            return productsToReturn;
        }
    }
}