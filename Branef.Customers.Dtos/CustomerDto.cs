using Branef.Framework.Data;
using System.ComponentModel.DataAnnotations;

namespace Branef.Customers.Dtos
{
    public class CustomerDto : BaseTrackableDto
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public CustomerSize Size { get; set; }
    }
}