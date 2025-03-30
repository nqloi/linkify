using System.Linq.Expressions;
using System.Xml.Linq;

namespace Linkify.Domain.Shared
{
    public class SortCriteria
    {
        public string FieldName { get; }
        public bool IsDescending { get; }

        public SortCriteria(string fieldName, bool isDescending = false)
        {
            FieldName = fieldName;
            IsDescending = isDescending;
        }

        public void Deconstruct(out string fieldName, out bool isDescending)
        {
            fieldName = FieldName;
            isDescending = IsDescending;
        }
    }
}
