using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using PoolScoreboard.Application.DataAccess.AppContext;
using PoolScoreboard.Application.Interfaces;

namespace PoolScoreboard.Application.DataAccess.Match
{
    public class FrameRepository : Repository<Frame, FrameDto>
    {
        protected override Frame CastFromDto(FrameDto dto)
        {
            var frame = new Frame
            {
                Id = dto.Id,
                //Shots = shotRepository.List(frame.Id);
                Start = DateTime.Parse(dto.Start)
            };
            return frame;
        }
    }
}