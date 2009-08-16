using System.Collections.Generic;

namespace AdamDotCom.Amazon.WebServiceTranslator.Interfaces
{
    public interface IListMapper
    {
        List<ListItemDTO> GetList();
        
        IList<KeyValuePair<string, string>> GetErrors();
    }
}