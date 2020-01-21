using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public interface IEmail
    {
        void SetCommand(ICommand command);
    }
}
