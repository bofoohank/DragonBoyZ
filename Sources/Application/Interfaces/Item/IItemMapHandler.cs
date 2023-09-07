using System;
using DragonBoyZ.Model.Map;

namespace DragonBoyZ.Application.Interfaces.Item
{
    public interface IItemMapHandler : IDisposable
    {
        void Update(Zone zone);
    }
}