using System;

namespace Octopus.Configuration
{
    public interface IModifiableConfiguration
    {
        void Save();
    }
}