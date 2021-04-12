using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InfTeh_test.Models.Toast
{
    public class ToastModel
    {
        public ToastModel()
        {
            IsAutohide = true;
            Delay_ms = 40000;
        }

        public string Head { get; set; }
        public string SmallHead { get; set; }
        public string CloseText { get; set; }
        public string Message { get; set; }

        public bool IsAutohide { get; set; }
        public long Delay_ms { get; set; }

        public string BgColor { get; set; }
        public string HeadColor { get; set; }
        public string HeadTextColor { get; set; }
        public string BodyTextColor { get; set; }
        public string CloseLinkColor { get; set; }
    }
}