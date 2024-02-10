using FluentValidation;
using FluentValidation.Results;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vanilla.Shared.Domain
{
    public abstract class Entity<TId, TEntity> :
        AbstractValidator<TEntity>
        where TId : struct
        where TEntity : Entity<TId, TEntity>
    {
        public TId Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }

        [NotMapped]
        public ValidationResult ValidationResult { get; protected set; }

        public abstract bool Validate();

        protected Entity()
        {
            Id = default(TId);
            CreatedAt = DateTime.Now;
            ValidationResult = new ValidationResult();
        }

        public TEntity? WithUpdatedAt(DateTime? updatedAt)
        {
            if ( updatedAt == null )
                return null;

            UpdatedAt = updatedAt.Value;
            return this as TEntity;
        }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity<TId, TEntity>;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity<TId, TEntity> a, Entity<TId, TEntity> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity<TId, TEntity> a, Entity<TId, TEntity> b) => !(a == b);

        public override int GetHashCode() => (GetType().GetHashCode() * 907) + Id.GetHashCode();

        public override string ToString() => GetType().Name + " [Id=" + Id + "]";
    }

    public interface IEntity
    {
        object WithUpdatedAt(DateTime? updatedAt);

    }
}
