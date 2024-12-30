using Linkify.Domain.Interfaces;

namespace Linkify.Domain.Bases
{
    public class BaseEntityAudit : BaseEntity, ISoftDelete
    {
        public bool IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Guid? CreatedById { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedById { get; set; }

        public BaseEntityAudit() // Required for EF
        {

        }

        protected BaseEntityAudit(Guid? userId) : base()
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
