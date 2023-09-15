using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;

namespace Sxe.Engine.UI
{
    public class XboxMessageInfo
    {
        private List<string> options;

        public event EventHandler<XboxMessageEventArgs> Completed;

        public string Title
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public PlayerIndex PlayerIndex
        {
            get;
            set;
        }

        public MessageBoxIcon MessageBoxIcon
        {
            get;
            set;
        }



        public List<string> Options
        {
            get { return options; }
        }

        public XboxMessageInfo(List<string> optionsList)
        {
            this.options = optionsList;
        }

        //This is a weird delegation, we should maybe just have the host send the result as opposed to sending a full fledged eventargs
        public void FireEvent(XboxMessageEventArgs args)
        {

            //Fire associated message info event
            if (this.Completed != null)
                this.Completed(this, args);
        }
    }
}
