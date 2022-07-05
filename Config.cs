using System.ComponentModel;
using Exiled.API.Interfaces;

namespace SCP106_BD
{
    public sealed class Config : IConfig
    {
        [Description("Plugin enabled?")]
        public bool IsEnabled { get; set; } = true;

        [Description("Time to wait before checking if 106 is present in the round (3F as default)")]
        public float Time_Per_SCP_Check { get; private set; } = 3F;

        [Description("Time to wait before checking the distance between all the doors and 106 (0.1F as default)")]
        public float Time_Per_Doors_check { get; set; } = 0.1F;
    }
}