namespace AdamDotCom.Amazon.Service
{
    public class AmazonService : IAmazon
    {

        public string Greet(string name)
        {

            return string.Format("Hello, {0}", name);

        }
    }
}
