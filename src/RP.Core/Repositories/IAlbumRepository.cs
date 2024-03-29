﻿using RP.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RP.Core.Repositories
{
    public interface IAlbumRepository
    {
        Task<IEnumerable<Album>> GetAsync(int? userId = null);
    }
}
