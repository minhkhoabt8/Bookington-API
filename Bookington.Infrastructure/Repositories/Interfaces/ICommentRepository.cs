﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookington.Core.Entities;

namespace Bookington.Infrastructure.Repositories.Interfaces
{
    public interface ICommentRepository :
        IGetAllAsync<Comment>,
        IAddAsync<Comment>,
        IUpdate<Comment>,
        IFindAsync<Comment>,
        IDelete<Comment>
    {
    }
}