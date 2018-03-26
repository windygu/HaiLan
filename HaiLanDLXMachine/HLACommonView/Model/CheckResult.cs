using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonView.Model
{
    public class CheckResult
    {
        public CheckResult()
        {
            IsRecheck = false;
            InventoryResult = true;
            Message = "";
        }
        public bool IsRecheck { get; set; }

        public bool InventoryResult { get; set; }
        public string Message { get; set; }

        public void UpdateMessage(string message)
        {
            if (!Message.Contains(message))
            {
                Message += message + ";";
            }
        }

        public void Init()
        {
            IsRecheck = false;
            InventoryResult = true;
            Message = "";
        }

        public CheckResult copy()
        {
            CheckResult re = new CheckResult();
            re.IsRecheck = IsRecheck;
            re.InventoryResult = InventoryResult;
            re.Message = Message;

            return re;
        }
    }
}
