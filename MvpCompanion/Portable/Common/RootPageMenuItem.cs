using MvpCompanion.Portable.Views;
using System;

namespace MvpCompanion.Portable.Common
{
    public class RootPageMenuItem
    {
        public RootPageMenuItem()
        {
            TargetType = typeof(RootPageDetail);
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string IconPath { get; set; }

        public Type TargetType { get; set; }
    }
}