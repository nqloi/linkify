using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Common.Models
{
    public class FileData
    {
        public required string FileName { get; set; }
        public required byte[] Content { get; set; }
    }
}
