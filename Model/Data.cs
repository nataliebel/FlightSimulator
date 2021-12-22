using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    class Data
    {
        private Dictionary<string, double> dic;
        public Dictionary<string, double> Dic
        {
            get
            {
                return dic;
            }
        }
        static Data instance = null;
        private Data()
        {
            dic = new Dictionary<string, double>();
        }
        static public Data getInstance()
        {
            if (instance == null)
            {
                instance = new Data();
                return instance;
            }
            else return instance;
        }
        public void setVal(string key, double value)
        {
            dic.Add(key, value);
            update();
        }
        public double getVal(string key)
        {
            if (dic.ContainsKey(key))
            {
                return dic[key];
            }
            throw new Exception();
        }
        public void update()
        {

        }
    }
}
