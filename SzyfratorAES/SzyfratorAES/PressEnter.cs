using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SzyfratorAES
{
    public static class CustomCommands
    {
        public static readonly RoutedUICommand Send = new RoutedUICommand
                (
                        "Send",
                        "Send",
                        typeof(CustomCommands),
                        new InputGestureCollection()
                        {
                                        new KeyGesture(Key.Enter)
                        }
                );
    }
}
