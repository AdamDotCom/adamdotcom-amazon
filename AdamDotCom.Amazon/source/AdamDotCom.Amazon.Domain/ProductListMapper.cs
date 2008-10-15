using System.Collections.Generic;
using AdamDotCom.Amazon.Domain.Extensions;
using AdamDotCom.Amazon.Domain.Interfaces;
using AdamDotCom.Amazon.WebServiceTranslator;
using AdamDotCom.Amazon.WebServiceTranslator.Interfaces;

namespace AdamDotCom.Amazon.Domain
{
    public class ProductListMapper: IProductListMapper
    {
        private IAmazonRequest amazonRequest;
        private List<ListItemDTO> listItems;
        private List<ProductDTO> products;
        private List<string> errors;

        public ProductListMapper(IAmazonRequest amazonRequest)
        {
            this.amazonRequest = amazonRequest;
            errors = new List<string>();

            ListMapper listMapper = new ListMapper(amazonRequest.AWSAccessKeyId, amazonRequest.AssociateTag, amazonRequest.ListId);

            using(listMapper)
            {
                listItems = listMapper.GetList();

                foreach (string error in listMapper.GetErrors())
                {
                    errors.Add(error);
                }

            }

            ProductMapper productMapper = new ProductMapper(amazonRequest.AWSAccessKeyId, amazonRequest.AssociateTag);

            using (productMapper)
            {
                products = productMapper.GetProducts(listItems.ConvertAll(listItem => listItem.ASIN));

                foreach (string error in productMapper.GetErrors())
                {
                    errors.Add(error);
                }
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
            var productToReturn = new Product
            {
                ASIN = product.ASIN,
                Authors = product.Authors(),
                AuthorsMLA = product.AuthorsInMlaFormat(),
                ImageUrl = product.ProductImageUrl(amazonRequest.AssociateTag),
                ProductPreviewUrl = product.ProductPreviewUrl(amazonRequest.AssociateTag),
                Publisher = product.Publisher,
                Title = product.Title,
                Url = product.Url
            };

            return productToReturn;
        }

        private List<Product> MapProducts(List<ProductDTO> products)
        {
            return products.ConvertAll(product => MapProduct(product));
        }
    }
}