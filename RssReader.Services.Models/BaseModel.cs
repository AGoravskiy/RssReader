using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader.Services.Models
{
    /// <summary>
    /// Represents an abstract base model. 
    /// </summary>
    public abstract class BaseModel
    {
        /// <summary>
        /// Gets or sets a base model ID.
        /// </summary>
        public int Id { get; set; }
    }
}
