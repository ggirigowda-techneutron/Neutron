#region Copyright TechNeutron © 2019

//
// NAME:			Entity.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			TechNeutron
// DATE:			2/20/2019
// PURPOSE:			Base entity class
//

#endregion

using System;
using System.Collections.Generic;

namespace Classlibrary.Infrastructure
{
    /// <summary>
    ///     Base class for entities
    /// </summary>
    public abstract class Entity<TEntity>
    {
        #region Members

        private int? _requestedHashCode;

        #endregion

        #region Properties

        /// <summary>
        ///     Get the persisten object identifier
        /// </summary>
        public virtual TEntity Id { get; set; }

        /// <summary>
        ///     Set the id
        /// </summary>
        /// <param name="id"></param>
        public void SetId(TEntity id)
        {
            if (EqualityComparer<TEntity>.Default.Equals(id, default(TEntity)))
            {
                throw new ArgumentException("id: Invalid");
            }
            Id = id;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Check if this entity is transient, ie, without identity at this moment
        /// </summary>
        /// <returns>True if entity is transient, else false</returns>
        public bool IsTransient()
        {
            return (EqualityComparer<TEntity>.Default.Equals(Id, default(TEntity)));
        }

        #endregion

        #region Overrides Methods

        /// <summary>
        ///     <see cref="M:System.Object.Equals" />
        /// </summary>
        /// <param name="obj">
        ///     <see cref="M:System.Object.Equals" />
        /// </param>
        /// <returns>
        ///     <see cref="M:System.Object.Equals" />
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity<TEntity>))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            var item = (Entity<TEntity>) obj;
            if (item.IsTransient() || IsTransient())
                return false;
            return EqualityComparer<TEntity>.Default.Equals(item.Id, Id);
        }

        /// <summary>
        ///     <see cref="M:System.Object.GetHashCode" />
        /// </summary>
        /// <returns>
        ///     <see cref="M:System.Object.GetHashCode" />
        /// </returns>
        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = Id.GetHashCode() ^ 31;
                        // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)
                return _requestedHashCode.Value;
            }
            return base.GetHashCode();
        }

        /// <summary>
        ///     Equal
        /// </summary>
        /// <returns>
        ///     <see cref="M:System.Boolean" />
        /// </returns>
        public static bool operator ==(Entity<TEntity> left, Entity<TEntity> right)
        {
            return Equals(left, null) ? (Equals(right, null)) : left.Equals(right);
        }

        /// <summary>
        ///     Not equal
        /// </summary>
        /// <returns>
        ///     <see cref="M:System.Boolean" />
        /// </returns>
        public static bool operator !=(Entity<TEntity> left, Entity<TEntity> right)
        {
            return !(left == right);
        }

        #endregion
    }
}