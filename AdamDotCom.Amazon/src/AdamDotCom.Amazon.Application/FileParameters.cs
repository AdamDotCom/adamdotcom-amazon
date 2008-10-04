namespace AdamDotCom.Amazon.Application
{
    public class FileParameters : IFileParameters
    {
        public string ErrorFileNameAndPath { get; set; }
        public string ReviewFileNameAndPath { get; set; }
        public string ProductFileNameAndPath { get; set; }
    }
}