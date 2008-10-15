namespace AdamDotCom.Amazon.Application.Interfaces
{
    public interface IFileParameters
    {
        string ErrorFileNameAndPath { get; set; }
        string ReviewFileNameAndPath { get; set; }
        string ProductFileNameAndPath { get; set; }
    }
}