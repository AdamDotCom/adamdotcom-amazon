using System.Collections.Generic;

namespace AdamDotCom.Amazon.WebServiceTranslator.Interfaces
{
    public interface IListMapper
    {
        List<ListItemDTO> GetList();
        List<string> GetErrors();
    }
}