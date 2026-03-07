using System;
using System.Collections.Generic;
using System.Text;

namespace KpopHall.Application.Artists.CreateArtist
{
    public class CreateArtistResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
