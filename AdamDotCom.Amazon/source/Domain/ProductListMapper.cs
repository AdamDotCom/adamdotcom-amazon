using System;
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
        private IList<KeyValuePair<string, string>> errors;

        public ProductListMapper(IAmazonRequest amazonRequest)
        {
            this.amazonRequest = amazonRequest;
            errors = new List<KeyValuePair<string, string>>();

            try
            {
                var listMapper = new ListMapper(amazonRequest.AWSAccessKeyId, amazonRequest.AssociateTag, amazonRequest.ListId);

                using (listMapper)
                {
                    listItems = listMapper.GetList();

                    foreach (var error in listMapper.GetErrors())
                    {
                        errors.Add(error);
                    }

                }

                var productMapper = new ProductMapper(amazonRequest.AWSAccessKeyId, amazonRequest.AssociateTag);

                using (productMapper)
                {
                    products = productMapper.GetProducts(listItems.ConvertAll(listItem => listItem.ASIN));

                    foreach (var error in productMapper.GetErrors())
                    {
                        errors.Add(error);
                    }
                }
            }
            catch(Exception ex)
            {
                errors.Add(new KeyValuePair<string, string>("ServiceError", ex.Message));
            }
        }

        public virtual List<Product> GetList()
        {
            return MapProducts(products);
        }

        public virtual IList<KeyValuePair<string, string>> GetErrors()
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