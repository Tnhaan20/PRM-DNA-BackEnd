using System;
using System.Linq;
using System.Collections.Generic;

namespace DNATestingSystem.Repository.NhanVT.ModelExtensions
{
    public static class DtoHelper
    {
        /// <summary>
        /// Converts an entity to a simple DTO excluding navigation properties
        /// </summary>
        public static TDto ToDto<TEntity, TDto>(this TEntity entity) where TDto : new()
        {
            if (entity == null)
                return default;

            var dto = new TDto();
            var entityType = typeof(TEntity);
            var dtoType = typeof(TDto);

            foreach (var dtoProp in dtoType.GetProperties())
            {
                var entityProp = entityType.GetProperty(dtoProp.Name);
                if (entityProp != null && dtoProp.CanWrite && entityProp.CanRead)
                {
                    var value = entityProp.GetValue(entity);
                    if (value != null && 
                        !typeof(IEnumerable<object>).IsAssignableFrom(entityProp.PropertyType) &&
                        entityProp.PropertyType.IsPublic &&
                        !entityProp.PropertyType.IsClass || entityProp.PropertyType == typeof(string))
                    {
                        dtoProp.SetValue(dto, value);
                    }
                }
            }

            return dto;
        }

        /// <summary>
        /// Converts a list of entities to a list of DTOs
        /// </summary>
        public static List<TDto> ToDtoList<TEntity, TDto>(this IEnumerable<TEntity> entities) where TDto : new()
        {
            return entities?.Select(e => e.ToDto<TEntity, TDto>()).ToList();
        }
    }
}
