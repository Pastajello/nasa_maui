using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nasa_maui.Interfaces
{
    public enum ScreenOrientation
    {
        Portrait,Landscape
    }
    public interface IScreenOrientationService
    {
        void SetScreenOrientation(ScreenOrientation screenOrientation);
    }
}
