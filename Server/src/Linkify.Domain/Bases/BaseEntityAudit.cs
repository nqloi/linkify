using Linkify.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Domain.Bases
{
    public class BaseEntityAudit : BaseEntity, ISoftDelete
    {
        public bool IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Guid? CreatedById { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedById { get; set; }

        public BaseEntityAudit()
        {

        }

        protected BaseEntityAudit(Guid? userId)
        {
            IsDeleted = false;
            CreatedAt = DateTime.UtcNow;
            CreatedById = userId;
        }

        public BaseEntityAudit SetAsDeleted()
        {
            IsDeleted = true;
            return this;
        }

        public BaseEntityAudit SetAudit(Guid? userId)
        {
            UpdatedAt = DateTime.UtcNow;
            UpdatedById = userId;
            return this;
        }
    }
}
