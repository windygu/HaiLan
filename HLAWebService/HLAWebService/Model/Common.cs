using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using HLAWebService.Utils;

namespace HLAWebService.Model
{
    public class CPDAZlkUploadData
    {
        public string loucheng;
        public string gonghao;
        public List<string> epcs;
    }

    public class CPDAZlkReDataSKU
    {
        public string gongHao;
        public string pin;
        public string se;
        public string gui;
        public string count;
        public CPDAZlkReDataSKU() { }
        public CPDAZlkReDataSKU(string gh,string p,string s,string g,string c)
        {
            gongHao = gh;
            pin = p;
            se = s;
            gui = g;
            count = c;
        }
    }
    public class CPDAZlkReData
    {
        public string status;
        public string msg;

        public List<CPDAZlkReDataSKU> skuData = new List<CPDAZlkReDataSKU>();
    }


}