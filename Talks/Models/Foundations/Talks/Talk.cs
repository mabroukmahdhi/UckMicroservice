using System;

namespace Talks.Models.Foundations.Talks
{
    public class Talk
    {
        public Guid Id { get; set; }

        // TODO: Add your properties here. 

        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }

    }
}
