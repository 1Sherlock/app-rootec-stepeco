using System;

namespace Stepeco.Core.DAL.Entities.Base
{
    public interface IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        TKey Id { get; set; }
    }
}
