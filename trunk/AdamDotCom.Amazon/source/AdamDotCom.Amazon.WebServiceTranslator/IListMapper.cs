using System.Collections.Generic;

namespace AdamDotCom.Amazon.WebServiceTranslator
{
    public interface IListMapper
    {
        List<ListItemDTO> GetList();
    }
}