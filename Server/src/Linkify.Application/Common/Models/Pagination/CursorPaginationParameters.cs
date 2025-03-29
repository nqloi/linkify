using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Common.Models
{
    public class CursorPaginationParameters : IPaginationParameters
    {
        private const int MaxLimit = 50;
        private int _limit = 10;

        public string? Cursor { get; init; }

        public int Limit
        {
            get => _limit;
            set => _limit = value > MaxLimit ? MaxLimit : value;
        }
    }
}
