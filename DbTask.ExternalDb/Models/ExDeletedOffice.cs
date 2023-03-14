﻿using System.ComponentModel.DataAnnotations.Schema;

namespace DbTask.ExternalDb.Models
{
    public sealed class ExDeletedOffice
    {
        /// <summary>
        /// Gets or sets id.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Gets or sets id of city.
        /// </summary>
        public long CityId { get; set; }

        /// <summary>
        /// Gets or sets time when office was deleted.
        /// </summary>
        public DateTime DeletedAt { get; set; }
    }
}
