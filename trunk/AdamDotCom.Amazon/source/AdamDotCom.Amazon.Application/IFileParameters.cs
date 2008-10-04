namespace AdamDotCom.Amazon.Application
{
    public interface IFileParameters
    {
        string ErrorFileNameAndPath { get; set; }
        string ReviewFileNameAndPath { get; set; }
        string ProductFileNameAndPath { get; set; }
    }
}