//Source taken from: http://s3.amazonaws.com/awsdownloads/a2s_simplestore_CS_VS2005.zip

/**********************************************************************************************
 * Copyright 2009 Amazon.com, Inc. or its affiliates. All Rights Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License"). You may not use this file 
 * except in compliance with the License. A copy of the License is located at
 *
 *       http://aws.amazon.com/apache2.0/
 *
 * or in the "LICENSE.txt" file accompanying this file. This file is distributed on an "AS IS"
 * BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations under the License. 
 *
 * ********************************************************************************************
 *
 *  Amazon Product Advertising API
 *  Signed Requests Sample Code
 *
 *  API Version: 2009-03-31
 *
 */

using System;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using Microsoft.Web.Services3;
using Microsoft.Web.Services3.Design;

namespace AdamDotCom.Amazon.WebServiceTranslator
{
    public class AmazonHmacAssertion : PolicyAssertion
    {
        // Namespace and Prefix for the security elements we'll add to the header.
        public const String AWS_NS = "http://security.amazonaws.com/doc/2007-01-01/";   
        public const String AWS_PFX = "aws";

        // AWS Access Key ID and corresponding Secret Key
        private readonly String akid;
        private readonly String secret;

        // Constructor
        public AmazonHmacAssertion(String awsAccessKeyId, String awsSecretKey)
        {
            akid = awsAccessKeyId;
            secret = awsSecretKey;
        }

        // Creates the policy that will be applied to all outgoing requests
        public Policy Policy()
        {
            var policy = new Policy();
            policy.Assertions.Add(this);

            return policy;
        }

        // The Client-side input filter does nothing since we're not interested in response.
        public override Microsoft.Web.Services3.SoapFilter CreateClientInputFilter(FilterCreationContext context)
        {
            return new ClientInputFilter();
        }

        // The Client-side output filter adds the security elements to the outgoing SOAP envelope.
        public override SoapFilter CreateClientOutputFilter(FilterCreationContext context)
        {
            return new ClientOutputFilter(akid, secret);
        }

        // Don't care about Server-side filters.
        public override SoapFilter CreateServiceInputFilter(FilterCreationContext context)
        {
            throw new NotImplementedException();
        }

        // Don't care about Server-side filters.
        public override SoapFilter CreateServiceOutputFilter(FilterCreationContext context)
        {
            throw new NotImplementedException();
        }
    }

    // Client-side input filter (response) - do nothing.
    internal class ClientInputFilter : SoapFilter
    {
        public override SoapFilterResult ProcessMessage(SoapEnvelope envelope)
        {
            return SoapFilterResult.Continue;
        }
    }

    // Client-side output filter (request) - add security elements to SOAP Header
    internal class ClientOutputFilter : SoapFilter
    {
        // to store the AWS Access Key ID and corresponding Secret Key.
        private readonly String akid;
        private readonly String secret;

        // Constructor
        public ClientOutputFilter(String awsAccessKeyId, String awsSecretKey)
        {
            akid = awsAccessKeyId;
            secret = awsSecretKey;
        }

        // Here's the core logic:
        // 1. Concatenate operation name and timestamp to get StringToSign.
        // 2. Compute HMAC on StringToSign with Secret Key to get Signature.
        // 3. Add AWSAccessKeyId, Timestamp and Signature elements to the header.
        public override SoapFilterResult ProcessMessage(SoapEnvelope envelope)
        {
            var body = envelope.Body;
            var firstNode = body.ChildNodes.Item(0);
            String operation = firstNode.Name;

            DateTime currentTime = DateTime.UtcNow;
            String timestamp = currentTime.ToString("yyyy-MM-ddTHH:mm:ssZ");

            String toSign = operation + timestamp;
            byte[] toSignBytes = Encoding.UTF8.GetBytes(toSign);
            byte[] secretBytes = Encoding.UTF8.GetBytes(secret);
            HMAC signer = new HMACSHA256(secretBytes); // important! has to be HMAC-SHA-256, SHA-1 will not work.

            byte[] sigBytes = signer.ComputeHash(toSignBytes);
            String signature = Convert.ToBase64String(sigBytes); // important! has to be Base64 encoded

            var header = envelope.Header;
            XmlDocument doc = header.OwnerDocument;

            // create the elements - Namespace and Prefix are critical!
            XmlElement akidElement = doc.CreateElement(AmazonHmacAssertion.AWS_PFX, "AWSAccessKeyId", AmazonHmacAssertion.AWS_NS);
            akidElement.AppendChild(doc.CreateTextNode(akid));

            XmlElement tsElement = doc.CreateElement(AmazonHmacAssertion.AWS_PFX, "Timestamp", AmazonHmacAssertion.AWS_NS);
            tsElement.AppendChild(doc.CreateTextNode(timestamp));

            XmlElement sigElement = doc.CreateElement(AmazonHmacAssertion.AWS_PFX, "Signature", AmazonHmacAssertion.AWS_NS);
            sigElement.AppendChild(doc.CreateTextNode(signature));

            header.AppendChild(akidElement);
            header.AppendChild(tsElement);
            header.AppendChild(sigElement);

            // we're done
            return SoapFilterResult.Continue;
        }
    }
}